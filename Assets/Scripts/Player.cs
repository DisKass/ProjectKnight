using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    UnityEngine.CharacterController characterController;
    [SerializeField]
    float speed;
    Camera camera;
    Transform tr;
    float currentRot;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float gravity;
    SphereCollider col;
    Vector3 movement = Vector3.zero;
    private Animator anim;
    float vertAxis;

    [SerializeField]
    private GameObject weapon;
    public GameObject Weapon { get => weapon; set => weapon = value; }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        col = GetComponent<SphereCollider>();
        characterController = GetComponent<UnityEngine.CharacterController>();
        camera = GetComponentInChildren<Camera>();
        tr = transform;
        currentRot = tr.rotation.y;
        anim = GetComponent<Animator>();
        anim.SetBool("isKeepingSword", true);
        //Weapon = gameObject.transform.Find("mixamorig:RightHand").GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Attack();
        RotateCharacter();
        PlayAnimations();
        //Debug.Log(" " + characterController.isGrounded);
    }
        //vertAxis = Input.GetAxis("Vertical");
        //if (vertAxis > 0)
        //{
        //    //anim.SetBool("isRunning", true);
        //}
        //else;
        ////anim.SetBool("isRunning", false);

        //if (vertAxis < 0)
        //{
        //    //anim.SetBool("isWalkingBack", true);
        //}
        //else;
        //    //anim.SetBool("isWalkingBack", false);

        //movement = vertAxis * tr.forward;
        //movement *= speed;
        //movement.y = 0;

        //if (characterController.isGrounded)
        //{
        //    //anim.SetBool("isFalling", false);
        //    if (Input.GetButton("Jump"))
        //    {
        //        //anim.SetTrigger("Jump");
        //        //nim.SetBool("isFalling", true);
        //    }
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //anim.SetTrigger("Attack");
        //    }
        //}
        //else
        //{
        //    //anim.SetBool("isFalling", true);
        //}
        //// Move the controller
        //characterController.Move((movement) * Time.deltaTime);
        //currentRot += 5 * Input.GetAxis("Mouse X");
        //tr.rotation = Quaternion.Euler(0, currentRot, 0);
        //camera.transform.RotateAround(tr.position + Vector3.up * 3, tr.right, -5* Input.GetAxis("Mouse Y"));
//    }
    void MoveCharacter()
    {
        float vertAx = Input.GetAxisRaw("Vertical");
        float vertDir = movement.y;
        movement = tr.forward * speed * vertAx;
        movement.y = vertDir;

        if (characterController.isGrounded)
        {
            movement.y = -0.1f;
            if (Input.GetButton("Jump"))
            {
                movement.y = jumpForce;
            }
        }
        else
        {
            movement.y -= gravity * Time.deltaTime;
        }
        characterController.Move(movement * Time.deltaTime);
    }
    void RotateCharacter()
    {
        currentRot += 5 * Input.GetAxis("Mouse X");
        tr.rotation = Quaternion.Euler(0, currentRot, 0);
        camera.transform.RotateAround(tr.position + Vector3.up * 3, tr.right, -5 * Input.GetAxis("Mouse Y"));
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Weapon = gameObject.GetComponent<PlayerStats>().currentWeapon;
            if (Weapon == null) return;
                Weapon.GetComponent<BoxCollider>().enabled = true;
        }
    }
    void PlayAnimations()
    {
        byte status = 0;
        // 0 - idle
        // 1 - running
        // 2 - backWalk
        // 3 - jump
        // 4 - backJump
        // 5 - attacking
        // 6 - falling
        if (characterController.isGrounded)
        {
            if (Input.GetAxisRaw("Vertical") > 0) status = 1;
            if (Input.GetAxisRaw("Vertical") < 0) status = 2;
            if (Input.GetButton("Jump"))
            {
                if (status == 2) status = 4;
                else status = 3;
            }
            if (Input.GetMouseButtonDown(0)) status = 5;
        }
        else
            status = 6;

        switch (status)
        {
            case 0: AnimatorCondition(); break;
            case 1: AnimatorCondition(isRunning: true); break;
            case 2: AnimatorCondition(isWalkingBack: true); break;
            case 3: AnimatorCondition(Jump: true); break;
            case 4: AnimatorCondition(isWalkingBack: true, Jump: true); break;
            case 5: AnimatorCondition(Attack: true); break;
            case 6: AnimatorCondition(isFalling: true); break;
            default: break;
        }
    }
    void AnimatorCondition(bool isRunning = false,
                            bool isWalkingBack = false,
                            bool isFalling = false,
                            bool Jump = false,
                            bool Attack = false)
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isWalkingBack", isWalkingBack);
        
            //if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
            //    anim.GetCurrentAnimatorStateInfo(0).IsName("BackFlip") ||
            //    anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"))) ;
            anim.SetBool("isFalling", isFalling);
        if (Jump)
        {
            anim.SetTrigger("Jump");
        }
        if (Attack)
        {
            if (Weapon != null)
            {
                anim.Play(Weapon.GetComponent<WeaponStat>().attackAnimationName); // Weapon устанавливается в метода Attack
                //Debug.Log(anim.GetNextAnimatorStateInfo(0).length);
                StartCoroutine(DoWithDelay(Weapon.GetComponent<WeaponStat>().attackAnimationDuration,
                    () => Weapon.GetComponent<BoxCollider>().enabled = false));
            }
        }
    }
    
    IEnumerator DoWithDelay(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
        StopCoroutine("DoWithDelay");
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}

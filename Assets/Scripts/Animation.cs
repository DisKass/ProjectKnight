using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    bool jumped = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isKeepingSword", true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else anim.SetBool("isRunning", false);

        if (Input.GetKey(KeyCode.Space))
        {
            if (!jumped)
            {
                jumped = true;
                anim.SetTrigger("Jump");
                anim.SetBool("isFalling", true);
            }
           
        }
       
        if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("isWalkingBack", true);
        }
        else anim.SetBool("isWalkingBack", false);

        if (Input.GetMouseButtonDown(0))
        {
            if (!jumped)
                anim.SetTrigger("Attack");

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jumped = false;
            anim.SetBool("isFalling", false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            anim.SetBool("isFalling", true);
            jumped = true;
        }
        

    }
}

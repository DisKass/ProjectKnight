using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] // ***** Скорость движения
    float maxSpeed;
    [SerializeField]
    float acceleration;
    float currentSpeed;
    Vector3 currentPosition;

    [SerializeField]
    float sensitivity;
    Vector2 currentRotation;
    [SerializeField]
    Vector2 maxVerticalAngle;
    [SerializeField]
    bool invertOX;

    internal bool isPlaying = true;

    Transform tr;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        gameObject.tag = "Player";
        camera = GetComponentInChildren<Camera>();
        tr = transform;
        currentPosition = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) return;
        if (Input.GetKeyDown(KeyCode.R)) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // ****************acceleration******************
        //if (Input.GetAxis("Vertical") > 0)
        //    currentSpeed += acceleration;
        //if (Input.GetAxis("Vertical") < 0)
        //    currentSpeed -= acceleration*0.5f;

        //if (Input.GetAxis("Vertical") == 0)
        //    if (currentSpeed != 0)
        //    {
        //        if (Mathf.Abs(currentSpeed) < acceleration) currentSpeed = 0;
        //        else currentSpeed -= acceleration * Mathf.Sign(currentSpeed);
        //    }
        //if (Mathf.Abs(currentSpeed) > maxSpeed) currentSpeed = maxSpeed * Mathf.Sign(currentSpeed);
        if (Input.GetAxis("Vertical") > 0)
            currentSpeed = maxSpeed;
        if (Input.GetAxis("Vertical") == 0)
            currentSpeed = 0;
        if (Input.GetAxis("Vertical") < 0)
            currentSpeed = -maxSpeed;

        currentRotation.y = -sensitivity * Input.GetAxis("Mouse Y");

        if (invertOX)
            currentRotation.x -= sensitivity * Input.GetAxis("Mouse X");
        else
            currentRotation.x += sensitivity * Input.GetAxis("Mouse X");

        //currentRotation.y = Mathf.Clamp(currentRotation.y, maxVerticalAngle.x, maxVerticalAngle.y);

        tr.rotation = Quaternion.Euler(0, currentRotation.x, 0); // вертикаль, горизонталь
        //if (camera.transform.rotation.x + currentRotation.y > maxVerticalAngle.y) currentRotation.y = maxVerticalAngle.y - camera.transform.rotation.x;
        camera.transform.RotateAround(tr.position + Vector3.up * 3, tr.right, currentRotation.y);
        currentPosition = new Vector3(currentSpeed * Time.deltaTime * tr.forward.x,
                                        0,
                                        currentSpeed * Time.deltaTime * tr.forward.z);
        tr.position += currentPosition;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpScript : MonoBehaviour
{
    [SerializeField]
    float forceAmount;
    [SerializeField]
    float MaxSpeed;
    Rigidbody rb;
    bool jumped = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < -MaxSpeed)
        {
            //Debug.Log(rb.velocity.y);
            rb.velocity = new Vector3(rb.velocity.x, -MaxSpeed, rb.velocity.z);
            //transform.position = new Vector3(14, 2, 11);
            rb.position = new Vector3(14, 2, 11);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (!jumped)
            {
                jumped = true;
                rb.AddForce(Vector3.up*forceAmount, ForceMode.Impulse);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor") jumped = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor") jumped = true;
    }
}

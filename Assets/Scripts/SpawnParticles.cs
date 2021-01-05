using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject partGM;
    ParticleSystem particle;
    void Start()
    {
        //particle = partGM.GetComponent<ParticleSystem>();
        //particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
            //Debug.Log(collision.GetContact(0).normal);
        if (collision.gameObject.tag == "Weapon")
        {
            GameObject tmp;
            tmp = Instantiate(partGM, transform);
            tmp.transform.position = collision.GetContact(0).point;
            tmp.transform.Rotate(collision.GetContact(0).normal * 180);
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.red);
            }
            //collision.GetContact(0).
            particle = partGM.GetComponent<ParticleSystem>();
            particle.Play();
        }
    }
}

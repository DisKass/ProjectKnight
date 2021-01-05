using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCopyByTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] flowers;
    [SerializeField]
    private float[] frequency;
    Transform tr;
    Vector3 pos;
    bool spawner = true;
    static GameObject way;
    static Transform wayTr;
    [SerializeField]
    static float height = 0;

    
    void Start()
    {
        tr = transform;
        pos = tr.position;
        if (way == null)
        {
            //way = new GameObject("Way");
            wayTr = GameObject.Find("greenGrounds").transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (spawner)
            {
                if (Random.value < 0.03)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(wayTr);
                    cube.transform.position = pos + Vector3.right * 1 + Vector3.up * 1.5f + Vector3.up * height;
                    cube.transform.localScale += Vector3.up * 2;
                }
                if (Random.value < 0.7)
                {

                    GameObject gm = Instantiate(gameObject, wayTr);
                    gm.transform.position = pos + Vector3.right * 1 + Vector3.up * height;
                    height = - Random.value;
                    gm.GetComponent<MeshCollider>().isTrigger = false;
                    gm.GetComponent<MeshCollider>().convex = false;
                    gm.GetComponent<MeshRenderer>().enabled = true;

                    if (Random.value < frequency[0])
                    {
                        Instantiate(flowers[0], tr.parent).transform.position = pos + Vector3.right * 1 + Vector3.up * height;
                    }
                }
                else
                {
                    GameObject gm = Instantiate(gameObject, wayTr);
                    gm.transform.position = pos + Vector3.right * 1 + Vector3.up * height;
                    gm.GetComponent<MeshCollider>().convex = true;
                    gm.GetComponent<MeshCollider>().isTrigger = true;
                    gm.GetComponent<MeshRenderer>().enabled = false;
                }
                spawner = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }
}

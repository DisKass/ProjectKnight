using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] model;
    [SerializeField]
    private Vector4 padding; // x, x, z, z
    [SerializeField]
    private float scaleFactor;
    [SerializeField]
    private Vector2 floorSizeX, floorSizeY;
    [SerializeField]
    private float[] frequency;
    void Start()
    {
        float tmp;
        Vector3 position = new Vector3();
        GameObject[] flowerContainers = new GameObject[model.Length];
        for (int i = 0; i < flowerContainers.Length; i++)
        {
            flowerContainers[i] = new GameObject("Floewrs" + i);
            flowerContainers[i].isStatic = true;
        }
        
        for (float i = floorSizeX.x + 0.5f; i < floorSizeX.y; i++)
        {
            for (float j = floorSizeY.x + 0.5f; j < floorSizeY.y; j++)
            {
                float lastFrequency = 0;
                for (int k = 0; k < frequency.Length; k++)
                {
                    tmp = Random.value;
                    if (tmp < frequency[k] + lastFrequency && tmp >= lastFrequency)
                    {
                        position.x = i + Random.Range(padding.x, padding.y) * scaleFactor;
                        position.z = j + Random.Range(padding.z, padding.w) * scaleFactor;
                        position.y = -7 * 0.0625f;
                        Instantiate(model[k], position, Quaternion.identity, flowerContainers[k].transform).isStatic = true;
                        break;
                    }
                    lastFrequency += frequency[k];
                }
            }
        }
        for (int i = 0; i < flowerContainers.Length; i++)
            if (flowerContainers[i].GetComponentsInChildren<Transform>().Length > 3)
                flowerContainers[i].AddComponent<CombiningChildMesh>();
    }

}

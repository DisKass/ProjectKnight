using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArmor : MonoBehaviour
{
    int count = 0;
    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.TryGetComponent<TakeDamage>(out TakeDamage a))
        {
            a.Hit(GetComponent<WeaponStat>().damage);
        }
        if (target.tag == "Block")
        {
            Debug.Log("Block" + count++);
            GetComponent<BoxCollider>().enabled = false;
            return;
        }
        if (target.name.Substring(target.name.Length-5) == "Armor")
        {
            Debug.Log(target.name);
            Destroy(target);
            GetComponent<BoxCollider>().enabled = false;

        }
    }
}

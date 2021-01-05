using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public void Hit(float damage)
    {
        GameObject.Find("DeathKnight@T-Pose(Clone)").GetComponent<PlayerStats>().health.value -= damage;
    }
}

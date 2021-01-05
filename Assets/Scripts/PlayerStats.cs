using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider health;
    public GameObject currentWeapon;

    [SerializeField]
    private GameObject[] Weapons;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();
    }
    void SelectWeapon()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < Weapons.Length && i <= 9; i++)
            {
                if (Input.GetKeyDown((KeyCode)(49 + i)))
                {
                    Transform tr = GameObject.FindGameObjectWithTag("RightHand").transform;
                    if (currentWeapon != null && currentWeapon.name == Weapons[i].name) break;
                    if (tr.childCount > 0)
                    {
                        Destroy(tr.GetChild(0).gameObject);
                    }
                    currentWeapon = Instantiate(Weapons[i], tr);
                    currentWeapon.name = Weapons[i].name;
                    gameObject.GetComponent<Player>().GetAnimator().SetBool("isKeepingSword", true);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    Transform tr = GameObject.FindGameObjectWithTag("RightHand").transform;
                    if (tr.childCount > 0)
                    {
                        Destroy(tr.GetChild(0).gameObject);
                    }
                    currentWeapon = null;
                    gameObject.GetComponent<Player>().GetAnimator().SetBool("isKeepingSword", false);
                }
            }
        }
    }
}

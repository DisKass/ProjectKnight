using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayer : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach(Behaviour e in componentsToDisable)
            {
                e.enabled = false;
            }
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}

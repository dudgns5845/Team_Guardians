using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Rio : MonoBehaviour
{
    public PlayerMove pm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pm.DamangeAction(10);
        }
    }
}

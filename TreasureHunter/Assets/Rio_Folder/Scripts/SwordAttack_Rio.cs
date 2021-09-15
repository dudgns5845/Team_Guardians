using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack_Rio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 맞은 녀석이 enemy라면
        enemy_Rio enemy = other.transform.GetComponent<enemy_Rio>();
        if (enemy) //참이면
        {
            // enemy에게 맞았다는 것을 알려준다
            enemy.OnDamageProcess(other.transform.forward);
        }

        // 맞은 녀석이 enemy라면
        Rikayon crab = other.transform.GetComponentInParent<Rikayon>();
        if (crab) //참이면
        {
            // enemy에게 맞았다는 것을 알려준다
            crab.OnDamageProcess(other.transform.forward);
        }
    }
}

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

        // 留욎? ???앹씠 enemy?쇰㈃
        Boss_Rio boss = other.transform.GetComponentInParent<Boss_Rio>();
        if (boss) //李몄씠硫?
        {
            // enemy?먭쾶 留욎븯?ㅻ뒗 寃껋쓣 ?뚮젮以???
            boss.OnDamageProcess(other.transform.forward);
        }

        // 맞은 녀석이 enemy라면
        FinalStageEnemy_Crab_Rio crab_1 = other.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
        if (crab_1) //참이면
        {
            // enemy에게 맞았다는 것을 알려준다
            crab_1.OnDamageProcess(other.transform.forward);
        }

        FinalStageEnemy_Skull_Rio skull_1 = other.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
        if (skull_1) //참이면
        {
            // enemy에게 맞았다는 것을 알려준다
            skull_1.OnDamageProcess(other.transform.forward);
        }

    }
}

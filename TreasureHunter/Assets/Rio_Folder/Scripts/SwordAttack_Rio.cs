using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack_Rio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ���� �༮�� enemy���
        enemy_Rio enemy = other.transform.GetComponent<enemy_Rio>();
        if (enemy) //���̸�
        {
            // enemy���� �¾Ҵٴ� ���� �˷��ش�
            enemy.OnDamageProcess(other.transform.forward);
        }

        // ���� �༮�� enemy���
        Rikayon crab = other.transform.GetComponentInParent<Rikayon>();
        if (crab) //���̸�
        {
            // enemy���� �¾Ҵٴ� ���� �˷��ش�
            crab.OnDamageProcess(other.transform.forward);
        }

        // 맞�? ?�?�이 enemy?�면
        Boss_Rio boss = other.transform.GetComponentInParent<Boss_Rio>();
        if (boss) //참이�?
        {
            // enemy?�게 맞았?�는 것을 ?�려준??
            boss.OnDamageProcess(other.transform.forward);
        }

        // ���� �༮�� enemy���
        FinalStageEnemy_Crab_Rio crab_1 = other.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
        if (crab_1) //���̸�
        {
            // enemy���� �¾Ҵٴ� ���� �˷��ش�
            crab_1.OnDamageProcess(other.transform.forward);
        }

        FinalStageEnemy_Skull_Rio skull_1 = other.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
        if (skull_1) //���̸�
        {
            // enemy���� �¾Ҵٴ� ���� �˷��ش�
            skull_1.OnDamageProcess(other.transform.forward);
        }

    }
}

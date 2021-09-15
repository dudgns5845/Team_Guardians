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
    }
}

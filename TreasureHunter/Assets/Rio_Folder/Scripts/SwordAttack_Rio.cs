using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack_Rio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ¸ÂÀº ³à¼®ÀÌ enemy¶ó¸é
        enemy_Rio enemy = other.transform.GetComponent<enemy_Rio>();
        if (enemy) //ÂüÀÌ¸é
        {
            // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
            enemy.OnDamageProcess(other.transform.forward);
        }

        // ¸ÂÀº ³à¼®ÀÌ enemy¶ó¸é
        Rikayon crab = other.transform.GetComponentInParent<Rikayon>();
        if (crab) //ÂüÀÌ¸é
        {
            // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
            crab.OnDamageProcess(other.transform.forward);
        }

        // ë§ì? ?€?ì´ enemy?¼ë©´
        Boss_Rio boss = other.transform.GetComponentInParent<Boss_Rio>();
        if (boss) //ì°¸ì´ë©?
        {
            // enemy?ê²Œ ë§ì•˜?¤ëŠ” ê²ƒì„ ?Œë ¤ì¤€??
            boss.OnDamageProcess(other.transform.forward);
        }

        // ¸ÂÀº ³à¼®ÀÌ enemy¶ó¸é
        FinalStageEnemy_Crab_Rio crab_1 = other.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
        if (crab_1) //ÂüÀÌ¸é
        {
            // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
            crab_1.OnDamageProcess(other.transform.forward);
        }

        FinalStageEnemy_Skull_Rio skull_1 = other.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
        if (skull_1) //ÂüÀÌ¸é
        {
            // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
            skull_1.OnDamageProcess(other.transform.forward);
        }

    }
}

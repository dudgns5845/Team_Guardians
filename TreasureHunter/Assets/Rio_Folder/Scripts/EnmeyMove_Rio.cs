using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyMove_Rio : MonoBehaviour
{
    public GameObject target; //따라갈 대상
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        /*
         * 타겟 방향으로 이동하고 싶다
         * 1.타겟 방향을 구하자 -> 벡터의 빼기
         * - 타겟 방향 = 타겟 위치 - 나의 위치
         */
        Vector3 dir = target.transform.position - transform.position;
        //gameObject.transform.LookAt(transform.forward);
        dir.Normalize(); //벡터의 정규화를 할 것
                         //그 방향으로 이동하고 싶다 Posisiton = Direction * Poisitiont
        transform.position += dir * speed * Time.deltaTime;
    }
}

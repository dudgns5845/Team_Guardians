using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyMove_Rio : MonoBehaviour
{
    public GameObject target; //���� ���
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        /*
         * Ÿ�� �������� �̵��ϰ� �ʹ�
         * 1.Ÿ�� ������ ������ -> ������ ����
         * - Ÿ�� ���� = Ÿ�� ��ġ - ���� ��ġ
         */
        Vector3 dir = target.transform.position - transform.position;
        //gameObject.transform.LookAt(transform.forward);
        dir.Normalize(); //������ ����ȭ�� �� ��
                         //�� �������� �̵��ϰ� �ʹ� Posisiton = Direction * Poisitiont
        transform.position += dir * speed * Time.deltaTime;
    }
}

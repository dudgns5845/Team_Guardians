using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terret : MonoBehaviour
{
    GameObject target;
    Renderer color;
    enum State
    {
        UNDETECT,
        DETECT
    }
    State m_state = State.UNDETECT;


    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        color = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case State.UNDETECT:
                Undetect();
                break;
            case State.DETECT:
                Detect();
                break;
        }
    }

    //������ : �� ���� �� �ٶ󺸱�
    private void Detect()
    {
        color.material.color = new Color(255,39,0);
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, RotSpeed * Time.deltaTime);
    }

    //���� ���� �� : ȸ�� ����
    public float RotSpeed = 30f;
    private void Undetect()
    {
        color.material.color = new Color(0, 199, 255);
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
    }

    //���� ���� �ȿ� ���ö� ���� ���·� ����
    private void OnTriggerEnter(Collider other)
    {
        m_state = State.DETECT;
    }

    //���� ���� ����� ���� ���·� ����
    private void OnTriggerExit(Collider other)
    {
        m_state = State.UNDETECT;
    }
}

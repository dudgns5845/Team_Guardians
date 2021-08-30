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

    //감지시 : 색 변경 및 바라보기
    private void Detect()
    {
        color.material.color = new Color(255,39,0);
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, RotSpeed * Time.deltaTime);
    }

    //감지 안할 때 : 회전 진행
    public float RotSpeed = 30f;
    private void Undetect()
    {
        color.material.color = new Color(0, 199, 255);
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
    }

    //영역 범위 안에 들어올때 감지 상태로 변경
    private void OnTriggerEnter(Collider other)
    {
        m_state = State.DETECT;
    }

    //영역 범위 벗어나면 비감지 상태로 변경
    private void OnTriggerExit(Collider other)
    {
        m_state = State.UNDETECT;
    }
}

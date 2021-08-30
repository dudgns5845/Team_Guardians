using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot_00 : MonoBehaviour
{
    //마우스의 회전 누적치를 저장할 변수
    float mx;
    float my;

    //회전할 속도
    float rotSpeed = 500;

    //회전 가능 여부
    public bool canRotH;
    public bool canRotV;

    // 진동 크기
    public float amp = 0.07f;
    public float freq = 20;
    float tempY;

    private void Start()
    {
        tempY = transform.localPosition.y;
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;
    }

    void Update()
    {
        //마우스의 움직임을 받아서
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //마우스의 회전값으로 각도를 누적하고(계산하고)
        if (canRotV)
            mx += v * rotSpeed * Time.deltaTime;
        if (canRotH)
            my += h * rotSpeed * Time.deltaTime;

        //mx 최소값을 -60, 최대값을 60으로 셋팅
        mx = Mathf.Clamp(mx, -60, 60);
        //누적된 회전값으로 게임오브젝트의 각도를 셋팅하자
        transform.localEulerAngles = new Vector3(-mx, my, 0);

        // 만약 카메라라면 
        if (gameObject.tag == "MainCamera")
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                amp = 0.07f;
                freq = 20;
                if (Input.GetKey(KeyCode.LeftShift)) //달리기 구현
                {
                    print("shift눌림");
                    amp = 0.07f;
                    freq = 50;
                }
                transform.localPosition = new Vector3(0, tempY + amp * Mathf.Sin(freq * Time.time), 0);
            }
        }
    }
}

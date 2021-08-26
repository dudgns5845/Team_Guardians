using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    //마우스의 회전 누적치 저장할 변수
    float mx;
    float my;

    //회전속력
    float rotSpeed = 150;

    //회전 가능여부
    public bool canRotH;
    public bool canRotV;

    //진동크기
    public float amp = 1;
    public float freq = 2;
    float tempY;

    // Start is called before the first frame update
    void Start()
    {
        tempY = transform.localPosition.y;
        //현재 게임오브젝트의 각도를 mx.my에 셋팅
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        //마우스의 움직임을 받아서
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //마우스의 회전값으로 각도를 누적하고(계산하고)
        if (canRotV == true)
        {
            mx += v * rotSpeed * Time.deltaTime;
        }
        if (canRotH == true)
        {
            my += h * rotSpeed * Time.deltaTime;
        }


        //만약에 mx의 값이 -60보다 작으면
        //if (mx < -60) //-60으로 다시 셋팅 mx = -60;

        //만약에 mx의 값이 60보다 커지면
        //if (mx > 60) //60으로 셋팅 mx = 60;
        //mx최소값을 -60, 최대값을 60으로 셋팅
        mx = Mathf.Clamp(mx, -60, 60);




        //누적된 회전값으로 게임오브젝트의 각도를 셋팅하자.
        transform.localEulerAngles = new Vector3(-mx, my, 0);

        //만약 카메라라면 사용
        if (gameObject.tag == "MainCamera")
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                transform.localPosition = new Vector3(0, tempY + Mathf.Sin(freq * Time.time), 0);
            }

        }
    }
}

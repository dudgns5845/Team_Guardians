using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate_Rio : MonoBehaviour
{

    public float rotateSpeed = 150;
    /// <summary>
    /// 마우스의 회전 누적치 저장할 변수
    /// </summary>
    float mouseX;
    float mouseY;

    //회전가능여부
    public bool canRotH; // 좌우 회전 가능
    public bool canRotV; // 상하 회전 가능

    private void Start()
    {
        //현재 게임오브젝트의 각도를 mouseX,mouseY에 셋팅
        mouseX = transform.eulerAngles.x;
        mouseY = transform.eulerAngles.y;
        //transform.rotation의 값은 Quaternion을 반환하는데 이것은 자체 변환된 값이라
        //transform.eulerAngles를 사용해야한다
        //이는 inspector에서 보이는 roatation의 숫자값과 같다 
    }

    private void Update()
    {
        //마우스 움직임을 받아서
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        
        //마우스의 회전값으로 각도를 누적하고(계산하고) 
        if(canRotV == true)
        {
            mouseX += v * rotateSpeed * Time.deltaTime;
        }
        if(canRotH == true)
        {
            mouseY += h * rotateSpeed * Time.deltaTime;
        }

        //mouseX의 값을 -60~60의 범위로 제한
        //if(mouseX < -60)
        //{
        //    mouseX = -60;
        //}
        //if(mouseX > 60)
        //{
        //    mouseX = 60;
        //}
        //위의 과정과 똑같은 기능
        mouseX = Mathf.Clamp(mouseX, -60, 60);
        //mouseY = Mathf.Clamp(mouseY, -60, 60);


        //누적된 회전값으로 게임오브젝트의 각도를 셋팅하자
        transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);
    }
}

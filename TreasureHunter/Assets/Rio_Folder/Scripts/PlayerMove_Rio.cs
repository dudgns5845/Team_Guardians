using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Rio : MonoBehaviour
{
    public float speed = 5;

    CharacterController controller;

    //점프 파워
    public float jumpPower = 10;
    //y속도
    public float ySpeed;
    //중력
    public float gravity = -20;

    int jumpCnt = 0;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float DirH = Input.GetAxisRaw("Horizontal");
        float DirV = Input.GetAxisRaw("Vertical");
        //Vector3 dir = Vector3.right * DirH + Vector3.forward * DirV; //월드 좌표
        Vector3 dir = transform.right * DirH + transform.forward * DirV; //로컬 좌표
        dir.Normalize();
        //transform.position += dir * speed * Time.deltaTime;


        dir.y = Jump();


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 60;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 50;
        }

        //charactercontroller는 move함수로 움직여야 충돌 반응이 가능하다
        controller.Move(dir * speed * Time.deltaTime);

    }


    float Jump()
    {

        //점프 구현하기
        //점프 누르면 y속도를 jumpPower로 한다.
        if (controller.isGrounded)
        {
            ySpeed = 0;
            jumpCnt = 0;
        }
        if (Input.GetButtonDown("Jump"))
        {

            if (jumpCnt < 1)
            {
                jumpCnt++;
                ySpeed = jumpPower;
            }
        }
        //dir.y에 y속도를 넣는다
        float dirY = ySpeed;
        //y속도를 중력만큼 더 해준다.
        ySpeed += gravity * Time.deltaTime;

        return dirY;
    }
}

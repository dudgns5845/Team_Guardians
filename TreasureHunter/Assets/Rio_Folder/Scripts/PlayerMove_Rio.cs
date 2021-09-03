using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Rio : MonoBehaviour
{
    public float speed = 5;

    CharacterController controller;

    //���� �Ŀ�
    public float jumpPower = 10;
    //y�ӵ�
    public float ySpeed;
    //�߷�
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
        //Vector3 dir = Vector3.right * DirH + Vector3.forward * DirV; //���� ��ǥ
        Vector3 dir = transform.right * DirH + transform.forward * DirV; //���� ��ǥ
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

        //charactercontroller�� move�Լ��� �������� �浹 ������ �����ϴ�
        controller.Move(dir * speed * Time.deltaTime);

    }


    float Jump()
    {

        //���� �����ϱ�
        //���� ������ y�ӵ��� jumpPower�� �Ѵ�.
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
        //dir.y�� y�ӵ��� �ִ´�
        float dirY = ySpeed;
        //y�ӵ��� �߷¸�ŭ �� ���ش�.
        ySpeed += gravity * Time.deltaTime;

        return dirY;
    }
}

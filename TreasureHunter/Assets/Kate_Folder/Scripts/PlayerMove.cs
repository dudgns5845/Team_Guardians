using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�ӷ�
    public float speed = 5;
    //Character Controller
    CharacterController cc;

    //�����Ŀ�
    public float jumpPower = 5;

    //y�ӵ�
    public float yVelocity;

    //�߷�
    float gravity = -20;
    //����Ƚ��
    int jumpCount;

    //�ִ� ����Ƚ��
    public int maxJumpCount = 2;


    // Start is called before the first frame update
    void Start()
    {

        cc = gameObject.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //if(input.GetkeyDown(KeyCode.W))
        //{
        //    //������ ����
        //}
        //if(Input .GetKeyUp .A)
        //{
        //    //�������� ������ ���ְ�
        //}

        //A.D�¿� 
        float h = Input.GetAxis("Horizontal");
        //W.S �յ�
        float v = Input.GetAxis("Vertical");

        //������ ���ϰ�
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        Jump(out dir.y);

        //�� �������� �����̰� ���弼��. P = P0+VT
        cc.Move(dir * speed * Time.deltaTime);
    }
    void Jump(out float dirY)
    {
       
        //���࿡ �����̽��ٸ� ("Jump")�� ������
        if (Input.GetButtonDown("Jump"))       //|| Input.GetKeyDown(KeyCode.Space))
        {
            //���� Ƚ���� �ִ�Ƚ�� ���� ���� ������
            if (jumpCount < maxJumpCount)
            {
                //y�ӵ��� jumpPower�� �Ѵ�. 
                yVelocity = jumpPower;
                jumpCount++;  //2

            }

        }

        //dirY�� y�ӵ��� �ִ´�. 
        dirY = yVelocity;    //1.0.0 -> 1,-1,0
        //y�ӵ��� �߷¸�ŭ �����ش�. 
        yVelocity += gravity * Time.deltaTime;
    }
}
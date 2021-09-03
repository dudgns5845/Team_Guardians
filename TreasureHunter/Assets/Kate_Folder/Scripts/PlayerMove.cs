using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region before code
    //Character Controller
    CharacterController cc;

    //�����Ŀ�
    public float jumpPower = 5;

    //y�ӵ�
    public float yVelocity;

    //�߷�
    float gravity = -20;
    float playerGravity = 0;       //�÷��̾� �߷����뿡 �ʿ��� ����
    //����Ƚ��
    int jumpCount;

    //�ִ� ����Ƚ��
    public int maxJumpCount = 2;

    //�ӷ�
    public float speed = 6;
    public float runspeed = 10;
    public float moveSpeed = 6.0f;        //�̵� �ӵ�
    public float backmoveSpeed = 5.0f;    //�ڷΰ��� �ӵ�


    Vector3 moveDir = Vector3.zero; //�÷��̾� �̵�����

    CharacterController playerController;

    public Animator anim;

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

        anim.SetFloat("Speed", v * v + h * h);

        //������ ���ϰ�
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        Jump(out dir.y);

        //�� �������� �����̰� ���弼��. P = P0+VT
        cc.Move(dir * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runspeed;

        }
        else
        {
            speed = moveSpeed;
        }

    }
    void Jump(out float dirY)
    {

        //���࿡ �����̽��ٸ� ("Jump")�� ������
        if (Input.GetButtonDown("Jump"))       //|| Input.GetKeyDown(KeyCode.Space))
        {
            //���� Ƚ���� �ִ�Ƚ�� ���� ���� ������
            if (jumpCount < 1)
            {
                //y�ӵ��� jumpPower�� �Ѵ�. 
                yVelocity = jumpPower;
                jumpCount++;  //2

            }
            jumpCount = 0;
        }

        //dirY�� y�ӵ��� �ִ´�. 
        dirY = yVelocity;    //1.0.0 -> 1,-1,0
        //y�ӵ��� �߷¸�ŭ �����ش�. 
        yVelocity += gravity * Time.deltaTime;

    }
    #endregion

//    public float speed = 6.0F;
//    public float jumpSpeed = 8.0F;
//    public float gravity = 20.0F;
//    private Vector3 moveDirection = Vector3.zero;
//    void Update()
//    {
//        CharacterController controller = GetComponent<CharacterController>();
//        if (controller.isGrounded)
//        {
//            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//            moveDirection = transform.TransformDirection(moveDirection);

//            if (Input.GetKeyDown(KeyCode.LeftShift))
//            {
//                speed = 10f;
//            }
//            else
//            {
//                speed = 6f;
//            }

//            moveDirection *= speed;
//            if (Input.GetButton("Jump"))
//                moveDirection.y = jumpSpeed;

//        }
//        moveDirection.y -= gravity * Time.deltaTime;
//        controller.Move(moveDirection * Time.deltaTime);
//    }
}

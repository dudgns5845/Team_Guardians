using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    //Character Controller
    CharacterController cc;

    //점프파워
    public float jumpPower = 5;

    //y속도
    public float yVelocity;

    //중력
    float gravity = -20;
float playerGravity = 0;       //플레이어 중력적용에 필요한 변수
    //점수횟수
    int jumpCount;

    //최대 점프횟수
    public int maxJumpCount = 2;

    //속력
    public float speed = 6;
    public float runspeed = 10;
    public float moveSpeed = 6.0f;        //이동 속도
    public float backmoveSpeed = 5.0f;    //뒤로가는 속도

   
    Vector3 moveDir = Vector3.zero; //플레이어 이동방향

    CharacterController playerController;

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
    //    //방향을 왼쪽
    //}
    //if(Input .GetKeyUp .A)
    //{
    //    //왼쪽으로 방향을 없애고
    //}

    //A.D좌우 
    float h = Input.GetAxis("Horizontal");
        //W.S 앞뒤
        float v = Input.GetAxis("Vertical");

        //방향을 정하고
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        Jump(out dir.y);

        //위 방향으로 움직이게 만드세요. P = P0+VT
        cc.Move(dir * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runspeed;

        }
        else
        {
            speed = speed;
        }

    }
    void Jump(out float dirY)
    {
       
        //만약에 스페이스바를 ("Jump")를 누르면
        if (Input.GetButtonDown("Jump"))       //|| Input.GetKeyDown(KeyCode.Space))
        {
            //점프 횟수가 최대횟수 점프 보다 작으면
            if (jumpCount < maxJumpCount)
            {
                //y속도를 jumpPower로 한다. 
                yVelocity = jumpPower;
                jumpCount++;  //2

            }

        }

        //dirY에 y속도를 넣는다. 
        dirY = yVelocity;    //1.0.0 -> 1,-1,0
        //y속도를 중력만큼 더해준다. 
        yVelocity += gravity * Time.deltaTime;

    }
}

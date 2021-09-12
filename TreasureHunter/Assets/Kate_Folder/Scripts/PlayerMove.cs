using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    #region before code
    ////Character Controller
    //CharacterController cc;

    ////점프파워
    //public float jumpPower = 5;

    ////y속도
    //public float yVelocity;

    ////중력
    //float gravity = -20;
    //float playerGravity = 0;       //플레이어 중력적용에 필요한 변수
    ////점수횟수
    //int jumpCount;

    ////최대 점프횟수
    //public int maxJumpCount = 2;

    ////속력
    //public float speed = 6;
    //public float runspeed = 10;
    //public float moveSpeed = 6.0f;        //이동 속도
    //public float backmoveSpeed = 5.0f;    //뒤로가는 속도


    //Vector3 moveDir = Vector3.zero; //플레이어 이동방향

    //CharacterController playerController;

    //public Animator anim;

    //// Start is called before the first frame update
    //void Start()
    //{

    //    cc = gameObject.GetComponent<CharacterController>();

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    //if(input.GetkeyDown(KeyCode.W))
    //    //{
    //    //    //방향을 왼쪽
    //    //}
    //    //if(Input .GetKeyUp .A)
    //    //{
    //    //    //왼쪽으로 방향을 없애고
    //    //}

    //    //A.D좌우 
    //    float h = Input.GetAxis("Horizontal");
    //    //W.S 앞뒤
    //    float v = Input.GetAxis("Vertical");

    //    anim.SetFloat("Speed", v * v + h * h);

    //    //방향을 정하고
    //    Vector3 dirH = transform.right * h;
    //    Vector3 dirV = transform.forward * v;
    //    Vector3 dir = dirH + dirV;
    //    dir.Normalize();

    //    Jump(out dir.y);

    //    //위 방향으로 움직이게 만드세요. P = P0+VT
    //    cc.Move(dir * speed * Time.deltaTime);

    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        speed = runspeed;

    //    }
    //    else
    //    {
    //        speed = moveSpeed;
    //    }

    //}
    //void Jump(out float dirY)
    //{

    //    //만약에 스페이스바를 ("Jump")를 누르면
    //    if (Input.GetButtonDown("Jump"))       //|| Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //점프 횟수가 최대횟수 점프 보다 작으면
    //        if (jumpCount < 1)
    //        {
    //            //y속도를 jumpPower로 한다. 
    //            yVelocity = jumpPower;
    //            jumpCount++;  //2

    //        }
    //        jumpCount = 0;
    //    }

    //    //dirY에 y속도를 넣는다. 
    //    dirY = yVelocity;    //1.0.0 -> 1,-1,0
    //    //y속도를 중력만큼 더해준다. 
    //    yVelocity += gravity * Time.deltaTime;

    //}
    #endregion

    public Animator anim;

    public float speed = 6.0F;
    public float walkSpeed = 6f;
    public float runSpeed = 10;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    //플레이어 체력 변수 
    public float hp = 100;

    //최대 체력 변수 
    int maxHP = 20;

    //hp 슬라이더 변수 
    public Slider hpSlider;

    //hit 효과 오브젝트
    public GameObject hiteffect;

    public void DamangeAction(int damage)
    {

        //에너미 공격력만큼 플레이어의 체력을 깍는다 
        hp -= damage;
        //만일 플레이어의 체력이 0보다 크면 피격효과를 출력한다. 
        if (hp>0)
        {
            //피격 이펙트 코르틴을 시작한다. 
            StartCoroutine(PlayHitEffect());
        }
        IEnumerator PlayHitEffect()
        {
            //피격 UI를 활성화 한다.
            hiteffect.SetActive(true);

            //0.3초간 대기한다.
            yield return new WaitForSeconds(0.3f);

            //피격 UI를 비활성화 한다. 
            hiteffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource eft_player = GetComponent<AudioSource>();
        AudioClip eft_sound = eft_player.clip;
        if(other.tag == "Weapon")
        {
            eft_player.PlayOneShot(eft_sound);
            DamangeAction(5);
            print(hp);
        }
    }

    void Update()
    {

        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //현재 플레이ㅓ hp(%)를 hp 슬라이더의 value 에 반영한다. 
        hpSlider.value = (float)hp / (float)maxHP;

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);

            anim.SetFloat("Speed", v * v + h * h);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;

            }
            else
            {
                speed = walkSpeed;
            }

            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

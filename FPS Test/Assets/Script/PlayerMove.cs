using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //이동속도 변수
    public float moveSpeed = 7f;
    //캐릭터 콘트롤러 변수
    CharacterController cc;

    //중력변수 
    public float gravit = -20;
    float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //w.a.s.d키를 누르면 입력하여 캐릭터를 그 방향으로 이동시키고 싶다.

        //1.사용자 입력을 받는다. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. 방향을 정하고
        //Vector3 dirH = transform.right * h;
        //Vector3 dirV = transform.forward * v;
        //Vector3 dir = dirH + dirV;
        //dir.Normalize();

        Vector3 dir = new Vector3(h, 0, v);

        //2-1 메인카메를 기준으로 방향을 변환한다. 
        dir = Camera.main.transform.TransformDirection(dir);

        // v = v0 + at
        yVelocity += gravit * Time.deltaTime;

        // 만약 바닥에 있다면
        if (cc.isGrounded)
        {
            // 수직속도는 0으로 만들어줘야 한다.
            yVelocity = 0;
        }


        dir.y = yVelocity;
        //3. 이동속도를 맞춰이동한다. 
        //p=p0+vt
        //transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}

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
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        //2-1 메인카메를 기준으로 방향을 변환한다. 
        dir = Camera.main.transform.TransformDirection(dir);

        //3. 이동속도를 맞춰이동한다. 
        //p=p0+vt
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 칼휘두르기
// -> 3개 액션중 랜덤으로 1개 선택해서 그녀석 재생하게 하기
public class PlayerFire : MonoBehaviour
{
    public GameObject gunObj;
    public GameObject swordObj;

    //총알공장
    public GameObject bulletFactory;
    //총구
    public Transform firePos;

    //파편효과
    public GameObject fragmentEft;
    public GameObject ParticleSystem;

    public Animator anim;
    public Animator fireAnim;

    public AnimatorOverrideController gunAnimController;
    public AnimatorOverrideController swordAnimController;

    public float flashTime = 0.01f;
    float currentTime = 0;
    public GameObject[] flashes;
    int curFlash = 0;
    bool isFiring = false;

    //무기 아이콘 스프라이트 변수 
    public GameObject weapon01;
    public GameObject weapon02;

    //Crosshair 구현
    public GameObject Crosshair;

    //
    // Start is called before the first frame update
    void Start()
    {
        fireAnim.speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunObj.SetActive(true);
            swordObj.SetActive(false);
            anim.runtimeAnimatorController = gunAnimController;

            //1번 스프라이트는 활성화 되고 2번 스프라이트는 비활성화 된다. 
            weapon01.SetActive(true);
            weapon02.SetActive(false);

            //1번 눌렀을 때 Crosshair 활성화되고
            Crosshair.SetActive(true);

        }
        if(Input .GetKeyDown(KeyCode.Alpha2))
        {
            swordObj.SetActive(true);
            gunObj.SetActive(false);
            anim.runtimeAnimatorController = swordAnimController;

            //1번 스프라이트는 비활성화 되고, 2번 스프라이트는 비활성화 된다
            weapon01.SetActive(false);
            weapon02.SetActive(true);

            //2번 눌렀을 때 Crosshair 비활성화된다
            Crosshair.SetActive(false);
        }

        //만약에 fire1 버트을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }

        //만약에 fire2 버튼을 누르면 (마우스 오른쪽, 왼쪽 alt)
        //칼을 들고 있을 때 동작하지 않는다. 
        if (gunObj.activeSelf && Input.GetButtonDown("Fire2"))
        {
            isFiring = true;

            fireAnim.SetTrigger("Fire");
            //카메라위치, 카메라 앞 방향으로 발사되는 Ray를 만든다. 
            Ray ray = new Ray(
               Camera.main.transform.position,
               Camera.main.transform.forward);
            //맞은 위치의 정보
            RaycastHit hitInfo;

            //Ray에 충돌하고 싶은 layer
            int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
            int layerwall = 1 << LayerMask.NameToLayer("Wall");
            int layer = 1 << LayerMask.NameToLayer("Player");

            //Ray를 발사시켜서 어딘가에 부딪혔다면
            if (Physics.Raycast(ray, out hitInfo, 100, ~layer))
            {
                
                //만든 효과를 맞은위치에 놓는다.
                fragmentEft.transform.position = hitInfo.point;

                //만든효과이 앞방향으르 부딪힌 면의 수직맥터(Normal백터)로 한다. 
                fragmentEft.transform.forward = hitInfo.normal;
                
                //맞은 효과에서 ParticleSystem컴포넌트 가져오자
                ParticleSystem ps = fragmentEft.GetComponent<ParticleSystem>();

                //가져온 컴포넌트의 기능중 Play실행
               ps.Play();

                ////맞은 녀석이 Enmey라면
                //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                //if (enemy)
                //{
                //    //Enemy한테 너 맞았어.라고 알려주고 싶다. 
                //    enemy.OnDamageProcess(ray.direction);
                //}


                //AudioSource컴포넌트 가져오자
                AudioSource audio = fragmentEft.GetComponent<AudioSource>();

                //가져온 컴포넌트의 기능중 Play 실행
                audio.Play();

                // 맞은 녀석이 enemy라면
                enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
                if (enemy) //참이면
                {
                    // enemy에게 맞았다는 것을 알려준다
                    enemy.OnDamageProcess(ray.direction);
                }

            }

            if (isFiring)
            {
                currentTime += Time.deltaTime;
                if (currentTime > flashTime)
                {
                    GameObject flash = Instantiate(flashes[curFlash]);
                    flash.transform.position = firePos.position;
                    curFlash++;
                    // 만약 flash 가 다 생성됐으면 제거
                    if (curFlash >= flashes.Length)
                    {
                        curFlash = 0;
                        isFiring = false;
                    }
                }
            }

        }
    }




    void SwordFire()
    {
        // 칼휘두르기
        // -> 3개 액션중 랜덤으로 1개 선택해서 그녀석 재생하게 하기
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                anim.SetTrigger("Attack1");
                break;
            case 1:
                anim.SetTrigger("Attack2");
                break;
            case 2:
                anim.SetTrigger("Attack3");
                break;

        }


    }

    //void GunFire()
    //{
    //    anim.Play("Idle");
    //    // 이동중일때는 총 발사 안되도록
    //    fireAnim.SetTrigger("Fire");

    //    //gun.Play();
    //    //총알공장에서 총알을 놓아둔다. 
    //    GameObject bullet = Instantiate(bulletFactory);
    //    //만들어진 총알의 앞방향을 총구에 앞방향으로 셋팅
    //    bullet.transform.forward = firePos.forward; //만약에 파란 화살표가 자신쪽으로 향해 있을땐 forward에 -를 붙인다. 
    //                                                //생성된 총알을 총구에 놓는다.
    //    bullet.transform.position = firePos.position;
    //}
}



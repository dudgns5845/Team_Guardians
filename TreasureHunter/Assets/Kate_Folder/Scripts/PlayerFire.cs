using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 칼휘?�르�?
// -> 3�??�션�??�덤?�로 1�??�택?�서 그�????�생?�게 ?�기
public class PlayerFire : MonoBehaviour
{
    public GameObject gunObj;
    public GameObject swordObj;

    //총알공장
    public GameObject bulletFactory;
    //총구
    public Transform firePos;

    //?�편?�과
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

    //무기 ?�이�??�프?�이??변??
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
        //게임 ?�태가 '게임�? ?�태?�때�?조작?????�게 ?�다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunObj.SetActive(true);
            swordObj.SetActive(false);
            anim.runtimeAnimatorController = gunAnimController;

            //1�??�프?�이?�는 ?�성???�고 2�??�프?�이?�는 비활?�화 ?�다. 
            weapon01.SetActive(true);
            weapon02.SetActive(false);

            //1�??��?????Crosshair ?�성?�되�?
            Crosshair.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swordObj.SetActive(true);
            gunObj.SetActive(false);
            anim.runtimeAnimatorController = swordAnimController;

            //1�??�프?�이?�는 비활?�화 ?�고, 2�??�프?�이?�는 비활?�화 ?�다
            weapon01.SetActive(false);
            weapon02.SetActive(true);

            //2�??��?????Crosshair 비활?�화?�다
            Crosshair.SetActive(false);
        }

        //만약??fire1 버트???�르�?
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }

        //만약??fire2 버튼???�르�?(마우???�른�? ?�쪽 alt)
        //칼을 ?�고 ?�을 ???�작?��? ?�는?? 
        if (gunObj.activeSelf && Input.GetButtonDown("Fire2"))
        {
            isFiring = true;

            fireAnim.SetTrigger("Fire");
            //카메?�위�? 카메????방향?�로 발사?�는 Ray�?만든?? 
            Ray ray = new Ray(
               Camera.main.transform.position,
               Camera.main.transform.forward);
            //맞�? ?�치???�보
            RaycastHit hitInfo;

            //Ray??충돌?�고 ?��? layer
            int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
            int layerwall = 1 << LayerMask.NameToLayer("Wall");
            int layer = 1 << LayerMask.NameToLayer("Player");

            //Ray�?발사?�켜???�딘가??부?�혔?�면
            if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
            {

                //만든 ?�과�?맞�??�치???�는??
                fragmentEft.transform.position = hitInfo.point;

                //만든?�과???�방?�으�?부?�힌 면의 ?�직맥터(Normal백터)�??�다. 
                fragmentEft.transform.forward = hitInfo.normal;

                //맞�? ?�과?�서 ParticleSystem컴포?�트 가?�오??
                ParticleSystem ps = fragmentEft.GetComponent<ParticleSystem>();

                //가?�온 컴포?�트??기능�?Play?�행
                ps.Play();

                ////맞�? ?�?�이 Enmey?�면
                //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                //if (enemy)
                //{
                //    //Enemy?�테 ??맞았???�고 ?�려주고 ?�다. 
                //    enemy.OnDamageProcess(ray.direction);
                //}


                //AudioSource컴포?�트 가?�오??
                AudioSource audio = fragmentEft.GetComponent<AudioSource>();

                //가?�온 컴포?�트??기능�?Play ?�행
                audio.Play();

                // 맞�? ?�?�이 enemy?�면
                enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
                if (enemy) //참이�?
                {
                    // enemy?�게 맞았?�는 것을 ?�려준??
                    enemy.OnDamageProcess(ray.direction);
                }

                // 맞�? ?�?�이 enemy?�면
                Rikayon crab = hitInfo.transform.GetComponentInParent<Rikayon>();
                if (crab) //참이�?
                {
                    // enemy?�게 맞았?�는 것을 ?�려준??
                    crab.OnDamageProcess(ray.direction);
                }

                // 맞�? ?�?�이 enemy?�면
                Boss_Rio boss = hitInfo.transform.GetComponentInParent<Boss_Rio>();
                if (boss) //참이�?
                {
                    // enemy?�게 맞았?�는 것을 ?�려준??
                    boss.OnDamageProcess(ray.direction);
                }

                // ���� �༮�� enemy���
                FinalStageEnemy_Crab_Rio crab_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
                if (crab_1) //���̸�
                {
                    // enemy���� �¾Ҵٴ� ���� �˷��ش�
                    crab_1.OnDamageProcess(ray.direction);
                }

                FinalStageEnemy_Skull_Rio skull_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
                if (skull_1) //���̸�
                {
                    // enemy���� �¾Ҵٴ� ���� �˷��ش�
                    skull_1.OnDamageProcess(ray.direction);
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
                    // 만약 flash 가 ???�성?�으�??�거
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
        // 칼휘?�르�?
        // -> 3�??�션�??�덤?�로 1�??�택?�서 그�????�생?�게 ?�기
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
    //    // ?�동중일?�는 �?발사 ?�되?�록
    //    fireAnim.SetTrigger("Fire");

    //    //gun.Play();
    //    //총알공장?�서 총알???�아?�다. 
    //    GameObject bullet = Instantiate(bulletFactory);
    //    //만들?�진 총알???�방?�을 총구???�방?�으�??�팅
    //    bullet.transform.forward = firePos.forward; //만약???��? ?�살?��? ?�신쪽으�??�해 ?�을??forward??-�?붙인?? 
    //                                                //?�성??총알??총구???�는??
    //    bullet.transform.position = firePos.position;
    //}
}


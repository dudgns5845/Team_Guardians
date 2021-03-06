using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ์นผํ?๋ฅด๊ธ?
// -> 3๊ฐ??ก์์ค??๋ค?ผ๋ก 1๊ฐ?? ํ?ด์ ๊ทธ๋????ฌ์?๊ฒ ?๊ธฐ
public class PlayerFire : MonoBehaviour
{
    public GameObject gunObj;
    public GameObject swordObj;

    //์ด์๊ณต์ฅ
    public GameObject bulletFactory;
    //์ด๊ตฌ
    public Transform firePos;

    //?ํธ?จ๊ณผ
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

    //๋ฌด๊ธฐ ?์ด์ฝ??คํ?ผ์ด??๋ณ??
    public GameObject weapon01;
    public GameObject weapon02;

    //Crosshair ๊ตฌํ
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
        //๊ฒ์ ?ํ๊ฐ '๊ฒ์์ค? ?ํ?ผ๋๋ง?์กฐ์?????๊ฒ ?๋ค. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunObj.SetActive(true);
            swordObj.SetActive(false);
            anim.runtimeAnimatorController = gunAnimController;

            //1๋ฒ??คํ?ผ์ด?ธ๋ ?์ฑ???๊ณ  2๋ฒ??คํ?ผ์ด?ธ๋ ๋นํ?ฑํ ?๋ค. 
            weapon01.SetActive(true);
            weapon02.SetActive(false);

            //1๋ฒ??๋?????Crosshair ?์ฑ?๋๊ณ?
            Crosshair.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swordObj.SetActive(true);
            gunObj.SetActive(false);
            anim.runtimeAnimatorController = swordAnimController;

            //1๋ฒ??คํ?ผ์ด?ธ๋ ๋นํ?ฑํ ?๊ณ , 2๋ฒ??คํ?ผ์ด?ธ๋ ๋นํ?ฑํ ?๋ค
            weapon01.SetActive(false);
            weapon02.SetActive(true);

            //2๋ฒ??๋?????Crosshair ๋นํ?ฑํ?๋ค
            Crosshair.SetActive(false);
        }

        //๋ง์ฝ??fire1 ๋ฒํธ???๋ฅด๋ฉ?
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }

        //๋ง์ฝ??fire2 ๋ฒํผ???๋ฅด๋ฉ?(๋ง์ฐ???ค๋ฅธ์ช? ?ผ์ชฝ alt)
        //์นผ์ ?ค๊ณ  ?์ ???์?์? ?๋?? 
        if (gunObj.activeSelf && Input.GetButtonDown("Fire2"))
        {
            isFiring = true;

            fireAnim.SetTrigger("Fire");
            //์นด๋ฉ?ผ์์น? ์นด๋ฉ????๋ฐฉํฅ?ผ๋ก ๋ฐ์ฌ?๋ Ray๋ฅ?๋ง๋ ?? 
            Ray ray = new Ray(
               Camera.main.transform.position,
               Camera.main.transform.forward);
            //๋ง์? ?์น???๋ณด
            RaycastHit hitInfo;

            //Ray??์ถฉ๋?๊ณ  ?ถ์? layer
            int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
            int layerwall = 1 << LayerMask.NameToLayer("Wall");
            int layer = 1 << LayerMask.NameToLayer("Player");

            //Ray๋ฅ?๋ฐ์ฌ?์ผ???ด๋๊ฐ??๋ถ?ชํ?ค๋ฉด
            if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
            {

                //๋ง๋  ?จ๊ณผ๋ฅ?๋ง์??์น???๋??
                fragmentEft.transform.position = hitInfo.point;

                //๋ง๋ ?จ๊ณผ???๋ฐฉ?ฅ์ผ๋ฅ?๋ถ?ชํ ๋ฉด์ ?์ง๋งฅํฐ(Normal๋ฐฑํฐ)๋ก??๋ค. 
                fragmentEft.transform.forward = hitInfo.normal;

                //๋ง์? ?จ๊ณผ?์ ParticleSystem์ปดํฌ?ํธ ๊ฐ?ธ์ค??
                ParticleSystem ps = fragmentEft.GetComponent<ParticleSystem>();

                //๊ฐ?ธ์จ ์ปดํฌ?ํธ??๊ธฐ๋ฅ์ค?Play?คํ
                ps.Play();

                ////๋ง์? ??์ด Enmey?ผ๋ฉด
                //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                //if (enemy)
                //{
                //    //Enemy?ํ ??๋ง์???ผ๊ณ  ?๋ ค์ฃผ๊ณ  ?ถ๋ค. 
                //    enemy.OnDamageProcess(ray.direction);
                //}


                //AudioSource์ปดํฌ?ํธ ๊ฐ?ธ์ค??
                AudioSource audio = fragmentEft.GetComponent<AudioSource>();

                //๊ฐ?ธ์จ ์ปดํฌ?ํธ??๊ธฐ๋ฅ์ค?Play ?คํ
                audio.Play();

                // ๋ง์? ??์ด enemy?ผ๋ฉด
                enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
                if (enemy) //์ฐธ์ด๋ฉ?
                {
                    // enemy?๊ฒ ๋ง์?ค๋ ๊ฒ์ ?๋ ค์ค??
                    enemy.OnDamageProcess(ray.direction);
                }

                // ๋ง์? ??์ด enemy?ผ๋ฉด
                Rikayon crab = hitInfo.transform.GetComponentInParent<Rikayon>();
                if (crab) //์ฐธ์ด๋ฉ?
                {
                    // enemy?๊ฒ ๋ง์?ค๋ ๊ฒ์ ?๋ ค์ค??
                    crab.OnDamageProcess(ray.direction);
                }

                // ๋ง์? ??์ด enemy?ผ๋ฉด
                Boss_Rio boss = hitInfo.transform.GetComponentInParent<Boss_Rio>();
                if (boss) //์ฐธ์ด๋ฉ?
                {
                    // enemy?๊ฒ ๋ง์?ค๋ ๊ฒ์ ?๋ ค์ค??
                    boss.OnDamageProcess(ray.direction);
                }

                // ธยภบ ณเผฎภฬ enemyถ๓ธ้
                FinalStageEnemy_Crab_Rio crab_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
                if (crab_1) //ย?ภฬธ้
                {
                    // enemyฟกฐิ ธยพาดูดย ฐอภป พหทมมุดู
                    crab_1.OnDamageProcess(ray.direction);
                }

                FinalStageEnemy_Skull_Rio skull_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
                if (skull_1) //ย?ภฬธ้
                {
                    // enemyฟกฐิ ธยพาดูดย ฐอภป พหทมมุดู
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
                    // ๋ง์ฝ flash ๊ฐ ???์ฑ?์ผ๋ฉ??๊ฑฐ
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
        // ์นผํ?๋ฅด๊ธ?
        // -> 3๊ฐ??ก์์ค??๋ค?ผ๋ก 1๊ฐ?? ํ?ด์ ๊ทธ๋????ฌ์?๊ฒ ?๊ธฐ
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
    //    // ?ด๋์ค์ผ?๋ ์ด?๋ฐ์ฌ ?๋?๋ก
    //    fireAnim.SetTrigger("Fire");

    //    //gun.Play();
    //    //์ด์๊ณต์ฅ?์ ์ด์???์?๋ค. 
    //    GameObject bullet = Instantiate(bulletFactory);
    //    //๋ง๋ค?ด์ง ์ด์???๋ฐฉ?ฅ์ ์ด๊ตฌ???๋ฐฉ?ฅ์ผ๋ก??ํ
    //    bullet.transform.forward = firePos.forward; //๋ง์ฝ???๋? ?์ด?๊? ?์ ์ชฝ์ผ๋ก??ฅํด ?์??forward??-๋ฅ?๋ถ์ธ?? 
    //                                                //?์ฑ??์ด์??์ด๊ตฌ???๋??
    //    bullet.transform.position = firePos.position;
    //}
}


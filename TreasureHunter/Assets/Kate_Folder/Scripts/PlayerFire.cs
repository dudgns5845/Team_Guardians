using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ì¹¼íœ˜?ë¥´ê¸?
// -> 3ê°??¡ì…˜ì¤??œë¤?¼ë¡œ 1ê°?? íƒ?´ì„œ ê·¸ë????¬ìƒ?˜ê²Œ ?˜ê¸°
public class PlayerFire : MonoBehaviour
{
    public GameObject gunObj;
    public GameObject swordObj;

    //ì´ì•Œê³µì¥
    public GameObject bulletFactory;
    //ì´êµ¬
    public Transform firePos;

    //?Œí¸?¨ê³¼
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

    //ë¬´ê¸° ?„ì´ì½??¤í”„?¼ì´??ë³€??
    public GameObject weapon01;
    public GameObject weapon02;

    //Crosshair êµ¬í˜„
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
        //ê²Œì„ ?íƒœê°€ 'ê²Œì„ì¤? ?íƒœ?¼ë•Œë§?ì¡°ì‘?????ˆê²Œ ?œë‹¤. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunObj.SetActive(true);
            swordObj.SetActive(false);
            anim.runtimeAnimatorController = gunAnimController;

            //1ë²??¤í”„?¼ì´?¸ëŠ” ?œì„±???˜ê³  2ë²??¤í”„?¼ì´?¸ëŠ” ë¹„í™œ?±í™” ?œë‹¤. 
            weapon01.SetActive(true);
            weapon02.SetActive(false);

            //1ë²??Œë?????Crosshair ?œì„±?”ë˜ê³?
            Crosshair.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swordObj.SetActive(true);
            gunObj.SetActive(false);
            anim.runtimeAnimatorController = swordAnimController;

            //1ë²??¤í”„?¼ì´?¸ëŠ” ë¹„í™œ?±í™” ?˜ê³ , 2ë²??¤í”„?¼ì´?¸ëŠ” ë¹„í™œ?±í™” ?œë‹¤
            weapon01.SetActive(false);
            weapon02.SetActive(true);

            //2ë²??Œë?????Crosshair ë¹„í™œ?±í™”?œë‹¤
            Crosshair.SetActive(false);
        }

        //ë§Œì•½??fire1 ë²„íŠ¸???„ë¥´ë©?
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }

        //ë§Œì•½??fire2 ë²„íŠ¼???„ë¥´ë©?(ë§ˆìš°???¤ë¥¸ìª? ?¼ìª½ alt)
        //ì¹¼ì„ ?¤ê³  ?ˆì„ ???™ì‘?˜ì? ?ŠëŠ”?? 
        if (gunObj.activeSelf && Input.GetButtonDown("Fire2"))
        {
            isFiring = true;

            fireAnim.SetTrigger("Fire");
            //ì¹´ë©”?¼ìœ„ì¹? ì¹´ë©”????ë°©í–¥?¼ë¡œ ë°œì‚¬?˜ëŠ” Rayë¥?ë§Œë“ ?? 
            Ray ray = new Ray(
               Camera.main.transform.position,
               Camera.main.transform.forward);
            //ë§ì? ?„ì¹˜???•ë³´
            RaycastHit hitInfo;

            //Ray??ì¶©ëŒ?˜ê³  ?¶ì? layer
            int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
            int layerwall = 1 << LayerMask.NameToLayer("Wall");
            int layer = 1 << LayerMask.NameToLayer("Player");

            //Rayë¥?ë°œì‚¬?œì¼œ???´ë”˜ê°€??ë¶€?ªí˜”?¤ë©´
            if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
            {

                //ë§Œë“  ?¨ê³¼ë¥?ë§ì??„ì¹˜???“ëŠ”??
                fragmentEft.transform.position = hitInfo.point;

                //ë§Œë“ ?¨ê³¼???ë°©?¥ìœ¼ë¥?ë¶€?ªíŒ ë©´ì˜ ?˜ì§ë§¥í„°(Normalë°±í„°)ë¡??œë‹¤. 
                fragmentEft.transform.forward = hitInfo.normal;

                //ë§ì? ?¨ê³¼?ì„œ ParticleSystemì»´í¬?ŒíŠ¸ ê°€?¸ì˜¤??
                ParticleSystem ps = fragmentEft.GetComponent<ParticleSystem>();

                //ê°€?¸ì˜¨ ì»´í¬?ŒíŠ¸??ê¸°ëŠ¥ì¤?Play?¤í–‰
                ps.Play();

                ////ë§ì? ?€?ì´ Enmey?¼ë©´
                //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                //if (enemy)
                //{
                //    //Enemy?œí…Œ ??ë§ì•˜???¼ê³  ?Œë ¤ì£¼ê³  ?¶ë‹¤. 
                //    enemy.OnDamageProcess(ray.direction);
                //}


                //AudioSourceì»´í¬?ŒíŠ¸ ê°€?¸ì˜¤??
                AudioSource audio = fragmentEft.GetComponent<AudioSource>();

                //ê°€?¸ì˜¨ ì»´í¬?ŒíŠ¸??ê¸°ëŠ¥ì¤?Play ?¤í–‰
                audio.Play();

                // ë§ì? ?€?ì´ enemy?¼ë©´
                enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
                if (enemy) //ì°¸ì´ë©?
                {
                    // enemy?ê²Œ ë§ì•˜?¤ëŠ” ê²ƒì„ ?Œë ¤ì¤€??
                    enemy.OnDamageProcess(ray.direction);
                }

                // ë§ì? ?€?ì´ enemy?¼ë©´
                Rikayon crab = hitInfo.transform.GetComponentInParent<Rikayon>();
                if (crab) //ì°¸ì´ë©?
                {
                    // enemy?ê²Œ ë§ì•˜?¤ëŠ” ê²ƒì„ ?Œë ¤ì¤€??
                    crab.OnDamageProcess(ray.direction);
                }

                // ë§ì? ?€?ì´ enemy?¼ë©´
                Boss_Rio boss = hitInfo.transform.GetComponentInParent<Boss_Rio>();
                if (boss) //ì°¸ì´ë©?
                {
                    // enemy?ê²Œ ë§ì•˜?¤ëŠ” ê²ƒì„ ?Œë ¤ì¤€??
                    boss.OnDamageProcess(ray.direction);
                }

                // ¸ÂÀº ³à¼®ÀÌ enemy¶ó¸é
                FinalStageEnemy_Crab_Rio crab_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Crab_Rio>();
                if (crab_1) //ÂüÀÌ¸é
                {
                    // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
                    crab_1.OnDamageProcess(ray.direction);
                }

                FinalStageEnemy_Skull_Rio skull_1 = hitInfo.transform.GetComponentInParent<FinalStageEnemy_Skull_Rio>();
                if (skull_1) //ÂüÀÌ¸é
                {
                    // enemy¿¡°Ô ¸Â¾Ò´Ù´Â °ÍÀ» ¾Ë·ÁÁØ´Ù
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
                    // ë§Œì•½ flash ê°€ ???ì„±?ìœ¼ë©??œê±°
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
        // ì¹¼íœ˜?ë¥´ê¸?
        // -> 3ê°??¡ì…˜ì¤??œë¤?¼ë¡œ 1ê°?? íƒ?´ì„œ ê·¸ë????¬ìƒ?˜ê²Œ ?˜ê¸°
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
    //    // ?´ë™ì¤‘ì¼?ŒëŠ” ì´?ë°œì‚¬ ?ˆë˜?„ë¡
    //    fireAnim.SetTrigger("Fire");

    //    //gun.Play();
    //    //ì´ì•Œê³µì¥?ì„œ ì´ì•Œ???“ì•„?”ë‹¤. 
    //    GameObject bullet = Instantiate(bulletFactory);
    //    //ë§Œë“¤?´ì§„ ì´ì•Œ???ë°©?¥ì„ ì´êµ¬???ë°©?¥ìœ¼ë¡??‹íŒ…
    //    bullet.transform.forward = firePos.forward; //ë§Œì•½???Œë? ?”ì‚´?œê? ?ì‹ ìª½ìœ¼ë¡??¥í•´ ?ˆì„??forward??-ë¥?ë¶™ì¸?? 
    //                                                //?ì„±??ì´ì•Œ??ì´êµ¬???“ëŠ”??
    //    bullet.transform.position = firePos.position;
    //}
}


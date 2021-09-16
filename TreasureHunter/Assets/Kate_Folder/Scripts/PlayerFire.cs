using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Į�ֵθ���
// -> 3�� �׼��� �������� 1�� �����ؼ� �׳༮ �����ϰ� �ϱ�
public class PlayerFire : MonoBehaviour
{

   public Image magazineImg;
    public Text magazineText;

    private AudioSource _audio;

    public int maxBullet = 10;
    public int remainingBullet = 10;

    //public float reloadTime = 2.0f;
    //private bool isReloading = false;

    public GameObject gunObj;
    public GameObject swordObj;

    //�Ѿ˰���
    public GameObject bulletFactory;
    //�ѱ�
    public Transform firePos;

    //����ȿ��
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

    //���� ������ ��������Ʈ ����
    public GameObject weapon01;
    public GameObject weapon02;

    //Crosshair ����
    public GameObject Crosshair;





    // Start is called before the first frame update
    void Start()
    {
        fireAnim.speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //���콺 ���� ��ư�� Ŭ������ �� fire �Լ� ȣ��
        //if (!isReloading && Input.GetMouseButtonDown(0))
        //{
        //    --remainingBullet;
        //    Fire();
        //}

        //���� �Ѿ��� ���� ���� ������ �ڷ�ƾ ȣ��
        //if (remainingBullet == 0)
        //{
        //    StartCoroutine(Reloading());
        //}

        //���� ���°� '������' �����϶��� ������ �� �ְ� �Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunObj.SetActive(true);
            swordObj.SetActive(false);
            anim.runtimeAnimatorController = gunAnimController;

            //1�� ��������Ʈ�� Ȱ��ȭ �ǰ� 2�� ��������Ʈ�� ��Ȱ��ȭ �ȴ�.
            weapon01.SetActive(true);
            weapon02.SetActive(false);

            //1�� ������ �� Crosshair Ȱ��ȭ�ǰ�
            Crosshair.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swordObj.SetActive(true);
            gunObj.SetActive(false);
            anim.runtimeAnimatorController = swordAnimController;

            //1�� ��������Ʈ�� ��Ȱ��ȭ �ǰ�, 2�� ��������Ʈ�� ��Ȱ��ȭ �ȴ�
            weapon01.SetActive(false);
            weapon02.SetActive(true);

            //2�� ������ �� Crosshair ��Ȱ��ȭ�ȴ�
            Crosshair.SetActive(false);
        }

        //���࿡ fire1 ��Ʈ�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }


            magazineImg.fillAmount = (float)remainingBullet / (float)maxBullet;
            UpdateBulletText();

            //���࿡ fire2 ��ư�� ������ (���콺 ������, ���� alt)
            //Į�� ���� ���� �� �������� �ʴ´�.
            if (gunObj.activeSelf && Input.GetButtonDown("Fire2"))
            {
                isFiring = true;

                fireAnim.SetTrigger("Fire");
                //ī�޶���ġ, ī�޶� �� �������� �߻��Ǵ� Ray�� ������.
                Ray ray = new Ray(
                   Camera.main.transform.position,
                   Camera.main.transform.forward);
                //���� ��ġ�� ����
                RaycastHit hitInfo;

                //Ray�� �浹�ϰ� ���� layer
                int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
                int layerwall = 1 << LayerMask.NameToLayer("Wall");
                int layer = 1 << LayerMask.NameToLayer("Player");

                //Ray�� �߻����Ѽ� ���򰡿� �ε����ٸ�
                if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
                {

                    //���� ȿ���� ������ġ�� ���´�.
                    fragmentEft.transform.position = hitInfo.point;

                    //����ȿ���� �չ������� �ε��� ���� ��������(Normal����)�� �Ѵ�.
                    fragmentEft.transform.forward = hitInfo.normal;

                    //���� ȿ������ ParticleSystem������Ʈ ��������
                    ParticleSystem ps = fragmentEft.GetComponent<ParticleSystem>();

                    //������ ������Ʈ�� ������ Play����
                    ps.Play();

                    ////���� �༮�� Enmey����
                    //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    //if (enemy)
                    //{
                    //    //Enemy���� �� �¾Ҿ�.���� �˷��ְ� �ʹ�.
                    //    enemy.OnDamageProcess(ray.direction);
                    //}


                    //AudioSource������Ʈ ��������
                    AudioSource audio = fragmentEft.GetComponent<AudioSource>();

                    //������ ������Ʈ�� ������ Play ����
                    audio.Play();

                // ���� �༮�� enemy����
                Boss_Rio boss = hitInfo.transform.GetComponentInParent<Boss_Rio>();
                if (boss) //���̸�
                {
                    // enemy���� �¾Ҵٴ� ���� �˷��ش�
                    boss.OnDamageProcess(ray.direction);
                }

            }

                    // ���� �༮�� enemy����
                    Rikayon crab = hitInfo.transform.GetComponentInParent<Rikayon>();
                    if (crab) //���̸�
                    {
                        // enemy���� �¾Ҵٴ� ���� �˷��ش�
                        crab.OnDamageProcess(ray.direction);
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
                        // ���� flash �� �� ���������� ����
                        if (curFlash >= flashes.Length)
                        {
                            curFlash = 0;
                            isFiring = false;
                        }
                    }
                }

            }
        }


    //IEnumerator Reloading()
    //{
    //   isReloading = true;

    //yield return new WaitForSeconds(0.2f);

    //    isReloading = false;
    //
    //    UpdateBulletText();

    //}

    private void UpdateBulletText()
    {
        magazineImg.fillAmount = 1.0f;
        remainingBullet = maxBullet;
        magazineText.text = string.Format(remainingBullet  + "   /    " + maxBullet);

    }

    void SwordFire()
    {
        // Į�ֵθ���
        // -> 3�� �׼��� �������� 1�� �����ؼ� �׳༮ �����ϰ� �ϱ�
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




        //void GunFire()
        //{
        //    anim.Play("Idle");
        //    // �̵����϶��� �� �߻� �ȵǵ���
        //    fireAnim.SetTrigger("Fire");

        //    //gun.Play();
        //    //�Ѿ˰��忡�� �Ѿ��� ���Ƶд�.
        //    GameObject bullet = Instantiate(bulletFactory);
        //    //�������� �Ѿ��� �չ����� �ѱ��� �չ������� ����
        //    bullet.transform.forward = firePos.forward; //���࿡ �Ķ� ȭ��ǥ�� �ڽ������� ���� ������ forward�� -�� ���δ�.
        //                                                //������ �Ѿ��� �ѱ��� ���´�.
        //    bullet.transform.position = firePos.position;
        //}
    }
}

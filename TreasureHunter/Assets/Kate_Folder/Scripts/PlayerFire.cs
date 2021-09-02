using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Į�ֵθ���
// -> 3�� �׼��� �������� 1�� �����ؼ� �׳༮ ����ϰ� �ϱ�
public class PlayerFire : MonoBehaviour
{
    

    //�Ѿ˰���
    public GameObject bulletFactory;
    //�ѱ�
    public Transform firePos;

    public ParticleSystem gun;

    public Animator anim;
    public Animator fireAnim;

    public AnimatorOverrideController gunAnimController;
    public AnimatorOverrideController swordAnimController;
    // Start is called before the first frame update
    void Start()
    {
        fireAnim.speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            anim.runtimeAnimatorController = swordAnimController;
        }
        //���࿡ fire1 ��Ʈ�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            //GunFire();
            SwordFire();
        }

    }

    void SwordFire()
    {
        // Į�ֵθ���
        // -> 3�� �׼��� �������� 1�� �����ؼ� �׳༮ ����ϰ� �ϱ�
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

    void GunFire()
    {
        anim.Play("Idle");
        // �̵����϶��� �� �߻� �ȵǵ���
        fireAnim.SetTrigger("Fire");

        gun.Play();
        //�Ѿ˰��忡�� �Ѿ��� ���Ƶд�. 
        GameObject bullet = Instantiate(bulletFactory);
        //������� �Ѿ��� �չ����� �ѱ��� �չ������� ����
        bullet.transform.forward = firePos.forward; //���࿡ �Ķ� ȭ��ǥ�� �ڽ������� ���� ������ forward�� -�� ���δ�. 
                                                    //������ �Ѿ��� �ѱ��� ���´�.
        bullet.transform.position = firePos.position;
    }
}

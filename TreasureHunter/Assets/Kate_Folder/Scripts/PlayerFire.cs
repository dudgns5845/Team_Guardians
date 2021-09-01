using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

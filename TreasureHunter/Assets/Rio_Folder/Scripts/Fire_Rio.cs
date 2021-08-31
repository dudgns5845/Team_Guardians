using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ǥ : ray�� �߻��� enemy���� ������ �˸���
/// </summary>
public class Fire_Rio : MonoBehaviour
{
    public Transform FirePos;
    public GameObject fragmentEFT;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            LayFire();
        }
    }

    void LayFire()
    {
        //ray ����
        //ray�� ������ �Ķ���� - ���� ��ġ, ����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //���� ��ġ�� ����
        RaycastHit hitInfo;

        //Ray�� �浹�ϰ� ���� Layer
        //int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
        int layer = 1 << LayerMask.NameToLayer("Player");

        //ray�� �߻���Ѽ� ��򰡿� �ε����ٸ�
        if(Physics.Raycast(ray, out hitInfo, 100, ~layer))
        {
            //��ƼŬ�� ���� ��ġ�� ��ġ
            fragmentEFT.transform.position = hitInfo.point;

            //ȿ���� forward�� �ε��� ���� normal���ͷ� �Ѵ�
            fragmentEFT.transform.forward = hitInfo.normal;

            //���� ȿ���� particleSystem ������Ʈ ��������
            ParticleSystem ps = fragmentEFT.GetComponent<ParticleSystem>();

            //��ƼŬ play
            ps.Play();

            // ���� �༮�� enemy���
            enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
            if (enemy) //���̸�
            {
                // enemy���� �¾Ҵٴ� ���� �˷��ش�
                enemy.OnDamageProcess(ray.direction);
            }
        }

        //��ƼŬ ȿ������ �������
        AudioSource audio = fragmentEFT.GetComponent<AudioSource>();
        audio.Play();
    }
}

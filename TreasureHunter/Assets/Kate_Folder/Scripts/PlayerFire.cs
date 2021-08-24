using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    //�Ѿ˰���
    public GameObject bulletFactory;
    //�ѱ�
    public Transform firePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���࿡ fire1 ��Ʈ�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            //�Ѿ˰��忡�� �Ѿ��� ���Ƶд�. 
            GameObject bullet = Instantiate(bulletFactory);
            //������� �Ѿ��� �չ����� �ѱ��� �չ������� ����
            bullet.transform.forward = firePos.forward; //���࿡ �Ķ� ȭ��ǥ�� �ڽ������� ���� ������ forward�� -�� ���δ�. 
            //������ �Ѿ��� �ѱ��� ���´�.
            bullet.transform.position = firePos.position;
        }

    }
}

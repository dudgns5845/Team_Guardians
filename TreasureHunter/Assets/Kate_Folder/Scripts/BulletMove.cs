using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //�ӵ� 
    public float speed = 7;

    //Rigidbody
    Rigidbody rb;
    //�Ŀ�
    public float power = 1000;
    //���߰���
    public GameObject exploFactory;

    //�ݰ�
    public float exploRange = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody ������Ʈ�� ��������
        rb = GetComponent<Rigidbody>();
        //�Ѿ��� �չ������� ���� �ش�.
        rb.AddForce(transform.forward * power);
    }

    // Update is called once per frame
    void Update()
    {
        //��� �ڱ� �ڽ��� ������ ���ϰ� �Ѵ�. 
        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //���߰��忡�� ����ȿ���� �����. 
        GameObject explo = Instantiate(exploFactory);
        //������� ������ ���� ��ġ�� ���´�. 
        explo.transform.position = transform.position;
        //������� ������ 3�ʵڿ� ����
        Destroy(explo, 3);

        //�ݰ�ȿ� ���� ��ֹ� üũ �� �ı�
        //ObstacleManager.instance.CheckExplo(transform.position, exploRange);

        Collider[] cols = Physics.OverlapSphere(transform.position, exploRange);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Map")
            {
                Destroy(cols[i].gameObject);
            }
        }

        ////���࿡ �ε��� ���� tag�� Obstacle�̸� 
        //if(other.gameObject .tag=="obstacle")
        //{
        //    //�ε����� �ı�

        //    Destroy(other.gameObject);
        //}

        Destroy(gameObject);
    }
}

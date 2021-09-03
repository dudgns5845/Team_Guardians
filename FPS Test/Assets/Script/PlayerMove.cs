using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //�̵��ӵ� ����
    public float moveSpeed = 7f;
    //ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    //�߷º��� 
    public float gravit = -20;
    float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //w.a.s.dŰ�� ������ �Է��Ͽ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�.

        //1.����� �Է��� �޴´�. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. ������ ���ϰ�
        //Vector3 dirH = transform.right * h;
        //Vector3 dirV = transform.forward * v;
        //Vector3 dir = dirH + dirV;
        //dir.Normalize();

        Vector3 dir = new Vector3(h, 0, v);

        //2-1 ����ī�޸� �������� ������ ��ȯ�Ѵ�. 
        dir = Camera.main.transform.TransformDirection(dir);

        // v = v0 + at
        yVelocity += gravit * Time.deltaTime;

        // ���� �ٴڿ� �ִٸ�
        if (cc.isGrounded)
        {
            // �����ӵ��� 0���� �������� �Ѵ�.
            yVelocity = 0;
        }


        dir.y = yVelocity;
        //3. �̵��ӵ��� �����̵��Ѵ�. 
        //p=p0+vt
        //transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}

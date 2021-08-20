using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    //���콺�� ȸ�� ����ġ ������ ����
    float mx;
    float my;

    //ȸ���ӷ�
    float rotSpeed = 150;

    //ȸ�� ���ɿ���
    public bool canRotH;
    public bool canRotV;

    // Start is called before the first frame update
    void Start()
    {
        //���� ���ӿ�����Ʈ�� ������ mx.my�� ����
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        //���콺�� �������� �޾Ƽ�
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //���콺�� ȸ�������� ������ �����ϰ�(����ϰ�)
        if (canRotV == true)
        {
            mx += v * rotSpeed * Time.deltaTime;
        }
        if (canRotH == true)
        {
            my += h * rotSpeed * Time.deltaTime;
        }


        //���࿡ mx�� ���� -60���� ������
        //if (mx < -60) //-60���� �ٽ� ���� mx = -60;

        //���࿡ mx�� ���� 60���� Ŀ����
        //if (mx > 60) //60���� ���� mx = 60;
        //mx�ּҰ��� -60, �ִ밪�� 60���� ����
        mx = Mathf.Clamp(mx, -60, 60);




        //������ ȸ�������� ���ӿ�����Ʈ�� ������ ��������.
        transform.localEulerAngles = new Vector3(-mx, my, 0);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //ȸ�� �ӵ� ���� 
    public float rotSpeed = 200f;

    //ȸ���� ���� 
    float mx = 0;
    float my = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������� ���콺�� �Է��� �޾� ��ü�� ȸ����Ű�� �ʹ�.
        //1. ���콺�� �Է��� �޴´�
        float mouse_X = Input.GetAxis("Mouse_X");
        float mouse_Y = Input.GetAxis("Mouse_Y");

        //1-1 ȸ���� ������ ���콺 �Է°� ��ŭ �̸� ������Ų��. 
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        //1-2 ���콺 ���� �̵� ȸ�� ����(my) �� ���� -90�� 90���̷� �����Ѵ�. 
        my = Mathf.Clamp(my, -90f, 90);

        //2. ȸ�� �������� ��ü�� ȸ����Ų��
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}

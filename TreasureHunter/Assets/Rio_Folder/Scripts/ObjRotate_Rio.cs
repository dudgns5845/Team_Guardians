using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate_Rio : MonoBehaviour
{

    public float rotateSpeed = 150;
    /// <summary>
    /// ���콺�� ȸ�� ����ġ ������ ����
    /// </summary>
    float mouseX;
    float mouseY;

    //ȸ�����ɿ���
    public bool canRotH; // �¿� ȸ�� ����
    public bool canRotV; // ���� ȸ�� ����

    private void Start()
    {
        //���� ���ӿ�����Ʈ�� ������ mouseX,mouseY�� ����
        mouseX = transform.eulerAngles.x;
        mouseY = transform.eulerAngles.y;
        //transform.rotation�� ���� Quaternion�� ��ȯ�ϴµ� �̰��� ��ü ��ȯ�� ���̶�
        //transform.eulerAngles�� ����ؾ��Ѵ�
        //�̴� inspector���� ���̴� roatation�� ���ڰ��� ���� 
    }

    private void Update()
    {
        //���콺 �������� �޾Ƽ�
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        
        //���콺�� ȸ�������� ������ �����ϰ�(����ϰ�) 
        if(canRotV == true)
        {
            mouseX += v * rotateSpeed * Time.deltaTime;
        }
        if(canRotH == true)
        {
            mouseY += h * rotateSpeed * Time.deltaTime;
        }

        //mouseX�� ���� -60~60�� ������ ����
        //if(mouseX < -60)
        //{
        //    mouseX = -60;
        //}
        //if(mouseX > 60)
        //{
        //    mouseX = 60;
        //}
        //���� ������ �Ȱ��� ���
        mouseX = Mathf.Clamp(mouseX, -60, 60);
        //mouseY = Mathf.Clamp(mouseY, -60, 60);


        //������ ȸ�������� ���ӿ�����Ʈ�� ������ ��������
        transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);
    }
}

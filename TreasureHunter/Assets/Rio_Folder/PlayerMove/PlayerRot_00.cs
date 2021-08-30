using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot_00 : MonoBehaviour
{
    //���콺�� ȸ�� ����ġ�� ������ ����
    float mx;
    float my;

    //ȸ���� �ӵ�
    float rotSpeed = 500;

    //ȸ�� ���� ����
    public bool canRotH;
    public bool canRotV;

    // ���� ũ��
    public float amp = 0.07f;
    public float freq = 20;
    float tempY;

    private void Start()
    {
        tempY = transform.localPosition.y;
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;
    }

    void Update()
    {
        //���콺�� �������� �޾Ƽ�
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //���콺�� ȸ�������� ������ �����ϰ�(����ϰ�)
        if (canRotV)
            mx += v * rotSpeed * Time.deltaTime;
        if (canRotH)
            my += h * rotSpeed * Time.deltaTime;

        //mx �ּҰ��� -60, �ִ밪�� 60���� ����
        mx = Mathf.Clamp(mx, -60, 60);
        //������ ȸ�������� ���ӿ�����Ʈ�� ������ ��������
        transform.localEulerAngles = new Vector3(-mx, my, 0);

        // ���� ī�޶��� 
        if (gameObject.tag == "MainCamera")
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                amp = 0.07f;
                freq = 20;
                if (Input.GetKey(KeyCode.LeftShift)) //�޸��� ����
                {
                    print("shift����");
                    amp = 0.07f;
                    freq = 50;
                }
                transform.localPosition = new Vector3(0, tempY + amp * Mathf.Sin(freq * Time.time), 0);
            }
        }
    }
}

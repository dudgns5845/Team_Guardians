using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //��ǥ�� �� Ʈ������ ������Ʈ 
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ī���޶��� ��ġ�� ��ǥ Ʈ�������� ��ġ�� ��ġ��Ų��. 
        transform.position = target.position;
    }
}

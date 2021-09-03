using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    //���� �� �ð� ����
    public float destroyTime = 1.5f;
    //��� �ð� ������ ����
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� ����ð��� ���ŵ� �ð��� �ʰ��ϸ� �ڱ� �ڽ��� �����Ѵ�. 
        if (currentTime > destroyTime)
        {
            Destroy(gameObject);
        }
        //����ð��� �����Ѵ�
        currentTime += Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //속도 
    public float speed = 7;

    //Rigidbody
    Rigidbody rb;
    //파워
    public float power = 1000;
    //폭발공장
    public GameObject exploFactory;

    //반경
    public float exploRange = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody 컨포넌트를 가져오자
        rb = GetComponent<Rigidbody>();
        //총알의 앞방향으로 힘을 준다.
        rb.AddForce(transform.forward * power);
    }

    // Update is called once per frame
    void Update()
    {
        //계속 자기 자신의 앞으로 향하게 한다. 
        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //폭발공장에서 폭발효과를 만든다. 
        GameObject explo = Instantiate(exploFactory);
        //만들어진 폭발을 나의 위치에 놓는다. 
        explo.transform.position = transform.position;
        //만들어진 폭발을 3초뒤에 삭제
        Destroy(explo, 3);

        //반경안에 들어온 장애물 체크 후 파괴
        //ObstacleManager.instance.CheckExplo(transform.position, exploRange);

        Collider[] cols = Physics.OverlapSphere(transform.position, exploRange);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Map")
            {
                Destroy(cols[i].gameObject);
            }
        }

        ////만약에 부딪힌 놈의 tag가 Obstacle이면 
        //if(other.gameObject .tag=="obstacle")
        //{
        //    //부딪힌놈 파괴

        //    Destroy(other.gameObject);
        //}

        Destroy(gameObject);
    }
}

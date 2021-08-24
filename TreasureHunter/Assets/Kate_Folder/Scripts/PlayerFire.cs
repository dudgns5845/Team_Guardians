using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    //총알공장
    public GameObject bulletFactory;
    //총구
    public Transform firePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //만약에 fire1 버트을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            //총알공장에서 총알을 놓아둔다. 
            GameObject bullet = Instantiate(bulletFactory);
            //만들어진 총알의 앞방향을 총구에 앞방향으로 셋팅
            bullet.transform.forward = firePos.forward; //만약에 파란 화살표가 자신쪽으로 향해 있을땐 forward에 -를 붙인다. 
            //생성된 총알을 총구에 놓는다.
            bullet.transform.position = firePos.position;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyMove_Rio : MonoBehaviour
{
    public GameObject target; //따라갈 대상
    public float speed = 2.0f;
    public float rotSpeed=5;
    CharacterController cc;

    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.transform.position - gameObject.transform.position;
        dir.Normalize();
       
        cc.SimpleMove(dir * speed); //점프 못함
        dir.y = 0;
     
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
    }
}

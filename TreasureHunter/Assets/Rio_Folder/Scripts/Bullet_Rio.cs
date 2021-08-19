using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rio : MonoBehaviour
{
    public float speed = 5f;
    public float power = 100;
    Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward* speed * Time.deltaTime;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*power);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

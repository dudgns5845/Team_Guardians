using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Rio : MonoBehaviour
{
    public Transform FirePos;
    public GameObject BulletPrefab;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gunFire();
        }
    }

    void gunFire()
    {
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.forward = FirePos.forward;
        bullet.transform.position = FirePos.position;
        
    }
}

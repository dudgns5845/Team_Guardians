using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCrosshair : MonoBehaviour
{
    public float flashTime = 0.01f;
    float currentTime = 0;
    public GameObject[] flashes;
    int curFlash = 0;
    bool isFiring = false;

    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
            currentTime = flashTime;
        }

        if (isFiring)
        {
            currentTime += Time.deltaTime;
            if (currentTime > flashTime)
            {
                GameObject flash = Instantiate(flashes[curFlash]);
                flash.transform.position = firePos.position;
                curFlash++;
                // 만약 flash 가 다 생성됐으면 제거
                if (curFlash >= flashes.Length)
                {
                    curFlash = 0;
                    isFiring = false;
                }
            }
        }
    }
}

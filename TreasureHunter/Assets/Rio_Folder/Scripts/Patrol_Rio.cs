using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Rio : MonoBehaviour
{
    public Transform[] spot;
    public int speed;
    int spotidx;
    float destination;
  
    void Start()
    {
        spotidx = 0;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

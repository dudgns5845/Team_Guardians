using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_Rio : MonoBehaviour
{
    public Transform target;

    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
   

    void Update()
    {
        nav.SetDestination(target.position);
    }
}

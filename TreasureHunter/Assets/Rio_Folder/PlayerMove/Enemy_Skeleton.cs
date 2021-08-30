using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : MonoBehaviour
{
    public GameObject target;

    enum State
    {
        IDEL,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }

    State m_state = State.IDEL;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case State.IDEL:
                break;
            case State.MOVE:
                break;
            case State.ATTACK:
                break;
            case State.DAMAGE:
                break;
            case State.DIE:
                break;
        }
    }
}

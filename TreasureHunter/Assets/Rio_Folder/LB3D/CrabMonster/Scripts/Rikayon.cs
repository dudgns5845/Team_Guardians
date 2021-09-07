using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rikayon : MonoBehaviour {

    Animator animator;
    GameObject target;

    enum STATE { 
        IDLE,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }

    STATE m_state = STATE.ATTACK;

    // Use this for initialization
    void Start () {
        target = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (m_state)
        {
            case STATE.IDLE:
                Idle();
                break;

            case STATE.MOVE:
                Move();

                break;
            case STATE.ATTACK:
                Attack();
                break;
        }

    }
    //일반상태일때는 player가 일정 반경에 오기 전까지는 
    //지정 구역을 돌아다닌다
    //중간중간 멈춰서 표효하는 애니메이션을 재생하고
    //다시 걷는다
    //그러다 플레이어가 일정 간격에 오면 공격모드로 전환하고
    //따라간다

    

    void Idle()
    {

    }

    void Move()
    {

    }


    public float attackTime = 1.5f;
    float attackCnt = 0;
    void Attack()
    {
        attackCnt += Time.deltaTime;
        if(attackCnt > attackTime)
        {
            int idx = Random.Range(1, 6);
            animator.SetTrigger("Attack_" + idx);
            attackCnt = 0;

        }   
    }

   

}

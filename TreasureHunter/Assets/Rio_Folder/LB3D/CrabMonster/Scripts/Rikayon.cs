using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rikayon : MonoBehaviour
{
    Animator animator;
    GameObject target;
    public List<Transform> spots;
    NavMeshAgent agent;

    enum STATE
    {
        PATROL,//정찰
        MOVE, //따라감
        ATTACK,//공격
        DAMAGE,//부상
        DIE//죽음
    }

    STATE m_state = STATE.PATROL;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = spots[idx].position;
    }

    // Update is called once per frame
    void Update()
    {
        print("현재상태 : " + m_state);
        switch (m_state)
        {
            case STATE.PATROL:
                Patrol();
                break;
            case STATE.MOVE:
                Move();
                break;
            case STATE.ATTACK:
                Attack();
                break;
            case STATE.DIE:
                Die();
                break;
        }

    }


    //일반상태일때는 player가 일정 반경에 오기 전까지는 
    //지정 구역을 돌아다닌다
    //중간중간 멈춰서 표효하는 애니메이션을 재생하고
    //다시 걷는다
    //그러다 플레이어가 일정 간격에 오면 공격모드로 전환하고
    //따라간다
    int idx = 0;
    bool isMove = false;
    public float attackRange = 20f;
    void Patrol()
    {
        if (!isMove)
        {
            animator.SetTrigger("Walk_Cycle_1");
            isMove = true;
        }

        //일정 반경에 오면 attack 시작
        Vector3 dir = target.transform.position - transform.position;
    
        if (dir.magnitude < attackRange)
        {
            m_state = STATE.ATTACK;
            isMove = false;
        }

        if (gameObject.transform.position.x == spots[idx].position.x && gameObject.transform.position.z == spots[idx].position.z)
        {
            if (idx == 5)
            {
                idx = -1;
            }
            idx++;

            agent.destination = spots[idx].position;
        }
    }

    private void Move()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        if (!isMove)
        {
            animator.SetTrigger("Walk_Cycle_1");
            isMove = true;
        }

        //일정 반경에 오면 attack 시작
        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < attackRange)
        {
            m_state = STATE.ATTACK;
            isMove = false;
        }
    }

    public float attackTime = 1.5f;
    float attackCnt = 0;
    void Attack()
    {
        agent.isStopped =true; //위치 이동 잠깐 멈추기
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        
        
        attackCnt += Time.deltaTime;
        if (attackCnt > attackTime)
        {
            print("ATTACK 호출");
            int idx = Random.Range(1, 6);
            animator.SetTrigger("Attack_" + idx);
            attackCnt = 0;

        }

        if(distance > attackRange)
        {
            m_state = STATE.MOVE;
        }
    }

    void Die() { }



}

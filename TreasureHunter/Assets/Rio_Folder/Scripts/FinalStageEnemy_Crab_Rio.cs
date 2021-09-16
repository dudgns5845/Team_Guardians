using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalStageEnemy_Crab_Rio : MonoBehaviour
{
    Animator animator;
    GameObject target;
   
    NavMeshAgent agent;

    enum STATE
    {
       IDLE,//정찰
        MOVE, //따라감
        ATTACK,//공격
        DAMAGE,//부상
        DIE//죽음
    }

    STATE m_state = STATE.IDLE;

    // Use this for initialization
    void Start()
    {
        
        target = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
      
    }

    // Update is called once per frame
    void Update()
    {
        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

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
    int idx;
    bool isMove = false;
    public float attackRange = 40f;
    public float followRange = 100f;
    void Idle()
    {
        if (!isMove)
        {
            animator.SetTrigger("Walk_Cycle_1");
            isMove = true;
        }

        //일정 반경에 오면 attack 시작
        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < followRange)
        {
            agent.stoppingDistance = 80f;
            m_state = STATE.MOVE;
            isMove = false;
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
        agent.isStopped = true; //위치 이동 잠깐 멈추기
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);



        attackCnt += Time.deltaTime;
        if (attackCnt > attackTime)
        {

            int idx = Random.Range(1, 6);
            animator.SetTrigger("Attack_" + idx);
            attackCnt = 0;

        }

        if (distance > attackRange)
        {
            m_state = STATE.MOVE;
        }
    }

    // 일정시간 기다렸다가 상태를 Idle 로 전환하고 싶다.
    // 필요속성 : damage 대기시간
    float damageDelayTime = 0.5f;
    private IEnumerator Damage(Vector3 shootDirection)
    {
        shootDirection.y = 0;
        //transform.position += shootDirection * 2;
        //transform.position = Vector3.Lerp(transform.position, shootDirection * 4, Time.deltaTime);
        m_state = STATE.DAMAGE;
        // animation 의 상태를 피격으로
        animator.SetTrigger("Take_Damage_1");

        // 대기시간 만큼 기다렸다가 
        yield return new WaitForSeconds(damageDelayTime);
        agent.enabled = true;
        // 상태를 Idle 로 전환
        m_state = STATE.MOVE;

    }

    // 플레이어로부터 피격 받았을때 처리할 함수
    Vector3 knockbackPos;
    float currentTime = 0;
    public int hp = 10;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        isMove = false;

        // 죽은 애는 또 피격처리 하고 싶지 않다.
        if (m_state == STATE.DIE)
        {
            return;
        }

        // 코루틴을 종료하고 싶다.
        StopAllCoroutines();

        agent.enabled = false;

        currentTime = 0;
        hp--;
        // 1. hp 가 0 이하면 없애고 싶다.
        if (hp <= 0)
        {
            // 죽으면 충돌체 꺼버리자
            //cc.enabled = false;
            m_state = STATE.DIE;
            animator.SetTrigger("Die");

        }
        // 2. 맞으면 상태를 피격으로 전환하고 싶다.
        else
        {
            // 넉백 knockback
            // 밀릴 방향
            // P = P0 + vt

            // 피격처리를 코루틴을 이용하여 처리하고 싶다.
            StartCoroutine(Damage(shootDirection));

        }
    }

   
    void Die()
    {
       
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_Rio : MonoBehaviour
{
    GameObject target;

    enum EnemyState
    {
        IDLE,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }

    EnemyState m_state = EnemyState.IDLE;

    public int hp = 3;

    Animator anim;

    CharacterController cc;

    NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // 적의 기본 상태(목차)를 구성하고 싶다.
        // 만약 적의 상태가 Idle 이라면
        switch (m_state)
        {
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.MOVE:
                Move();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
            case EnemyState.DIE:
                Die();
                break;
        }
    }

    // 일정시간이 지나면 상태를 Move 로 전환하고 싶다.
    // 필요속성 : 대기시간, 경과시간
    public float idleDelayTime = 2;
    float currentTime;
    private void Idle()
    {
        // 일정시간이 지나면 상태를 Move 로 전환하고 싶다.
        // 1. 시간이 흘렀으니까
        currentTime += Time.deltaTime;
        // 2. 일정시간이 지났으까
        // -> 만약 경과시간이 대기시간을 초과하였다면
        if (currentTime > idleDelayTime)
        {
            // 3. 상태를 Move 로 전환하고 싶다.
            m_state = EnemyState.MOVE;
            // Animation 상태도 Move 로 전환하고 싶다.
            anim.SetTrigger("Move");
            currentTime = 0;
            agent.enabled = true;
        }
    }

    // 1. 타겟쪽으로 이동하고 싶다.
    // 필요속성 : 타겟, 이동 속도
    // 2. 타겟쪽으로 회전하고 싶다.
    // 필요속성 : 회전속도
    public float speed = 5;
    public float rotSpeed = 5;
    // 필요속성 : 공격범위
    public float attackRange = 2;
    private void Move()
    {
        // 타겟쪽으로 이동하고 싶다.
        // 1. 방향이 필요
        // -> direction = target - me
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;

        // agent 를 이용해서 이동하기
        agent.destination = target.transform.position;
        if (distance < attackRange)
        {
            // 2. 상태를 공격으로 전환하고 싶다.
            m_state = EnemyState.ATTACK;
            currentTime = attackDelayTime;
            agent.enabled = false;
        }
    }

   

    // 일정시간에 한번씩 공격하고 싶다.
    // 필요속성 : 공격대기시간
    public float attackDelayTime = 2;
    private void Attack()
    {
        
        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        transform.forward = dir;
        // 일정시간에 한번씩 공격하고 싶다.
        // 1. 시간이 흘렀으니까w
        currentTime += Time.deltaTime;
        // 2. 공격시간이 됐으니까
        // -> 만약 경과시간이 공격대기시간을 초과하였다면
        if (currentTime > attackDelayTime)
        {
           
            int rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    anim.SetTrigger("Attack00");
                    break;
                case 1:
                    anim.SetTrigger("Attack01");
                    break;
                case 2:
                    anim.SetTrigger("Attack02");
                    break;

            }
            currentTime = 0;
        }

        // 타겟이 공격 범위를 벗어나면 상태를 Move 로 전환하고 싶다.
        // 1. 타겟이 공격 범위를 벗어났으니까
        // -> 만약 타겟과의 거리가 공격 범위를 초과 했다면
        // -> 필요정보 : 타겟과의 거리
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > attackRange)
        {
            // 2. 상태를 Move 로 전환하고 싶다.
            m_state = EnemyState.MOVE;
            anim.SetTrigger("Move");
            agent.enabled = true;
        }

    }


    // 일정시간 기다렸다가 상태를 Idle 로 전환하고 싶다.
    // 필요속성 : damage 대기시간
    public float damageDelayTime = 2;
    private IEnumerator Damage(Vector3 shootDirection)
    {
        shootDirection.y = 0;
        //transform.position += shootDirection * 2;
        transform.position = Vector3.Lerp(transform.position, shootDirection * 4, Time.deltaTime); 
        m_state = EnemyState.DAMAGE;
        // animation 의 상태를 피격으로
        anim.SetTrigger("Damage");

        // 대기시간 만큼 기다렸다가 
        yield return new WaitForSeconds(damageDelayTime);
        // 상태를 Idle 로 전환
        m_state = EnemyState.IDLE;

    }

    // 플레이어로부터 피격 받았을때 처리할 함수
    Vector3 knockbackPos;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        // 죽은 애는 또 피격처리 하고 싶지 않다.
        if (m_state == EnemyState.DIE)
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
            cc.enabled = false;
            m_state = EnemyState.DIE;
            anim.SetTrigger("Die");
            
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

    // 아래로 내려가도록 하자.
    // 필요속성 : 떨어지는 속도
    public float downSpeed = 2;
    // 완전히 없어지면 제거하자
    public StageManager_Rio manager;
    bool isAlive = true;
    public Collider swordCollider;
    private void Die()
    {
        swordCollider.enabled = false;
       if(gameObject.name == "Boss")
        {
            manager.Callme();
        }

        // 2초정도 대기하고 
        //currentTime += Time.deltaTime;
        //if (currentTime > 2)
        //{
        //    // 아래로 내려가도록 하자.
        //    // P = P0 + vt
        //    Vector3 vt = Vector3.down * downSpeed * Time.deltaTime;
        //    Vector3 P0 = transform.position;
        //    Vector3 P = P0 + vt;
        //    transform.position = P;

        //    // 완전히 없어지면 제거하자
        //    // 만약 나의 위치가 -1 보다 작아지면
        //    if (P.y <= -1)
        //    {
        //        // 제거하자
        //        // 오브젝트풀에 다시 넣어줘야 한다.
        //        // 1. EnemyManager 객체(인스턴스, 변수) 가 있어야한다.
        //        //EnemyManager em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        //        // 2. 오브젝트풀 있어야 한다.
        //        // 3. 풀에 삽입이 가능하다.
        //        //EnemyManager.Instance.enemyPool.Add(gameObject);
        //        // 4. 나를 비활성화시켜야 한다.
        //        gameObject.SetActive(false);
        //        //Destroy(gameObject);
        //    }
        //}
    }
}

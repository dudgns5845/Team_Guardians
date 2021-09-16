using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalStageEnemy_Skull_Rio : MonoBehaviour
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
        agent.destination = target.transform.position;
        agent.isStopped = true;
    }

    void Update()
    {
        print("skull" + m_state);
        //���� ���°� '������' ������ �븸 ������ �� �հ� �Ѵ�. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ���� �⺻ ����(����)�� �����ϰ� �ʹ�.
        // ���� ���� ���°� Idle �̶��
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

    // �����ð��� ������ ���¸� Move �� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ð�, ����ð�
    public float idleDelayTime = 2;
    float currentTime;
    public float followRange = 60f;
    private void Idle()
    {
        agent.enabled = false;

        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;

        print(distance);
       
        if (distance < followRange)
        { // agent �� �̿��ؼ� �̵��ϱ�
            agent.enabled = true;
            agent.destination = target.transform.position;
            // 2. ���¸� �������� ��ȯ�ϰ� �ʹ�.
            m_state = EnemyState.MOVE;
            anim.SetTrigger("Move");
            currentTime = attackDelayTime;
            
        }
    }

    // 1. Ÿ�������� �̵��ϰ� �ʹ�.
    // �ʿ�Ӽ� : Ÿ��, �̵� �ӵ�
    // 2. Ÿ�������� ȸ���ϰ� �ʹ�.
    // �ʿ�Ӽ� : ȸ���ӵ�
    public float speed = 5;
    public float rotSpeed = 5;
    // �ʿ�Ӽ� : ���ݹ���
    public float attackRange = 2;
    
    private void Move()
    {
        agent.enabled = true;  
        // agent �� �̿��ؼ� �̵��ϱ�
        agent.destination = target.transform.position;
        // Ÿ�������� �̵��ϰ� �ʹ�.
        // 1. ������ �ʿ�
        // -> direction = target - me
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;

      
        if (distance < attackRange)
        {
            // 2. ���¸� �������� ��ȯ�ϰ� �ʹ�.
            m_state = EnemyState.ATTACK;
            currentTime = attackDelayTime;
            agent.enabled = false;
        }
    }



    // �����ð��� �ѹ��� �����ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ݴ��ð�
    public float attackDelayTime = 2;
    private void Attack()
    {

        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        transform.forward = dir;
        // �����ð��� �ѹ��� �����ϰ� �ʹ�.
        // 1. �ð��� �귶���ϱ�w
        currentTime += Time.deltaTime;
        // 2. ���ݽð��� �����ϱ�
        // -> ���� ����ð��� ���ݴ��ð��� �ʰ��Ͽ��ٸ�
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

        // Ÿ���� ���� ������ ����� ���¸� Move �� ��ȯ�ϰ� �ʹ�.
        // 1. Ÿ���� ���� ������ ������ϱ�
        // -> ���� Ÿ�ٰ��� �Ÿ��� ���� ������ �ʰ� �ߴٸ�
        // -> �ʿ����� : Ÿ�ٰ��� �Ÿ�
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > attackRange)
        {
            // 2. ���¸� Move �� ��ȯ�ϰ� �ʹ�.
            m_state = EnemyState.MOVE;
            anim.SetTrigger("Move");
            agent.enabled = true;
        }

    }


    // �����ð� ��ٷȴٰ� ���¸� Idle �� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : damage ���ð�
    public float damageDelayTime = 2;
    private IEnumerator Damage(Vector3 shootDirection)
    {
        shootDirection.y = 0;
        //transform.position += shootDirection * 2;
        //transform.position = Vector3.Lerp(transform.position, shootDirection * 4, Time.deltaTime);
        m_state = EnemyState.DAMAGE;
        // animation �� ���¸� �ǰ�����
        anim.SetTrigger("Damage");

        // ���ð� ��ŭ ��ٷȴٰ� 
        yield return new WaitForSeconds(damageDelayTime);
        // ���¸� Idle �� ��ȯ
        m_state = EnemyState.MOVE;

    }

    // �÷��̾�κ��� �ǰ� �޾����� ó���� �Լ�
    Vector3 knockbackPos;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        // ���� �ִ� �� �ǰ�ó�� �ϰ� ���� �ʴ�.
        if (m_state == EnemyState.DIE)
        {
            return;
        }
        // �ڷ�ƾ�� �����ϰ� �ʹ�.
        StopAllCoroutines();

        agent.enabled = false;

        currentTime = 0;
        hp--;
        // 1. hp �� 0 ���ϸ� ���ְ� �ʹ�.
        if (hp <= 0)
        {
            // ������ �浹ü ��������
            cc.enabled = false;
            m_state = EnemyState.DIE;
            anim.SetTrigger("Die");

        }
        // 2. ������ ���¸� �ǰ����� ��ȯ�ϰ� �ʹ�.
        else
        {
            // �˹� knockback
            // �и� ����
            // P = P0 + vt

            // �ǰ�ó���� �ڷ�ƾ�� �̿��Ͽ� ó���ϰ� �ʹ�.
            StartCoroutine(Damage(shootDirection));

        }
    }

 
  
    public Collider swordCollider;
    private void Die()
    {
        swordCollider.enabled = false;
    }
}

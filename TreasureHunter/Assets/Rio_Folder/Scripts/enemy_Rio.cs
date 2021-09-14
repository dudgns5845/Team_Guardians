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
    private void Idle()
    {
        // �����ð��� ������ ���¸� Move �� ��ȯ�ϰ� �ʹ�.
        // 1. �ð��� �귶���ϱ�
        currentTime += Time.deltaTime;
        // 2. �����ð��� ��������
        // -> ���� ����ð��� ���ð��� �ʰ��Ͽ��ٸ�
        if (currentTime > idleDelayTime)
        {
            // 3. ���¸� Move �� ��ȯ�ϰ� �ʹ�.
            m_state = EnemyState.MOVE;
            // Animation ���µ� Move �� ��ȯ�ϰ� �ʹ�.
            anim.SetTrigger("Move");
            currentTime = 0;
            agent.enabled = true;
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
        // Ÿ�������� �̵��ϰ� �ʹ�.
        // 1. ������ �ʿ�
        // -> direction = target - me
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;

        // agent �� �̿��ؼ� �̵��ϱ�
        agent.destination = target.transform.position;
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
        transform.position = Vector3.Lerp(transform.position, shootDirection * 4, Time.deltaTime); 
        m_state = EnemyState.DAMAGE;
        // animation �� ���¸� �ǰ�����
        anim.SetTrigger("Damage");

        // ���ð� ��ŭ ��ٷȴٰ� 
        yield return new WaitForSeconds(damageDelayTime);
        // ���¸� Idle �� ��ȯ
        m_state = EnemyState.IDLE;

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

    // �Ʒ��� ���������� ����.
    // �ʿ�Ӽ� : �������� �ӵ�
    public float downSpeed = 2;
    // ������ �������� ��������
    public StageManager_Rio manager;
    bool isAlive = true;
    private void Die()
    {
        if (isAlive)
        {
            manager.minEnemy();
            isAlive = false;
        }

        // 2������ ����ϰ� 
        //currentTime += Time.deltaTime;
        //if (currentTime > 2)
        //{
        //    // �Ʒ��� ���������� ����.
        //    // P = P0 + vt
        //    Vector3 vt = Vector3.down * downSpeed * Time.deltaTime;
        //    Vector3 P0 = transform.position;
        //    Vector3 P = P0 + vt;
        //    transform.position = P;

        //    // ������ �������� ��������
        //    // ���� ���� ��ġ�� -1 ���� �۾�����
        //    if (P.y <= -1)
        //    {
        //        // ��������
        //        // ������ƮǮ�� �ٽ� �־���� �Ѵ�.
        //        // 1. EnemyManager ��ü(�ν��Ͻ�, ����) �� �־���Ѵ�.
        //        //EnemyManager em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        //        // 2. ������ƮǮ �־�� �Ѵ�.
        //        // 3. Ǯ�� ������ �����ϴ�.
        //        //EnemyManager.Instance.enemyPool.Add(gameObject);
        //        // 4. ���� ��Ȱ��ȭ���Ѿ� �Ѵ�.
        //        gameObject.SetActive(false);
        //        //Destroy(gameObject);
        //    }
        //}
    }
}

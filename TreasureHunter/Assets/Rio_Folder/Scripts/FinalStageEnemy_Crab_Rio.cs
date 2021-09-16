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
       IDLE,//����
        MOVE, //����
        ATTACK,//����
        DAMAGE,//�λ�
        DIE//����
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
        //���� ���°� '������' ������ �븸 ������ �� �հ� �Ѵ�. 
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


    //�Ϲݻ����϶��� player�� ���� �ݰ濡 ���� �������� 
    //���� ������ ���ƴٴѴ�
    //�߰��߰� ���缭 ǥȿ�ϴ� �ִϸ��̼��� ����ϰ�
    //�ٽ� �ȴ´�
    //�׷��� �÷��̾ ���� ���ݿ� ���� ���ݸ��� ��ȯ�ϰ�
    //���󰣴�
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

        //���� �ݰ濡 ���� attack ����
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

        //���� �ݰ濡 ���� attack ����
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
        agent.isStopped = true; //��ġ �̵� ��� ���߱�
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

    // �����ð� ��ٷȴٰ� ���¸� Idle �� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : damage ���ð�
    float damageDelayTime = 0.5f;
    private IEnumerator Damage(Vector3 shootDirection)
    {
        shootDirection.y = 0;
        //transform.position += shootDirection * 2;
        //transform.position = Vector3.Lerp(transform.position, shootDirection * 4, Time.deltaTime);
        m_state = STATE.DAMAGE;
        // animation �� ���¸� �ǰ�����
        animator.SetTrigger("Take_Damage_1");

        // ���ð� ��ŭ ��ٷȴٰ� 
        yield return new WaitForSeconds(damageDelayTime);
        agent.enabled = true;
        // ���¸� Idle �� ��ȯ
        m_state = STATE.MOVE;

    }

    // �÷��̾�κ��� �ǰ� �޾����� ó���� �Լ�
    Vector3 knockbackPos;
    float currentTime = 0;
    public int hp = 10;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        isMove = false;

        // ���� �ִ� �� �ǰ�ó�� �ϰ� ���� �ʴ�.
        if (m_state == STATE.DIE)
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
            //cc.enabled = false;
            m_state = STATE.DIE;
            animator.SetTrigger("Die");

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

   
    void Die()
    {
       
    }



}

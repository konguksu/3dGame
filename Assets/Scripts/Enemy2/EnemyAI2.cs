using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//<<<���� ���� ��ũ��Ʈ>>>
public class EnemyAI2 : MonoBehaviour
{
    // �� ����
    public enum State
    {
        TRACE = 1,
        ATTACK = 2,
        STOP = 3,
        DIE = 4
    }
    //���� ���� ����
    public State state = State.TRACE;
    //���� ��Ÿ�(�ٰŸ�)
    public float attackDist = 2.0f;
    //���� ��Ÿ� - ���� �� �ȿ� ������ �߰�
    public float traceDist = 10.0f;
    //��� ���� �Ǵ��� ����
    public bool isDie = false;
    //�÷��̾� ��ġ ���� ����
    Transform playerTr;
    //�� ��ġ ���� ����
    Transform enemyTr;
    //�ڷ�ƾ���� ����� �����ð�
    WaitForSeconds ws;
    //�̵� ���� MoveAgent Ŭ���� ����
    MoveAgent moveAgent;
    //Animator ������Ʈ ����
    Animator animator;
    //���� �����ϴ� EnemtAttack Ŭ����
    EnemyAttack2 enemyAttack;


    public int st;

    private void Awake()
    {
        //�÷��̾� GameObject ����
        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");
        //�÷��̾��� Transform ������Ʈ ����
        if(player != null)
        {
            playerTr = player.transform;
        }
        //�� Transform ������Ʈ ����
        enemyTr = GetComponent<Transform>();
        //�̵� ���� MoveAgent Ŭ���� ����
        moveAgent = GetComponent<MoveAgent>();
        //Animator ������Ʈ ����
        animator = GetComponent<Animator>();
        //���� ���� EnemyAttack Ŭ���� ����
        enemyAttack = GetComponent<EnemyAttack2>();
        //�ڷ�ƾ�� �����ð� ����
        ws = new WaitForSeconds(0.3f);
    }
    private void OnEnable()
    {
        //checkState �ڷ�ƾ �Լ� ����
        StartCoroutine(CheckState());
        //Action �ڷ�ƾ �Լ� ����
        StartCoroutine(Action());
    }

    //�� ���� �˻� �ڷ�ƾ
    IEnumerator CheckState()
    {
        //�� ��������� �ݺ�
        while (!isDie)
        {
            //���°� ��� -> ����
            if (state == State.DIE) yield break;
            //���ΰ��� �� ĳ���� ���� �Ÿ� ���
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if (dist <= attackDist) //���� ��Ÿ� �̳�
            { 
                state = State.ATTACK; 
            }
            else if (dist > attackDist) //���� ��Ÿ� ���� �־�����
            {
                state = State.TRACE;
            }
            else //����
            {
                state = State.STOP;
            }
            //0.3�ʰ� ����� �簳
            yield return ws;
        }
    }
    //�� ���¿� ���� �� ĳ������ �ൿ ó�� �ڷ�ƾ
    IEnumerator Action()
    {
        //�� �ױ� ������ �ݺ�
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.TRACE:
                    //���� ����
                    enemyAttack.isAttack = false;
                    moveAgent.SetTraceTarget(playerTr.position);
                    animator.SetInteger("State", (int)State.TRACE);
                    break;
                case State.ATTACK:
                    if(enemyAttack.isAttack == false)
                    {
                        enemyAttack.isAttack = true;
                    }          
                    moveAgent.Stop();                   
                    //animator.SetInteger("State", (int)State.ATTACK);
                    break;
                case State.STOP:
                    enemyAttack.isAttack = false;
                    moveAgent.Stop();
                    animator.SetInteger("State", (int)State.STOP);
                    break;
                case State.DIE:
                    isDie = true;
                    enemyAttack.isAttack = false;
                    moveAgent.Stop();
                    animator.SetTrigger("DIE");
                    GetComponent<CapsuleCollider>().enabled = false;
                    Destroy(gameObject, 3);
                    break;    
            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //�� �ü� �÷��̾� ����
        if (!isDie)
        {
            enemyTr.LookAt(playerTr);
        }
        

        st = animator.GetInteger("State");
    }
}

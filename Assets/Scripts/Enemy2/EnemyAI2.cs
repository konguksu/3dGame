using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//<<<상태 변경 스크립트>>>
public class EnemyAI2 : MonoBehaviour
{
    // 적 상태
    public enum State
    {
        TRACE = 1,
        ATTACK = 2,
        STOP = 3,
        DIE = 4
    }
    //상태 저장 변수
    public State state = State.TRACE;
    //공격 사거리(근거리)
    public float attackDist = 2.0f;
    //추적 사거리 - 같은 방 안에 있으면 추격
    public float traceDist = 10.0f;
    //사망 여부 판단할 변수
    public bool isDie = false;
    //플레이어 위치 저장 변수
    Transform playerTr;
    //적 위치 저장 변수
    Transform enemyTr;
    //코루틴에서 사용할 지연시간
    WaitForSeconds ws;
    //이동 제어 MoveAgent 클래스 저장
    MoveAgent moveAgent;
    //Animator 컴포넌트 저장
    Animator animator;
    //공격 제어하는 EnemtAttack 클래스
    EnemyAttack2 enemyAttack;


    public int st;

    private void Awake()
    {
        //플레이어 GameObject 추출
        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");
        //플레이어의 Transform 컴포넌트 추출
        if(player != null)
        {
            playerTr = player.transform;
        }
        //적 Transform 컴포넌트 추출
        enemyTr = GetComponent<Transform>();
        //이동 제어 MoveAgent 클래스 추출
        moveAgent = GetComponent<MoveAgent>();
        //Animator 컴포넌트 추출
        animator = GetComponent<Animator>();
        //공격 제어 EnemyAttack 클래스 추출
        enemyAttack = GetComponent<EnemyAttack2>();
        //코루틴의 지연시간 생성
        ws = new WaitForSeconds(0.3f);
    }
    private void OnEnable()
    {
        //checkState 코루틴 함수 실행
        StartCoroutine(CheckState());
        //Action 코루틴 함수 실행
        StartCoroutine(Action());
    }

    //적 상태 검사 코루틴
    IEnumerator CheckState()
    {
        //적 사망전까지 반복
        while (!isDie)
        {
            //상태가 사망 -> 종료
            if (state == State.DIE) yield break;
            //주인공과 적 캐릭터 간의 거리 계산
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if (dist <= attackDist) //공격 사거리 이내
            { 
                state = State.ATTACK; 
            }
            else if (dist > attackDist) //공격 사거리 보다 멀어지면
            {
                state = State.TRACE;
            }
            else //멈춤
            {
                state = State.STOP;
            }
            //0.3초간 대기후 재개
            yield return ws;
        }
    }
    //적 상태에 따라 적 캐릭터의 행동 처리 코루틴
    IEnumerator Action()
    {
        //적 죽기 전까지 반복
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.TRACE:
                    //추적 상태
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
        //적 시선 플레이어 추적
        if (!isDie)
        {
            enemyTr.LookAt(playerTr);
        }
        

        st = animator.GetInteger("State");
    }
}

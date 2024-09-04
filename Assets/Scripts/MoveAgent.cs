using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//<<<적 행동 처리 스크립트>>>
public class MoveAgent : MonoBehaviour
{
    //플레이어 추적 속도
    public float traceSpeed = 3.0f;

    //NavMeshAgent 컴포넌트 저장 변수
    NavMeshAgent agent;
    //추적 대상 위치 저장 변수
    Vector3 traceTarget;

    public void SetTraceTarget(Vector3 pos)
    {
        traceTarget = pos;
        agent.speed = traceSpeed;
        TraceTarget(traceTarget);
    }
    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent 컴포넌트 추출 후 변수에 저장
        agent = GetComponent<NavMeshAgent>();
        agent.speed = traceSpeed;

        //추적
        //SetTraceTarget()
    }
    //주인공 추적 이동 함수
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;
        agent.destination = pos;
        agent.isStopped = false;
    }
    // 정지
    public void Stop()
    {
        {
            agent.isStopped = true;
            //바로 정지 -> 속도 0 으로
            agent.velocity = Vector3.zero;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

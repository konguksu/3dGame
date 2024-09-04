using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//<<<�� �ൿ ó�� ��ũ��Ʈ>>>
public class MoveAgent : MonoBehaviour
{
    //�÷��̾� ���� �ӵ�
    public float traceSpeed = 3.0f;

    //NavMeshAgent ������Ʈ ���� ����
    NavMeshAgent agent;
    //���� ��� ��ġ ���� ����
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
        //NavMeshAgent ������Ʈ ���� �� ������ ����
        agent = GetComponent<NavMeshAgent>();
        agent.speed = traceSpeed;

        //����
        //SetTraceTarget()
    }
    //���ΰ� ���� �̵� �Լ�
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;
        agent.destination = pos;
        agent.isStopped = false;
    }
    // ����
    public void Stop()
    {
        {
            agent.isStopped = true;
            //�ٷ� ���� -> �ӵ� 0 ����
            agent.velocity = Vector3.zero;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

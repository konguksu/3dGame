using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_boneThrow : MonoBehaviour
{
    // 파괴력 
    public float damage = 10.0f;
    // 타겟 == 플레이어
    public Transform target;
    //점호각
    public float firingAngle = 45.0f;
    //중력
    public float gravity = 9.8f;

    //코루틴 대기시간
    WaitForSeconds ws;

    // 발사체 위치
    public Transform Projectile;

    //속도
    public float speed = 5;


    private void Awake()
    {

        //타겟 (플레이어)위치
        //플레이어 GameObject 추출
        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");
        //플레이어의 Transform 컴포넌트 추출
        if (player != null)
        {
            target = player.transform;
        }

        //코루틴의 지연시간 생성
        ws = new WaitForSeconds(0.3f);

    }

    void Start()
    {
        //발사체 이동 코루틴
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        //대기시간
        yield return ws;
        //발사체 위치 설정
        //Projectile.position = boneTr.position + new Vector3(0, 0.0f, 0);
        //타겟까지 거리 계산
        float targetDis = Vector3.Distance(Projectile.position, target.position);
        
        //속도 계산
        float projectile_Velocity = targetDis / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        //속도의x,y 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        //비행 시간 계산
        float flightDuration = targetDis / Vx;
        //타겟 보게 회전
        Projectile.rotation = Quaternion.LookRotation(target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Projectile.position.y < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}

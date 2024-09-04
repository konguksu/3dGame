using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 공격 여부 판단
    public bool isThrow = false;
    // 공격 사운드
    public AudioClip throwSound;
    //뼈다귀 프리팹
    public GameObject bone;
    //발사 위치 정보
    public Transform throwPos;

    //AudioSource 컴포넌트 저장
    AudioSource _audio;
    //Animator 컴포넌트
    Animator animator;
    //플레이어 Transform
    Transform playerTr;
    //적 Transform
    Transform enemyTr;
    //발사 효과
    //ParticleSystem 
    //다음 발사할 시간 계산 변수
    float nextThrow = 0.0f;
    //발사 간격
    float ThrowRate = 1.0f;
    //회전 속도 계수
    //float damping = 10.0f;

    void Start()
    {
        //컴포넌트들 추출
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isThrow)
        {
            //현재 시간이 다음 공격 시간보다 큰지
            if(Time.time >= nextThrow)
            {
                Throw();
                //다음 공격 시간 계산
                nextThrow = Time.time + ThrowRate + Random.Range(0.0f, 0.5f);
            }
        }
    }


    void Throw()
    {
        animator.SetTrigger("Attack");
        _audio.PlayOneShot(throwSound, 0.5f);

        //뼈 생성
        GameObject _bone = Instantiate(bone, throwPos.position, throwPos.rotation);
        //일정 시간 지난 후 삭제
        Destroy(_bone, 3.0f);
    }
}

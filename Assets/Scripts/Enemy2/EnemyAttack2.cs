using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//근거리 공격 적 스크립트
public class EnemyAttack2 : MonoBehaviour
{
    // 공격 여부 판단
    public bool isAttack = false;
    // 공격 사운드
    public AudioClip AttackSound;
    //무기 프리팹
    public GameObject sword;
    

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
    //다음 공격할 시간 계산 변수
    float nextAttack = 0.0f;
    //발사 간격
    float attackRate = 1.0f;
   

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
        if (isAttack)
        {
            //현재 시간이 다음 공격 시간보다 큰지
            if(Time.time >= nextAttack)
            {
                Attack();
                //다음 공격 시간 계산
                nextAttack = Time.time + attackRate + Random.Range(0.0f, 0.5f);
            }
        }
    }


    void Attack()
    {
        animator.SetTrigger("Attack");
        _audio.PlayOneShot(AttackSound, 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 이동 속도
    public float moveSpeed = 5.0f;
    //회전 속도 변수
    public float rotSpeed = 80.0f;
    //캐릭터 공격력
    public float damage = 30.0f;
    //Animator 컴포넌트를 저장할 변수
    Animator animator;
    //사망 여부
    public bool isDie = false;
    //코루틴에서 사용할 지연시간 변수
    WaitForSeconds ws;
    //캐릭터 상태 표현
    public enum State
    {
        IDLE,
        WALK,
        ATTACK,
        DAMAGE,
        DIE
    }
    //상태 저장
    public State state = State.IDLE;
    //PlayerAttack 컴포넌트
    PlayerAttack playerAttack;

    //소리
    public AudioClip walkSound;
    //AudioSource 컴포넌트 저장
    AudioSource _audio;

    void Start()
    {
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(v, 0, -h);

        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * -h) + (Vector3.right * v);
        if (state !=State.DIE)
        {
            //바라보는 방향으로 회전
            if (!(h == 0 && v == 0) && !GetComponent<PlayerAttack>().isAttack)
            {

                //이동 및 회전
                transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
                //회전하는 부분
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

                state = State.WALK;

            }
            else if (playerAttack.isAttack)
            {
                state = State.ATTACK;
            }
            else
            {
                state = State.IDLE;
                

            }
        }
        
    }

    private void Awake()
    {
        //Animatior 컴포넌트 추출
        animator = GetComponent<Animator>();

        //코루틴의 지연시간 생성
        ws = new WaitForSeconds(0.3f);
    }

    private void OnEnable()
    {
        //Action 코루틴 함수 실행
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                //기본
                case State.IDLE:
                    animator.SetBool("IsMove", false);
                    break;

                case State.WALK:
                    animator.SetBool("IsMove", true);
                    break;
                case State.ATTACK:
                    animator.SetBool("IsMove", false);
                    break;
                case State.DIE:
                    isDie = true;
                    GetComponent<PlayerAttack>().enabled = false;
                    GetComponent<PlayerDamage>().enabled = false;
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;

            }
        }
    }
}

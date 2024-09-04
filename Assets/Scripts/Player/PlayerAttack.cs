using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //공격 소리
    public AudioClip attackSound;
    //AudioSource 컴포넌트 저장
    AudioSource _audio;
    //Animator 컴포넌트
    Animator animator;
    //공격 쿨타임
    public float wTime = 0.7f;
    float cTime = 0;
    //공격중인지
    public bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트들 추출
        animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //스페이스바가 눌리고 공격중 상태가 아니면
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            if(gameObject.GetComponent<PlayerCtrl>().state != PlayerCtrl.State.DIE)
            {
                //공격
                Attack();
            }
            
        }
        //공격중 상태라면 일정 시간이 지난 후 다시 false로 복귀
        if (isAttack)
        {
            cTime += Time.deltaTime;
            if (cTime >= wTime)
            {
                isAttack = false;
                cTime = 0;
            }
        }
    }

    void Attack()
    {
        isAttack = true;
        animator.SetTrigger("Attack");
        _audio.PlayOneShot(attackSound, 0.5f);

    }
}

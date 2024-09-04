using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //���� �Ҹ�
    public AudioClip attackSound;
    //AudioSource ������Ʈ ����
    AudioSource _audio;
    //Animator ������Ʈ
    Animator animator;
    //���� ��Ÿ��
    public float wTime = 0.7f;
    float cTime = 0;
    //����������
    public bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ�� ����
        animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����̽��ٰ� ������ ������ ���°� �ƴϸ�
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            if(gameObject.GetComponent<PlayerCtrl>().state != PlayerCtrl.State.DIE)
            {
                //����
                Attack();
            }
            
        }
        //������ ���¶�� ���� �ð��� ���� �� �ٽ� false�� ����
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

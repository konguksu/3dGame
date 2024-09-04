using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ٰŸ� ���� �� ��ũ��Ʈ
public class EnemyAttack2 : MonoBehaviour
{
    // ���� ���� �Ǵ�
    public bool isAttack = false;
    // ���� ����
    public AudioClip AttackSound;
    //���� ������
    public GameObject sword;
    

    //AudioSource ������Ʈ ����
    AudioSource _audio;
    //Animator ������Ʈ
    Animator animator;
    //�÷��̾� Transform
    Transform playerTr;
    //�� Transform
    Transform enemyTr;
    //�߻� ȿ��
    //ParticleSystem 
    //���� ������ �ð� ��� ����
    float nextAttack = 0.0f;
    //�߻� ����
    float attackRate = 1.0f;
   

    void Start()
    {
        //������Ʈ�� ����
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
            //���� �ð��� ���� ���� �ð����� ū��
            if(Time.time >= nextAttack)
            {
                Attack();
                //���� ���� �ð� ���
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

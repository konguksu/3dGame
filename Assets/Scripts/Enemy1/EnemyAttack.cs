using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ���� ���� �Ǵ�
    public bool isThrow = false;
    // ���� ����
    public AudioClip throwSound;
    //���ٱ� ������
    public GameObject bone;
    //�߻� ��ġ ����
    public Transform throwPos;

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
    //���� �߻��� �ð� ��� ����
    float nextThrow = 0.0f;
    //�߻� ����
    float ThrowRate = 1.0f;
    //ȸ�� �ӵ� ���
    //float damping = 10.0f;

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
        if (isThrow)
        {
            //���� �ð��� ���� ���� �ð����� ū��
            if(Time.time >= nextThrow)
            {
                Throw();
                //���� ���� �ð� ���
                nextThrow = Time.time + ThrowRate + Random.Range(0.0f, 0.5f);
            }
        }
    }


    void Throw()
    {
        animator.SetTrigger("Attack");
        _audio.PlayOneShot(throwSound, 0.5f);

        //�� ����
        GameObject _bone = Instantiate(bone, throwPos.position, throwPos.rotation);
        //���� �ð� ���� �� ����
        Destroy(_bone, 3.0f);
    }
}

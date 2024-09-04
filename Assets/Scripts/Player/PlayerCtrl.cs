using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // �̵� �ӵ�
    public float moveSpeed = 5.0f;
    //ȸ�� �ӵ� ����
    public float rotSpeed = 80.0f;
    //ĳ���� ���ݷ�
    public float damage = 30.0f;
    //Animator ������Ʈ�� ������ ����
    Animator animator;
    //��� ����
    public bool isDie = false;
    //�ڷ�ƾ���� ����� �����ð� ����
    WaitForSeconds ws;
    //ĳ���� ���� ǥ��
    public enum State
    {
        IDLE,
        WALK,
        ATTACK,
        DAMAGE,
        DIE
    }
    //���� ����
    public State state = State.IDLE;
    //PlayerAttack ������Ʈ
    PlayerAttack playerAttack;

    //�Ҹ�
    public AudioClip walkSound;
    //AudioSource ������Ʈ ����
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

        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * -h) + (Vector3.right * v);
        if (state !=State.DIE)
        {
            //�ٶ󺸴� �������� ȸ��
            if (!(h == 0 && v == 0) && !GetComponent<PlayerAttack>().isAttack)
            {

                //�̵� �� ȸ��
                transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
                //ȸ���ϴ� �κ�
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
        //Animatior ������Ʈ ����
        animator = GetComponent<Animator>();

        //�ڷ�ƾ�� �����ð� ����
        ws = new WaitForSeconds(0.3f);
    }

    private void OnEnable()
    {
        //Action �ڷ�ƾ �Լ� ����
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                //�⺻
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

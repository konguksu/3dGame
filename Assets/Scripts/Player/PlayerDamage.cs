using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    // ���� ������ 
   // public float hp;

    //Animator ������Ʈ�� ������ ����
    Animator animator;
    //Hp Bar �̹��� 
    Image hpBar;
    //���� ������ ó�� ����
    //Color initColor = new Vector4(0.3171649f, 0.6037736f, 0.05923815f, 1.0f);
    //���� ������ ���� ����
    Color currColor;
    //���� �Ŵ���
    GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�� 3���� �߶� �� �κ�� ���ư�
        if(transform.position.y <= -3f)
        {
            BackToLoby();
        }
    }
    private void Awake()
    {
        //Animatior ������Ʈ ����
        animator = GetComponent<Animator>();
        //������Ʈ ��������
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //���� �Ŵ���
        hpBar = gameManager.hpBar;

        //ü�� ����ä�� �κ񿡼� ����� �ִϸ��̼�
        if(gameManager.playerHP<100.0f && SceneManager.GetActiveScene().name == "Loby" && gameManager.clearToLoby == false)
        {
            animator.SetTrigger("GetUp");
        }
        gameManager.clearToLoby = false;
        //������ �ð��� �ʱ�ȭ
        gameManager.time = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //�浹 �±�
        print(collision.tag);
        //���� �浹
        if (collision.tag == "BULLET")
        {
            //���ݷ¸�ŭ ü�� ����           
            gameManager.playerHP -= collision.gameObject.GetComponent<E_boneThrow>().damage;

            IsDie();
        }
        //Į�̶� �浹
        if (collision.tag == "SWORD")
        {
            //���ݷ¸�ŭ ü�� ����           
            gameManager.playerHP -= 5.0f;

            IsDie();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        //���ö� �浹
        if (other.tag == "SPIKE")
        {
            gameManager.playerHP -= 150.0f*Time.deltaTime;
            IsDie();
        }
        //������ �浹
        if (other.tag == "BOSS")
        {
            gameManager.playerHP -= 100.0f * Time.deltaTime;
            IsDie();
        }
    }
    public void DisplayHpBar()
    {
        //���� hp
        float ratio = gameManager.playerHP / 100.0f;
        //���� ��ġ 50% �̻� �����
        if (ratio >0.5f)
        {
            currColor = new Vector4(0.3171649f, 0.6037736f, 0.05923815f, 1.0f);
        }
        //���� ��ġ 50% ���� �����
        else if (ratio > 0.2f && ratio <= 0.5f)
        {
            currColor = new Vector4(1.0f, 0.795037f, 0f, 1.0f);

        }
        //���� ��ġ 20%���� ������
        else if(ratio <= 0.2f)
        {
            currColor = new Vector4(0.7843138f, 0.3333333f, 0.1607843f, 1.0f);
        }
        //HpBar ���� ����
        hpBar.color = currColor;
        //HpBar�� ũ�� ����
        hpBar.fillAmount = ratio;
    }

    public void IsDie()
    {
        //ü�� ��� ��� �κ� �ƴϸ� 
        if (gameManager.playerHP <= 0.0f && SceneManager.GetActiveScene().name != "Loby")
        {
            if (GetComponent<PlayerCtrl>().state != PlayerCtrl.State.DIE)
            {
                //�÷��̾� ���� ������� ����
                GetComponent<PlayerCtrl>().state = PlayerCtrl.State.DIE;
                animator.SetTrigger("DIE");              
                //3�ʵ� �κ�� ���ư�
                Invoke("BackToLoby", 3.0f);

            }

        }
        //���� �������� ���� �� ũ�� ���� �Լ�
        DisplayHpBar();
    }

    //��� �� �κ�� �̵�
    public void BackToLoby()
    {
        SceneManager.LoadScene("Loby");
    }

}

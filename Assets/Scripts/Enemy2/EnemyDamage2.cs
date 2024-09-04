using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage2 : MonoBehaviour
{
    // ���� ������ 
    public float hp = 80.0f;
    //���� ������ ������ ���� ����
    public GameObject hpBarPrefab;
    //���� ������ ��ġ ���� ������
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //�θ� �� Canvas ��ü
    private Canvas uiCanvas;
    //���� ��ġ�� ���� fillAmount �Ӽ� ���� Image
    private Image hpBarImage;
    //�ǰ� �Ҹ�
    public AudioClip Sound;
    //AudioSource ������Ʈ ����
    AudioSource _audio;
    void Start()
    {
        //���� ������ ���� �� �ʱ�ȭ
        SetHpBar();

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);

        //�浹 && �÷��̾ ���� ��
        if (other.tag == "PLAYER_ATTACK" && other.gameObject.GetComponentInParent<PlayerAttack>().isAttack)
        {
            //�÷��̾� ���ݷ¸�ŭ ü�� ����
            hp -= other.gameObject.GetComponentInParent<PlayerCtrl>().damage;
            //���� ������ ����(����ü��/�ִ�ü��)
            hpBarImage.fillAmount = hp / 80.0f;
            

            if (hp <= 0.0f)
            {
                //����� �� �� ����
                GameObject.Find("GameManager").GetComponent<GameManager>().killEnemyNum++;
                //�� ���� ������� ����
                GetComponent<EnemyAI2>().state = EnemyAI2.State.DIE;
                //�ǰ� �Ҹ�
                _audio.PlayOneShot(Sound, 0.5f);
                //�� ��� �� ���� ������ �Ⱥ��̰�
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                hpBarImage.GetComponentInParent<EnemyHPBar>().enabled = false;

            }
        }
    }
    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //UI Canvas ������ ���� ������ ����
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        //fillAmount �Ӽ� ������ �̹��� ����
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        //���� �������� ���󰡾� �� ���� ������ �� ����
        EnemyHPBar bar = hpBar.GetComponent<EnemyHPBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }
}

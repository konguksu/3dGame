using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //�ؽ�Ʈ
    public GameObject text;
    public GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //�÷��̾ ���� �ȿ� ������ ü���� ���������
        if (other.tag == "PLAYER" && gameManager.playerHP < 100.0f)
        {
            //e �����ÿ� �ؽ�Ʈ Ȱ��ȭ
            text.SetActive(true);
            //e ������
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                //Ǯ�� ȸ��
                gameManager.playerHP = 100f;
                GameObject.Find("Player").GetComponent<PlayerDamage>().DisplayHpBar();
                //�ؽ�Ʈ �Ⱥ��̰�
                text.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
            text.SetActive(false);
    }
}

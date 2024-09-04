using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //텍스트
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
        //플레이어가 범위 안에 들어오고 체력이 닳아있으면
        if (other.tag == "PLAYER" && gameManager.playerHP < 100.0f)
        {
            //e 누르시오 텍스트 활성화
            text.SetActive(true);
            //e 누르면
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                //풀피 회복
                gameManager.playerHP = 100f;
                GameObject.Find("Player").GetComponent<PlayerDamage>().DisplayHpBar();
                //텍스트 안보이게
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    // 생명 게이지 
   // public float hp;

    //Animator 컴포넌트를 저장할 변수
    Animator animator;
    //Hp Bar 이미지 
    Image hpBar;
    //생명 게이지 처음 색상
    //Color initColor = new Vector4(0.3171649f, 0.6037736f, 0.05923815f, 1.0f);
    //생명 게이지 현재 색상
    Color currColor;
    //게임 매니저
    GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //방 3에서 추락 시 로비로 돌아감
        if(transform.position.y <= -3f)
        {
            BackToLoby();
        }
    }
    private void Awake()
    {
        //Animatior 컴포넌트 추출
        animator = GetComponent<Animator>();
        //컴포넌트 가져오기
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //게임 매니저
        hpBar = gameManager.hpBar;

        //체력 닳은채로 로비에서 깨어나면 애니메이션
        if(gameManager.playerHP<100.0f && SceneManager.GetActiveScene().name == "Loby" && gameManager.clearToLoby == false)
        {
            animator.SetTrigger("GetUp");
        }
        gameManager.clearToLoby = false;
        //보스방 시간초 초기화
        gameManager.time = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //충돌 태그
        print(collision.tag);
        //뼈랑 충돌
        if (collision.tag == "BULLET")
        {
            //공격력만큼 체력 차감           
            gameManager.playerHP -= collision.gameObject.GetComponent<E_boneThrow>().damage;

            IsDie();
        }
        //칼이랑 충돌
        if (collision.tag == "SWORD")
        {
            //공격력만큼 체력 차감           
            gameManager.playerHP -= 5.0f;

            IsDie();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        //가시랑 충돌
        if (other.tag == "SPIKE")
        {
            gameManager.playerHP -= 150.0f*Time.deltaTime;
            IsDie();
        }
        //보스랑 충돌
        if (other.tag == "BOSS")
        {
            gameManager.playerHP -= 100.0f * Time.deltaTime;
            IsDie();
        }
    }
    public void DisplayHpBar()
    {
        //남은 hp
        float ratio = gameManager.playerHP / 100.0f;
        //생명 수치 50% 이상 노란색
        if (ratio >0.5f)
        {
            currColor = new Vector4(0.3171649f, 0.6037736f, 0.05923815f, 1.0f);
        }
        //생명 수치 50% 이하 노란색
        else if (ratio > 0.2f && ratio <= 0.5f)
        {
            currColor = new Vector4(1.0f, 0.795037f, 0f, 1.0f);

        }
        //생명 수치 20%이하 빨간색
        else if(ratio <= 0.2f)
        {
            currColor = new Vector4(0.7843138f, 0.3333333f, 0.1607843f, 1.0f);
        }
        //HpBar 색상 변경
        hpBar.color = currColor;
        //HpBar의 크기 변경
        hpBar.fillAmount = ratio;
    }

    public void IsDie()
    {
        //체력 모두 닳고 로비가 아니면 
        if (gameManager.playerHP <= 0.0f && SceneManager.GetActiveScene().name != "Loby")
        {
            if (GetComponent<PlayerCtrl>().state != PlayerCtrl.State.DIE)
            {
                //플레이어 상태 사망으로 변경
                GetComponent<PlayerCtrl>().state = PlayerCtrl.State.DIE;
                animator.SetTrigger("DIE");              
                //3초뒤 로비로 돌아감
                Invoke("BackToLoby", 3.0f);

            }

        }
        //생명 게이지의 색상 및 크기 변경 함수
        DisplayHpBar();
    }

    //사망 후 로비로 이동
    public void BackToLoby()
    {
        SceneManager.LoadScene("Loby");
    }

}

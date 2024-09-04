using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage2 : MonoBehaviour
{
    // 생명 게이지 
    public float hp = 80.0f;
    //생명 게이지 프리팹 저장 변수
    public GameObject hpBarPrefab;
    //생명 게이지 위치 보정 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성 변경 Image
    private Image hpBarImage;
    //피격 소리
    public AudioClip Sound;
    //AudioSource 컴포넌트 저장
    AudioSource _audio;
    void Start()
    {
        //생명 게이지 생성 및 초기화
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

        //충돌 && 플레이어가 공격 중
        if (other.tag == "PLAYER_ATTACK" && other.gameObject.GetComponentInParent<PlayerAttack>().isAttack)
        {
            //플레이어 공격력만큼 체력 차감
            hp -= other.gameObject.GetComponentInParent<PlayerCtrl>().damage;
            //생명 게이지 감소(현재체력/최대체력)
            hpBarImage.fillAmount = hp / 80.0f;
            

            if (hp <= 0.0f)
            {
                //사망한 적 수 증가
                GameObject.Find("GameManager").GetComponent<GameManager>().killEnemyNum++;
                //적 상태 사망으로 변경
                GetComponent<EnemyAI2>().state = EnemyAI2.State.DIE;
                //피격 소리
                _audio.PlayOneShot(Sound, 0.5f);
                //적 사망 후 생명 게이지 안보이게
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                hpBarImage.GetComponentInParent<EnemyHPBar>().enabled = false;

            }
        }
    }
    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //UI Canvas 하위로 생명 게이지 생성
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        //fillAmount 속성 변경할 이미지 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        //생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        EnemyHPBar bar = hpBar.GetComponent<EnemyHPBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }
}

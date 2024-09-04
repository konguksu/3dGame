using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //유아이 캔버스
    public GameObject canvas;
    //클리어 ui 캔버스
    public GameObject Clearcanvas;
    //방 1 클리어 여부
    public bool is1Clear = false;
    //상자 프리팹
    public GameObject chest;
    //Hp Bar 이미지 
    public Image hpBar;
    //ui 키 이미지
    public Image[] keyImage = new Image[3];
    //죽여야 하는 적 수 & 죽인 적 수
    public int enemyNum = 3;
    public int killEnemyNum = 0;
    //게임 오버 여부
    public bool gameOver = false;
    //플레이어 체력
    public float playerHP = 100.0f;
    //보스 방 제한시간
    public float timeClear = 15.0f;
    public float time = 0;
    //보스 방 시간 텍스트
    public GameObject timeText;
    //방 깨고 로비로 돌아온건지
    public bool clearToLoby = false;
    // 씬 정보
    public enum Scene
    {
        MAIN,
        LOBY,
        ROOM1,
        ROOM2,
        ROOM3,
        BOSS,
        GOVER,
        GCLEAR
    }
    // 씬 정보 저장 변수
    public Scene scene = Scene.MAIN;
    //코루틴에서 사용할 지연시간
    WaitForSeconds ws;
    private void Awake()
    {
        //코루틴의 지연시간 생성
        ws = new WaitForSeconds(0.3f);
        if(scene != Scene.GCLEAR)
        {
            //삭제되지 않는오브젝트 지정
            DontDestroyOnLoad(gameObject);
        }
        


    }
    private void OnEnable()
    {
        //씬체크 코루틴 함수 실행
        StartCoroutine(CheckScene());
        StartCoroutine(CheckRoomClear());
    }

    IEnumerator CheckScene()
    {
        //현재 씬 계속 감시
        while (true)
        {
            //현재 씬
            if(SceneManager.GetActiveScene().name == "Room_1")
            {
                scene = Scene.ROOM1;
            }
            else if (SceneManager.GetActiveScene().name == "Room_2")
            {
                scene = Scene.ROOM2;
            }
            else if (SceneManager.GetActiveScene().name == "Room_3")
            {
                scene = Scene.ROOM3;
            }
            else if (SceneManager.GetActiveScene().name == "Boss")
            {
                scene = Scene.BOSS;
            }
            else if (SceneManager.GetActiveScene().name == "GOver")
            {
                scene = Scene.GOVER;
            }
            else if (SceneManager.GetActiveScene().name == "ClearScene")
            {
                scene = Scene.GCLEAR;
            }
            else if(SceneManager.GetActiveScene().name == "Loby")
            {
                scene = Scene.LOBY;
            }
            //ws 만큼 대기 후 재개
            yield return ws;
        }
    }

    IEnumerator CheckRoomClear()
    {
        
        while (!gameOver)
        {
            yield return ws;
            switch (scene)
            {
                case Scene.ROOM1:
                    //클리어 여부 확인
                    Room1_isClear();
                    
                    //방 1 클리어 시
                    if (is1Clear == true && GameObject.FindGameObjectWithTag("CHEST") == null)
                    {
                        //상자 생성
                        Instantiate(chest);
                        
                    }
                    break;
                case Scene.ROOM2:
                    if (keyImage[1].color == Color.white)
                    {
                        //스파이크 비활성화
                        GameObject[] Spike = GameObject.FindGameObjectsWithTag("SPIKE");
                        for (int i = 0;i< Spike.Length; i++)
                        {
                            Destroy(Spike[i]);
                        }
                        
                    }
                    break;
                case Scene.BOSS:
                    
                    break;
                
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scene == Scene.BOSS)
        {
            //남은시간 표시
            timeText.SetActive(true);
            timeText.GetComponent<Text>().text = "남은 시간: " + (int)(15.0f - time);
            time += Time.deltaTime;
            //시간 제한 넘기면 클리어
            if (time >= timeClear)
            {
                canvas.SetActive(false);
                timeText.SetActive(false);
                scene = Scene.GCLEAR;
                print("클리어");
                SceneManager.LoadScene("ClearScene");
                Clearcanvas.SetActive(true);
            }
            
        }
        else
            timeText.SetActive(false);




    }

    public void Room1_isClear()
    {
        //적이 모두 죽으면
        if (enemyNum - killEnemyNum == 0)
        {
            is1Clear = true;
        }
       
    }
    //로비 이동 버튼(게임 시작 버튼)
    public void StartGame()
    {
        SceneManager.LoadScene("Loby");
        canvas.SetActive(true);

    }
    //재시작 버튼
    public void RestartGame()
    {
        //시작화면 불러오기
        SceneManager.LoadScene("StartScene");
        //클리어씬 ui 비활성화
        Clearcanvas.SetActive(false);
        //새로 시작하니 제거
        Destroy(this.gameObject);
        print(gameObject.name);
    }
}

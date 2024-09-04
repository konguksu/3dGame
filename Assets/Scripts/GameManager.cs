using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //������ ĵ����
    public GameObject canvas;
    //Ŭ���� ui ĵ����
    public GameObject Clearcanvas;
    //�� 1 Ŭ���� ����
    public bool is1Clear = false;
    //���� ������
    public GameObject chest;
    //Hp Bar �̹��� 
    public Image hpBar;
    //ui Ű �̹���
    public Image[] keyImage = new Image[3];
    //�׿��� �ϴ� �� �� & ���� �� ��
    public int enemyNum = 3;
    public int killEnemyNum = 0;
    //���� ���� ����
    public bool gameOver = false;
    //�÷��̾� ü��
    public float playerHP = 100.0f;
    //���� �� ���ѽð�
    public float timeClear = 15.0f;
    public float time = 0;
    //���� �� �ð� �ؽ�Ʈ
    public GameObject timeText;
    //�� ���� �κ�� ���ƿ°���
    public bool clearToLoby = false;
    // �� ����
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
    // �� ���� ���� ����
    public Scene scene = Scene.MAIN;
    //�ڷ�ƾ���� ����� �����ð�
    WaitForSeconds ws;
    private void Awake()
    {
        //�ڷ�ƾ�� �����ð� ����
        ws = new WaitForSeconds(0.3f);
        if(scene != Scene.GCLEAR)
        {
            //�������� �ʴ¿�����Ʈ ����
            DontDestroyOnLoad(gameObject);
        }
        


    }
    private void OnEnable()
    {
        //��üũ �ڷ�ƾ �Լ� ����
        StartCoroutine(CheckScene());
        StartCoroutine(CheckRoomClear());
    }

    IEnumerator CheckScene()
    {
        //���� �� ��� ����
        while (true)
        {
            //���� ��
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
            //ws ��ŭ ��� �� �簳
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
                    //Ŭ���� ���� Ȯ��
                    Room1_isClear();
                    
                    //�� 1 Ŭ���� ��
                    if (is1Clear == true && GameObject.FindGameObjectWithTag("CHEST") == null)
                    {
                        //���� ����
                        Instantiate(chest);
                        
                    }
                    break;
                case Scene.ROOM2:
                    if (keyImage[1].color == Color.white)
                    {
                        //������ũ ��Ȱ��ȭ
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
            //�����ð� ǥ��
            timeText.SetActive(true);
            timeText.GetComponent<Text>().text = "���� �ð�: " + (int)(15.0f - time);
            time += Time.deltaTime;
            //�ð� ���� �ѱ�� Ŭ����
            if (time >= timeClear)
            {
                canvas.SetActive(false);
                timeText.SetActive(false);
                scene = Scene.GCLEAR;
                print("Ŭ����");
                SceneManager.LoadScene("ClearScene");
                Clearcanvas.SetActive(true);
            }
            
        }
        else
            timeText.SetActive(false);




    }

    public void Room1_isClear()
    {
        //���� ��� ������
        if (enemyNum - killEnemyNum == 0)
        {
            is1Clear = true;
        }
       
    }
    //�κ� �̵� ��ư(���� ���� ��ư)
    public void StartGame()
    {
        SceneManager.LoadScene("Loby");
        canvas.SetActive(true);

    }
    //����� ��ư
    public void RestartGame()
    {
        //����ȭ�� �ҷ�����
        SceneManager.LoadScene("StartScene");
        //Ŭ����� ui ��Ȱ��ȭ
        Clearcanvas.SetActive(false);
        //���� �����ϴ� ����
        Destroy(this.gameObject);
        print(gameObject.name);
    }
}

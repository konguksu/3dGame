using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpen : MonoBehaviour
{
    //상자 뚜껑
    public GameObject chestLid;
    //열리는 속도
    Vector3 lidRot = new Vector3(-100, 0, 0);
    //텍스트
    public GameObject text;
    //게임매니저
    public GameManager gameManager;
    //상자 위치
    public Transform chestPos;
    //열쇠
    public GameObject key;
    //문
    public GameObject door;

    // 소리
    public AudioClip BoxSound;
    public AudioClip keySound;
    public AudioClip doorSound;

    //AudioSource 컴포넌트 저장
    AudioSource _audio;
    void Awake()
    {
        //컴포넌트 가져오기
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        door = GameObject.FindGameObjectWithTag("DOOR");
        _audio = GetComponent<AudioSource>();
        //상자 위치 가져오기
        chestPos = GameObject.Find("ChestPos").GetComponent<Transform>();
        transform.position = chestPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        //범위 안에 들어오면
        if (text.activeSelf == true)
        {
            //상자 뚜껑 열리게
            if (chestLid.transform.rotation.x <= 0 && chestLid.transform.rotation.x >= -0.5f)
            {
                //print(chestLid.transform.rotation.x);
                chestLid.transform.Rotate(lidRot * Time.deltaTime);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        //플레이어가 범위 안에 들어오고 키 획득 전이면
        if(other.tag == "PLAYER" && key.activeSelf == true)
        {
            //e 누르시오 텍스트 활성화
            text.SetActive(true);
            //e 누르면
            if(Input.GetKeyDown(KeyCode.E) == true)
            {
                //현재 씬에 따라 키 획득 표시 & 키 사라지게 && 텍스트 사라지게
                switch (gameManager.scene)
                {
                    case GameManager.Scene.ROOM1:
                        gameManager.keyImage[0].color = Color.white;
                        key.SetActive(false);
                        text.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f);//소리
                        _audio.PlayOneShot(doorSound, 0.5f); //문 소리
                        //문 열리게
                        door.GetComponent<Door>().clear = true;
                        break;
                    case GameManager.Scene.ROOM2:
                        gameManager.keyImage[1].color = Color.white;
                        text.SetActive(false);
                        key.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f);//소리
                        _audio.PlayOneShot(doorSound, 0.5f); //문 소리
                        //문 열리게
                        door.GetComponent<Door>().clear = true;
                        break;
                    case GameManager.Scene.ROOM3:
                        gameManager.keyImage[2].color = Color.white;
                        key.SetActive(false);
                        text.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f); //열쇠소리
                        _audio.PlayOneShot(doorSound, 0.5f); //문 소리
                        //문 열리게
                        door.GetComponent<Door>().clear = true;
                        break;
                }
                
            }
        }
    }
    //범위 벗어나면 텍스트 안보이게
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
            text.SetActive(false);
    }
    //상자열리는 소리
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER" && key.activeSelf == true)
        {
            _audio.PlayOneShot(BoxSound, 0.5f);
        }
    }
}

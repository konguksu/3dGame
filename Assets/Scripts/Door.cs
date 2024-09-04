using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //문
    public GameObject door;
    //열리는 속도
    Vector3 doorRot = new Vector3(0, -100, 0);
    //현재 방 클리어(열쇠까지) 여부
    public bool clear = false;

    //소리
    public AudioClip doorSound;
    //AudioSource 컴포넌트 저장
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.PlayOneShot(doorSound, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //시작시 문닫힘
        if (!clear)
        {
            //print("문: " + door.transform.rotation.y);
            
            if (door.transform.rotation.y >= 0.7f )
            {
               
                door.transform.Rotate(doorRot * Time.deltaTime);
            }
        }
        //열쇠 먹으면 문열림
        else
        {
            if(door.transform.rotation.y < 0.9999f )
            {
                door.transform.Rotate(-doorRot * Time.deltaTime);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //문 밖으로 나가면
        if(other.tag == "PLAYER")
        {
            //클리어해서 로비로 돌아감
            GameObject.Find("GameManager").GetComponent<GameManager>().clearToLoby = true;
            SceneManager.LoadScene("Loby");
        }
    }
}

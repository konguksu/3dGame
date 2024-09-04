using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossDoor : MonoBehaviour
{

    bool isFront;

    //문
    public GameObject doorObj;
    // 클리어 여부(열쇠 모두 모았는지)
    public bool clear = false;
    //열리는 속도
    Vector3 doorRot = new Vector3(0, -100, 0);
    //열쇠 이미지
    Image[] key;
    
    
    void Start()
    {
        //열쇠 이미지 컴포넌트 가져오기
        key = GameObject.Find("GameManager").GetComponent<GameManager>().keyImage;
    }

    // Update is called once per frame
    void Update()
    {
        //컴포넌트 가져오기
        isFront = gameObject.GetComponentInChildren<DoorColl>().isFront;
        //열쇠 세개를 모두 모았고 문 앞이면
        if (clear && isFront == true)
        {
            //print("문: " + doorObj.transform.rotation.y);
            if (doorObj.transform.rotation.y < 0.9999f)
            {
                doorObj.transform.Rotate(-doorRot * Time.deltaTime);
            }

        }
        //아니면 닫힘
        else
        {
            if (doorObj.transform.rotation.y >= 0.7f)
            {

                doorObj.transform.Rotate(doorRot * Time.deltaTime);
            }
        }

        //클리어 여부 확인(열쇠 3개 모았는지)
        if(key[0].color == Color.white && key[1].color == Color.white && key[2].color == Color.white)
        {
            clear = true;
        }
    }
    //문 안에 들어가면 보스 방으로 이동
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            SceneManager.LoadScene("Boss");

        }
    }

}

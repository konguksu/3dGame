using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobyDoor : MonoBehaviour
{
    //어느 방 문인지
    public enum Door
    {
        room1 = 0,
        room2 = 1,
        room3 = 2
    }
    public Door door;
    bool isFront;

    //문
    public GameObject doorObj;
    // 해당 방 클리어 여부
    public bool clear = false;
    //열리는 속도
    Vector3 doorRot = new Vector3(0, -100, 0);

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //컴포넌트 가져오기
        isFront = gameObject.GetComponentInChildren<DoorColl>().isFront;
        //클리어된 방이 아니라면 + 문 앞이라면 열림
        if (!clear && isFront == true)
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

        //클리어 여부 확인
        if(GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[(int)door].color == Color.white)
        {
            clear = true;
        }
    }
    //방 안에 들어가면 해당 방으로 이동
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            if(door == Door.room1)
            {
                print("방1");
                SceneManager.LoadScene("Room_1");
            }
            else if (door == Door.room2)
            {
                SceneManager.LoadScene("Room_2");
            }
            else if (door == Door.room3)
            {
                SceneManager.LoadScene("Room_3");
            }

        }
    }

}

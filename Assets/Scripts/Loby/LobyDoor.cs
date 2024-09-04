using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobyDoor : MonoBehaviour
{
    //��� �� ������
    public enum Door
    {
        room1 = 0,
        room2 = 1,
        room3 = 2
    }
    public Door door;
    bool isFront;

    //��
    public GameObject doorObj;
    // �ش� �� Ŭ���� ����
    public bool clear = false;
    //������ �ӵ�
    Vector3 doorRot = new Vector3(0, -100, 0);

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������Ʈ ��������
        isFront = gameObject.GetComponentInChildren<DoorColl>().isFront;
        //Ŭ����� ���� �ƴ϶�� + �� ���̶�� ����
        if (!clear && isFront == true)
        {
            //print("��: " + doorObj.transform.rotation.y);
            if (doorObj.transform.rotation.y < 0.9999f)
            {
                doorObj.transform.Rotate(-doorRot * Time.deltaTime);
            }

        }
        //�ƴϸ� ����
        else
        {
            if (doorObj.transform.rotation.y >= 0.7f)
            {

                doorObj.transform.Rotate(doorRot * Time.deltaTime);
            }
        }

        //Ŭ���� ���� Ȯ��
        if(GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[(int)door].color == Color.white)
        {
            clear = true;
        }
    }
    //�� �ȿ� ���� �ش� ������ �̵�
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            if(door == Door.room1)
            {
                print("��1");
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

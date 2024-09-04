using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossDoor : MonoBehaviour
{

    bool isFront;

    //��
    public GameObject doorObj;
    // Ŭ���� ����(���� ��� ��Ҵ���)
    public bool clear = false;
    //������ �ӵ�
    Vector3 doorRot = new Vector3(0, -100, 0);
    //���� �̹���
    Image[] key;
    
    
    void Start()
    {
        //���� �̹��� ������Ʈ ��������
        key = GameObject.Find("GameManager").GetComponent<GameManager>().keyImage;
    }

    // Update is called once per frame
    void Update()
    {
        //������Ʈ ��������
        isFront = gameObject.GetComponentInChildren<DoorColl>().isFront;
        //���� ������ ��� ��Ұ� �� ���̸�
        if (clear && isFront == true)
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

        //Ŭ���� ���� Ȯ��(���� 3�� ��Ҵ���)
        if(key[0].color == Color.white && key[1].color == Color.white && key[2].color == Color.white)
        {
            clear = true;
        }
    }
    //�� �ȿ� ���� ���� ������ �̵�
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            SceneManager.LoadScene("Boss");

        }
    }

}

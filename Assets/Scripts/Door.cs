using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //��
    public GameObject door;
    //������ �ӵ�
    Vector3 doorRot = new Vector3(0, -100, 0);
    //���� �� Ŭ����(�������) ����
    public bool clear = false;

    //�Ҹ�
    public AudioClip doorSound;
    //AudioSource ������Ʈ ����
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
        //���۽� ������
        if (!clear)
        {
            //print("��: " + door.transform.rotation.y);
            
            if (door.transform.rotation.y >= 0.7f )
            {
               
                door.transform.Rotate(doorRot * Time.deltaTime);
            }
        }
        //���� ������ ������
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
        //�� ������ ������
        if(other.tag == "PLAYER")
        {
            //Ŭ�����ؼ� �κ�� ���ư�
            GameObject.Find("GameManager").GetComponent<GameManager>().clearToLoby = true;
            SceneManager.LoadScene("Loby");
        }
    }
}

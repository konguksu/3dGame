using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpen : MonoBehaviour
{
    //���� �Ѳ�
    public GameObject chestLid;
    //������ �ӵ�
    Vector3 lidRot = new Vector3(-100, 0, 0);
    //�ؽ�Ʈ
    public GameObject text;
    //���ӸŴ���
    public GameManager gameManager;
    //���� ��ġ
    public Transform chestPos;
    //����
    public GameObject key;
    //��
    public GameObject door;

    // �Ҹ�
    public AudioClip BoxSound;
    public AudioClip keySound;
    public AudioClip doorSound;

    //AudioSource ������Ʈ ����
    AudioSource _audio;
    void Awake()
    {
        //������Ʈ ��������
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        door = GameObject.FindGameObjectWithTag("DOOR");
        _audio = GetComponent<AudioSource>();
        //���� ��ġ ��������
        chestPos = GameObject.Find("ChestPos").GetComponent<Transform>();
        transform.position = chestPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        //���� �ȿ� ������
        if (text.activeSelf == true)
        {
            //���� �Ѳ� ������
            if (chestLid.transform.rotation.x <= 0 && chestLid.transform.rotation.x >= -0.5f)
            {
                //print(chestLid.transform.rotation.x);
                chestLid.transform.Rotate(lidRot * Time.deltaTime);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        //�÷��̾ ���� �ȿ� ������ Ű ȹ�� ���̸�
        if(other.tag == "PLAYER" && key.activeSelf == true)
        {
            //e �����ÿ� �ؽ�Ʈ Ȱ��ȭ
            text.SetActive(true);
            //e ������
            if(Input.GetKeyDown(KeyCode.E) == true)
            {
                //���� ���� ���� Ű ȹ�� ǥ�� & Ű ������� && �ؽ�Ʈ �������
                switch (gameManager.scene)
                {
                    case GameManager.Scene.ROOM1:
                        gameManager.keyImage[0].color = Color.white;
                        key.SetActive(false);
                        text.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f);//�Ҹ�
                        _audio.PlayOneShot(doorSound, 0.5f); //�� �Ҹ�
                        //�� ������
                        door.GetComponent<Door>().clear = true;
                        break;
                    case GameManager.Scene.ROOM2:
                        gameManager.keyImage[1].color = Color.white;
                        text.SetActive(false);
                        key.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f);//�Ҹ�
                        _audio.PlayOneShot(doorSound, 0.5f); //�� �Ҹ�
                        //�� ������
                        door.GetComponent<Door>().clear = true;
                        break;
                    case GameManager.Scene.ROOM3:
                        gameManager.keyImage[2].color = Color.white;
                        key.SetActive(false);
                        text.SetActive(false);
                        _audio.PlayOneShot(keySound, 0.5f); //����Ҹ�
                        _audio.PlayOneShot(doorSound, 0.5f); //�� �Ҹ�
                        //�� ������
                        door.GetComponent<Door>().clear = true;
                        break;
                }
                
            }
        }
    }
    //���� ����� �ؽ�Ʈ �Ⱥ��̰�
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
            text.SetActive(false);
    }
    //���ڿ����� �Ҹ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER" && key.activeSelf == true)
        {
            _audio.PlayOneShot(BoxSound, 0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColl : MonoBehaviour
{
    //�� �տ� �ִ���
    public bool isFront = false;
    //�Ҹ�
    public AudioClip OpenSound;
    //AudioSource ������Ʈ ����
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�� �� ���� �� �÷��̾� ���Դ��� üũ
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            if(GetComponentInParent<LobyDoor>() != null && GetComponentInParent<LobyDoor>().clear == false)
            {
                _audio.PlayOneShot(OpenSound, 0.5f);
            }
            isFront = true;
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            isFront = false;
        }
    }
}

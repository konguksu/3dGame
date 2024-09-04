using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColl : MonoBehaviour
{
    //문 앞에 있는지
    public bool isFront = false;
    //소리
    public AudioClip OpenSound;
    //AudioSource 컴포넌트 저장
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
    //문 앞 범위 내 플레이어 들어왔는지 체크
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

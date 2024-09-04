using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike2 : MonoBehaviour
{
    public GameObject spike;

    Vector3 Pos;//현재 위치
    float delta = 2.0f;//상하 이동 최대값
    float speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //위치 가져오기
        Pos = spike.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //키 먹기 전까지 반복
        if (GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[1].color != Color.white)
        {
            spikeMove();
        }
        else
            Destroy(this);


    }
    
    void spikeMove()
    {
        //상하 반복이동
        Vector3 v = Pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        spike.transform.position = v;
    }
    
}

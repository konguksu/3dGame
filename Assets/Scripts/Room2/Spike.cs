using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject spike;

    Vector3 Pos;//현재 위치
    float delta = 2.0f;//상하 이동 최대값
    float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //위치 가져오기
        Pos = spike.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //키 먹기 전까지
        if (GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[1].color != Color.white)
        {
            //상하 반복이동
            Vector3 v = Pos;
            v.y += delta * Mathf.Sin(Time.time * speed);
            spike.transform.position = v;
        }
        //키 먹었으면 없애기
        else
            Destroy(this);
        
        
    }
    
    
}

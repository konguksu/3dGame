using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    Vector3 Pos;//현재 위치
    float delta = 17.0f;//좌우 이동 최대값
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1.0f, 3.0f);
        //위치 가져오기
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //상하 반복이동
        Vector3 v = Pos;
        v.z += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        
    }
    
    
}

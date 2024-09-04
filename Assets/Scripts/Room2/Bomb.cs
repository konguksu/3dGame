using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    Vector3 Pos;//���� ��ġ
    float delta = 17.0f;//�¿� �̵� �ִ밪
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1.0f, 3.0f);
        //��ġ ��������
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //���� �ݺ��̵�
        Vector3 v = Pos;
        v.z += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        
    }
    
    
}

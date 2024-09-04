using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject spike;

    Vector3 Pos;//���� ��ġ
    float delta = 2.0f;//���� �̵� �ִ밪
    float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //��ġ ��������
        Pos = spike.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Ű �Ա� ������
        if (GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[1].color != Color.white)
        {
            //���� �ݺ��̵�
            Vector3 v = Pos;
            v.y += delta * Mathf.Sin(Time.time * speed);
            spike.transform.position = v;
        }
        //Ű �Ծ����� ���ֱ�
        else
            Destroy(this);
        
        
    }
    
    
}

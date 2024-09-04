using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike2 : MonoBehaviour
{
    public GameObject spike;

    Vector3 Pos;//���� ��ġ
    float delta = 2.0f;//���� �̵� �ִ밪
    float speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //��ġ ��������
        Pos = spike.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Ű �Ա� ������ �ݺ�
        if (GameObject.Find("GameManager").GetComponent<GameManager>().keyImage[1].color != Color.white)
        {
            spikeMove();
        }
        else
            Destroy(this);


    }
    
    void spikeMove()
    {
        //���� �ݺ��̵�
        Vector3 v = Pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        spike.transform.position = v;
    }
    
}

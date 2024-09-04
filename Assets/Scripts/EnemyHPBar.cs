using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    // ĵ���� �������ϴ� ī�޶�
    Camera uiCamera;
    //UI�� �ֻ��� ĵ����
    Canvas canvas;
    //�θ� RectTransform ������Ʈ
    RectTransform rectParent;
    //�ڽ� RectTransform ������Ʈ
    RectTransform rectHP;
    //HPBar �̹��� ��ġ ���� ������
    public Vector3 offset = Vector3.zero;
    //������ ����� Transform ������Ʈ
    public Transform targetTr;
    void Start()
    {
        //������Ʈ ����&�Ҵ�
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //���� ��ǥ -> ��ũ�� ��ǥ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        //ī�޶� ���� ����(180�� ȸ��)�� �� ��ǥ ����
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }
        //RectTransform �� ���޹��� ����
        Vector2 localPos = Vector2.zero;
        //��ũ�� ��ǥ�� RectTransform ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectParent, screenPos, uiCamera, out localPos);
        //���� ������ �̹����� ��ġ ����
        rectHP.localPosition = localPos;
    }
}

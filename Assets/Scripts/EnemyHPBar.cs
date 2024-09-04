using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    // 캔버스 렌더링하는 카메라
    Camera uiCamera;
    //UI용 최상위 캔버스
    Canvas canvas;
    //부모 RectTransform 컴포넌트
    RectTransform rectParent;
    //자신 RectTransform 컴포넌트
    RectTransform rectHP;
    //HPBar 이미지 위치 조절 오프셋
    public Vector3 offset = Vector3.zero;
    //추적할 대상의 Transform 컴포넌트
    public Transform targetTr;
    void Start()
    {
        //컴포넌트 추출&할당
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //월드 좌표 -> 스크린 좌표
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        //카메라 뒷쪽 영역(180도 회전)일 때 좌표 보정
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }
        //RectTransform 값 전달받을 변수
        Vector2 localPos = Vector2.zero;
        //스크린 좌표를 RectTransform 기준 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectParent, screenPos, uiCamera, out localPos);
        //생명 게이지 이미지의 위치 변경
        rectHP.localPosition = localPos;
    }
}

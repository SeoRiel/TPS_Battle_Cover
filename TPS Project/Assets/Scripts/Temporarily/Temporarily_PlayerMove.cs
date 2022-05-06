using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 수정 완료 후 PlayerMove로 변경
public class Temporarily_PlayerMove : MonoBehaviour
{

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // 회전 체크 -> 현재 바라보고 있는 곳 기준으로 직진 이동 : nowRotation
        // - 마우스 이동하면 회전
        // - 마우스 이동하지 않으면 정지

        // W
        // 회전 체크 -> 현재 바라보고 있는 곳 기준으로 회전 : nowRotation
        // 직진 이동

        // A
        // 회전 : -45.0f -> 현재 바라보고 있는 각도 기준으로 회전 : nowRotation - 45.0f
        // 이동 : forward + left

        // S
        // 회전 : 45.0f -> nowRotation + 45.0f;
        // 이동 : forward + right
        // D
    }

    private void LateUpdate()
    {
        
    }
}

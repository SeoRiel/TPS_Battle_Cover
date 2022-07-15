using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 마우스 이동 상태
    // 마우스 위치
    // https://robotree.tistory.com/7

    // 이동 속도 조절
    [Header("Player setting")]
    [Header("Move Speed")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;

    // 회전 속도 조절
    [Header("Rotate Speed")]
    [SerializeField] private float keyboardRotate = 1.0f;
    [SerializeField] private float mouseRotate = 1.0f;

    private void Update()
    {
        Input.GetAxis("Mouse X");
        Input.GetAxis("Mouse Y");
    }
}

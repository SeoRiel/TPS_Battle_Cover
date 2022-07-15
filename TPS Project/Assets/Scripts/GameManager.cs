using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���콺 �̵� ����
    // ���콺 ��ġ
    // https://robotree.tistory.com/7

    // �̵� �ӵ� ����
    [Header("Player setting")]
    [Header("Move Speed")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;

    // ȸ�� �ӵ� ����
    [Header("Rotate Speed")]
    [SerializeField] private float keyboardRotate = 1.0f;
    [SerializeField] private float mouseRotate = 1.0f;

    private void Update()
    {
        Input.GetAxis("Mouse X");
        Input.GetAxis("Mouse Y");
    }
}

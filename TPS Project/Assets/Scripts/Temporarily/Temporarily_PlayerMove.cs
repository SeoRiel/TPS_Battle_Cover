using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ϸ� �� PlayerMove�� ����
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
        // ȸ�� üũ -> ���� �ٶ󺸰� �ִ� �� �������� ���� �̵� : nowRotation
        // - ���콺 �̵��ϸ� ȸ��
        // - ���콺 �̵����� ������ ����

        // W
        // ȸ�� üũ -> ���� �ٶ󺸰� �ִ� �� �������� ȸ�� : nowRotation
        // ���� �̵�

        // A
        // ȸ�� : -45.0f -> ���� �ٶ󺸰� �ִ� ���� �������� ȸ�� : nowRotation - 45.0f
        // �̵� : forward + left

        // S
        // ȸ�� : 45.0f -> nowRotation + 45.0f;
        // �̵� : forward + right
        // D
    }

    private void LateUpdate()
    {
        
    }
}

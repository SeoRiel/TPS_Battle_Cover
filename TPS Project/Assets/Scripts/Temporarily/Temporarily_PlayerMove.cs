using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ϸ� �� PlayerMove�� ����
public class Temporarily_PlayerMove_1 : MonoBehaviour
{
    [Header("Input is character move speed")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;

    [Header("Input is character rotate speed")]
    [SerializeField] private float mouseRotate = 1.0f;
    [SerializeField] private float keyboardRotate = 1.0f;

    private CharacterController playerController;
    private Animator playerAnimator;

    private Vector3 direction;

    private float mouseX;
    private float mouseY;

    private float speed;
    private float inputState;

    // Setting player position & rotation
    private float y;

    private bool moveState;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        direction = Vector3.zero;
        inputState = 0.0f;
        speed = walkSpeed;
        moveState = false;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        GetMousePosition();

        // ȸ�� üũ -> ���� �ٶ󺸰� �ִ� �� �������� ���� �̵� : nowRotation
        // - ���콺 �̵��ϸ� ȸ��
        // - ���콺 �̵����� ������ ����

        // W
        if(Input.GetKey(KeyCode.W))
        {
            // �̵� ����
            DashCheck();

            // ȸ�� üũ -> ���� �ٶ󺸰� �ִ� �� �������� ȸ�� : nowRotation
            // ���� �̵�
        }
        // A
        else if(Input.GetKey(KeyCode.A))
        {
            // �̵� ����
            DashCheck();

            // ȸ�� : -45.0f -> ���� �ٶ󺸰� �ִ� ���� �������� ȸ�� : nowRotation - 45.0f
            // �̵� : forward + left
        }
        // S
        else if(Input.GetKey(KeyCode.S))
        {
            // �̵� ����
            DashCheck();

            // ȸ�� : 45.0f -> nowRotation + 45.0f
            // �̵� : forward + left
        }
        // D
        else if (Input.GetKey(KeyCode.D))
        {
            // �̵� ����
            DashCheck();

            // ȸ�� : 90.0f -> nowRotation + 90.0f
            // �̵� : Vector3.forward + Vector3.right
        }
    }

    private void LateUpdate()
    {
        
    }

    private void GetMousePosition()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    private bool MoveState(bool check)
    {


        return check;
    }

    private void DashCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("Dash", true);

            speed = dashSpeed;

            playerAnimator.SetBool("WalkFront", false);
        }
        else
        {
            playerAnimator.SetBool("WalkFront", true);

            speed = walkSpeed;

            playerAnimator.SetBool("Dash", false);
        }
    }

    private void PlayerRotate()
    {

    }
}

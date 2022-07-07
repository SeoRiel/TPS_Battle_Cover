using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 수정 완료 후 PlayerMove로 변경
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

        // 회전 체크 -> 현재 바라보고 있는 곳 기준으로 직진 이동 : nowRotation
        // - 마우스 이동하면 회전
        // - 마우스 이동하지 않으면 정지

        // W
        if(Input.GetKey(KeyCode.W))
        {
            // 이동 상태
            DashCheck();

            // 회전 체크 -> 현재 바라보고 있는 곳 기준으로 회전 : nowRotation
            // 직진 이동
        }
        // A
        else if(Input.GetKey(KeyCode.A))
        {
            // 이동 상태
            DashCheck();

            // 회전 : -45.0f -> 현재 바라보고 있는 각도 기준으로 회전 : nowRotation - 45.0f
            // 이동 : forward + left
        }
        // S
        else if(Input.GetKey(KeyCode.S))
        {
            // 이동 상태
            DashCheck();

            // 회전 : 45.0f -> nowRotation + 45.0f
            // 이동 : forward + left
        }
        // D
        else if (Input.GetKey(KeyCode.D))
        {
            // 이동 상태
            DashCheck();

            // 회전 : 90.0f -> nowRotation + 90.0f
            // 이동 : Vector3.forward + Vector3.right
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

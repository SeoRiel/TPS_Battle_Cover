using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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

    private void Update()
    {
        GetMousePosition();
        // Debug.Log("Mouse X : " + mouseX);

        if (Input.GetKey(KeyCode.W))
        {
            // y = 0.0f;
            PlayerRotate(0.0f);

            KeyInput(true);
            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                // y = -45.0f;
                PlayerRotate(-45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // y = 45.0f;
                PlayerRotate(45.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // y = -90.0f;
            PlayerRotate(-90.0f);

            KeyInput(true);
            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                // y = -45.0f;
                PlayerRotate(-45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                // y = -135.0f;
                PlayerRotate(-135.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // y = 180.0f;
            PlayerRotate(180.0f);

            KeyInput(true);
            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.D))
            {
                // y = 135.0f;
                PlayerRotate(135.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                // y = 225.0f;
                PlayerRotate(225.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // y = 90.0f;
            PlayerRotate(90.0f);

            KeyInput(true);
            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                // y = 45.0f;
                PlayerRotate(45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                // y = 180.0f;
                PlayerRotate(180.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else
        {
            KeyInput(false);
            playerAnimator.SetBool("WalkFront", false);
            playerAnimator.SetBool("Dash", false);
        }

        // if (playerController.isGrounded)
        PlayerRotate(this.transform.rotation.y);

        // direction에 Vector3를 할당해서 넣지말고 즉각 적용하는 방식으로 변경
        playerController.Move(this.transform.TransformDirection(direction.normalized) * speed * Time.deltaTime * inputState);
    }

    // 마우스 값 저장
    private void GetMousePosition()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    // 애니메이션 변경
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

    //
    private void InputRotate(float inputY, bool keyState)
    {
        moveState = keyState;

        if(moveState)
        {
            inputState = 1.0f;
            playerController.transform.Rotate(Vector3.up, mouseX * mouseRotate);
        }
        else
        {
            inputState = 0.0f;
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, Quaternion.Euler(0.0f, inputY, 0.0f), Time.deltaTime * keyboardRotate);
        }
    }

    private void PlayerMovement(Vector3 way)
    {
        // 마우스 미사용


        // 마우스 사용

        playerController.Move(this.transform.TransformDirection(way.normalized) * speed * Time.deltaTime * inputState);
    }


    // KeyInput과 PlayerRotate 함수 통일
    private void KeyInput(bool keyState)
    {
        moveState = keyState;

        if(moveState)
        {
            inputState = 1.0f;
        }
        else
        {
            inputState = 0.0f;
        }
    }

    // 키 입력 및 마우스 입력을 통한 회전
    private void PlayerRotate(float inputY)
    {
        if (moveState)
        {
            playerController.transform.Rotate(Vector3.up, mouseX * mouseRotate);
        }
        else
        {
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, Quaternion.Euler(0.0f, inputY, 0.0f), Time.deltaTime * keyboardRotate);
        }
    }

    // getter
    public bool GetMoveState()
    {
        return moveState;
    }

    public float GetMousePositionX()
    {
        return mouseX;
    }

    public float GetMousePositionY()
    {
        return mouseY;
    }
}
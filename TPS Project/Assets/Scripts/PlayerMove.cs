using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Input is character move speed")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;

    [Header("Input is character rotate speed")]
    [SerializeField] private float mouseSpeed = 1.0f;
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
        Debug.Log("Mouse X : " + mouseX);

        if (Input.GetKey(KeyCode.W))
        {
            KeyboardInput(true);
            PlayerRotate(0.0f);

            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                KeyboardInput(true);
                PlayerRotate(-45.0f);

                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                KeyboardInput(true);
                PlayerRotate(45.0f);

                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            KeyboardInput(true);
            PlayerRotate(-90.0f);

            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                KeyboardInput(true);
                PlayerRotate(-45.0f);

                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                KeyboardInput(true);
                PlayerRotate(-135.0f);

                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            KeyboardInput(true);
            PlayerRotate(180.0f);

            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.D))
            {
                KeyboardInput(true);
                // PlayerRotate(135.0f);
                PlayerRotate(-45.0f);

                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                KeyboardInput(true);
                // PlayerRotate(225.0f);
                PlayerRotate(45.0f);

                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            KeyboardInput(true);
            PlayerRotate(90.0f);

            DashCheck();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                KeyboardInput(true);
                PlayerRotate(-45.0f);

                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                KeyboardInput(true);
                PlayerRotate(90.0f);

                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else
        {
            KeyboardInput(false);
            // PlayerRotate(this.transform.rotation.y);

            playerAnimator.SetBool("WalkFront", false);
            playerAnimator.SetBool("Dash", false);
        }

        // if (playerController.isGrounded)

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
    private void KeyboardInput(bool isKeyInput)
    {
        moveState = isKeyInput;

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
    // 너무 시간 끄니까 나중에 수정...;;
    private void PlayerRotate(float inputY)
    {
        Quaternion keyRotation = Quaternion.Euler(0.0f, inputY, 0.0f);

        // 기존 로직
        //if (inputState >= 0.0f)
        if (mouseX == 0)
        {
            playerController.transform.rotation = Quaternion.Slerp(this.transform.rotation, keyRotation, Time.deltaTime * keyboardRotate);
        }
        else
        {
            playerController.transform.Rotate(Vector3.up, mouseX * mouseSpeed);
        }

        // 변경 로직 - 1 -
        //if (mouseX == 0.0f)
        //{
        //    if(playerController.transform.rotation.y != inputY)
        //    {
        //        // playerController.transform.Rotate(0.0f, inputY, 0.0f);
        //        // playerController.transform.rotation = Quaternion.Slerp(this.transform.rotation, keyRotation, keyboardRotate * Time.deltaTime);

        //        // keyRotation = Quaternion.Euler(0.0f, inputY, 0.0f);
        //    }

        //}
        //else
        //{
        //    keyRotation = Quaternion.Euler(0.0f, inputY + (mouseX * mouseSpeed), 0.0f);
        //    // playerController.transform.Rotate(Vector3.up, mouseX * mouseSpeed);
        //}

        // 변경 로직 - 2 -
        //if (mouseX != 0)
        //{
        //    keyRotation.y += (mouseX * mouseSpeed);
        //}

        //playerController.transform.rotation = Quaternion.Slerp(this.transform.rotation, keyRotation, keyboardRotate * Time.deltaTime);
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
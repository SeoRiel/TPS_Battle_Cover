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
    // private float y;

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

        if (Input.GetKey(KeyCode.W))
        {
            KeyboardInput(true);
            PlayerRotate(0.0f);

            DashMove();

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

            DashMove();

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

            DashMove();

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.D))
            {
                KeyboardInput(true);
                PlayerRotate(135.0f);

                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                KeyboardInput(true);
                PlayerRotate(45.0f);

                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            KeyboardInput(true);
            PlayerRotate(90.0f);

            DashMove();

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

            playerAnimator.SetBool("WalkFront", false);
            playerAnimator.SetBool("Dash", false);
        }

        playerController.Move(this.transform.TransformDirection(direction.normalized) * speed * Time.deltaTime * inputState);
    }

    // -
    private void GetMousePosition()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    // 애니메이션 변경
    private void DashMove()
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

    private bool KeyboardInput(bool isKeyInput)
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

        return moveState;
    }

    private void PlayerRotate(float inputY)
    {
        // https://wergia.tistory.com/230

        // 키보드 회전
        Quaternion keyRotation = Quaternion.Euler(0.0f, this.transform.rotation.y + inputY, 0.0f);
        // playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, keyRotation, keyboardRotate * Time.deltaTime);
        // playerController.transform.Rotate(Vector3.up, mouseSpeed * mouseX);
        
        if (Input.anyKey)
        {
            if(mouseX == 0)
            {
                playerController.transform.Rotate(new Vector3(0.0f, inputY, 0.0f) * keyboardRotate * Time.deltaTime, Space.Self);
                // playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, keyRotation, keyboardRotate * Time.deltaTime);
            }
            else
            {
                // 마우스 회전
                playerController.transform.Rotate(Vector3.up, mouseSpeed * mouseX);
            }
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
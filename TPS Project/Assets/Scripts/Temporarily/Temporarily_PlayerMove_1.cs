using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporarily_PlayerMove : MonoBehaviour
{
    [Header("Input is character move speed")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;

    [Header("Input is character rotate speed")]
    [SerializeField] private float mouseSpeed = 1.0f;
    [SerializeField] private float keyboardSpeed = 1.0f;

    private CharacterController playerController;
    private Animator playerAnimator;

    private Vector3 direction;
    private Quaternion keyRotation;

    private float mouseX;
    private float mouseY;

    private float speed;

    private float inputState;
    private bool moveState;
    private bool mouseMoveX;
    private bool mouseMoveY;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        direction = Vector3.zero;
        keyRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        speed = walkSpeed;
        inputState = 0.0f;

        moveState = mouseMoveX = mouseMoveY = false;
    }

    private void Update()
    {
        GetMousePosition();

        KeyboardMove();
        // MouseMove();
    }

    private void GetMousePosition()
    {
        mouseX = Input.GetAxis("Mouse X");
        if (0 < mouseX || mouseX < 0)
        {
            mouseMoveX = true;
        }
        else
        {
            mouseMoveX = false;
        }

        mouseY = Input.GetAxis("Mouse Y");
        if (0 < mouseY || mouseY < 0)
        {
            mouseMoveY = true;
        }
        else
        {
            mouseMoveY = false;
        }
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

    // 
    private void MouseMove()
    {
        if (mouseX == 0)
        {
            inputState = 0.0f;
        }
        else if (0 < mouseX)
        {
            playerController.transform.Rotate(Vector3.up, mouseSpeed * 1.0f);
        }
        else if (mouseX < 0)
        {
            playerController.transform.Rotate(Vector3.up, mouseSpeed * -1.0f);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            direction = Vector3.forward;
        }

        playerController.Move(this.transform.TransformDirection(direction.normalized) * speed * Time.deltaTime * inputState);
    }

    private void KeyboardMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            DashMove();
            KeyboardInput(true);

            KeyboarRotate(0.0f);
            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                KeyboarRotate(-45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                KeyboarRotate(45.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            KeyboarRotate(-90.0f);

            DashMove();
            KeyboardInput(true);
            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                KeyboarRotate(-45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                KeyboarRotate(-135.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            KeyboarRotate(-180.0f);

            DashMove();
            KeyboardInput(true);
            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.D))
            {
                KeyboarRotate(135.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                KeyboarRotate(45.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            KeyboarRotate(90.0f);

            DashMove();
            KeyboardInput(true);
            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.W))
            {
                KeyboarRotate(-45.0f);
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                KeyboarRotate(90.0f);
                direction = Vector3.forward;
            }

            playerAnimator.SetBool("WalkFront", true);
        }
        else
        {
            playerAnimator.SetBool("WalkFront", false);
            playerAnimator.SetBool("Dash", false);
            KeyboardInput(false);
        }

        playerController.Move(this.transform.TransformDirection(direction.normalized) * speed * Time.deltaTime * inputState);
    }

    private void KeyboarRotate(float value)
    {
        // playerController.transform.Rotate(Vector3.up, mouseX * mouseSpeed);

        keyRotation = Quaternion.Euler(0.0f, value, 0.0f);
        playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, keyRotation, keyboardSpeed * Time.deltaTime);
    }

    private bool KeyboardInput(bool isKeyInput)
    {
        moveState = isKeyInput;

        if (moveState)
        {
            inputState = 1.0f;
        }
        else
        {
            inputState = 0.0f;
        }

        return moveState;
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
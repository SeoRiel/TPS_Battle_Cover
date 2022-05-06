using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Save : MonoBehaviour
{

    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;
    [SerializeField] private float mouseRotate = 1.0f;
    [SerializeField] private float keyboardRotate = 1.0f;

    private CharacterController playerController;
    private Animator playerAnimator;

    private Vector3 direction = Vector3.zero;

    private float mouseX;
    private float mouseY;

    private float axis;
    private float speed;

    // setting player position & rotation 
    private float x;
    private float y;

    private bool moving = false;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        speed = 1.0f;
        axis = 0.0f;
    }

    private void Update()
    {
        speed = walkSpeed;

        GetMousePosition();

        if (Input.GetKey(KeyCode.W))
        {
            // 이동 중에 마우스 회전 가능
            // 마우스 커서 이동이 진행되면 캐릭터 회전도 같이 진행
            // 마우스 커서가 이동하지 않을 경우, 키보드 방향키 회전만 적용
            y = 0.0f;
            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                y = -45.0f;
                direction += Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                y = 45.0f;
                direction += Vector3.right;
            }

            DashCheck();

            DefineMovingAxis(1.0f, true);

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            y = -90.0f;
            direction = Vector3.left;

            if (Input.GetKey(KeyCode.W))
            {
                y = -45.0f;
                direction += Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                y = -135.0f;
                direction += Vector3.back;
            }

            DashCheck();

            DefineMovingAxis(1.0f, true);

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = 180.0f;
            direction = Vector3.back;

            if (Input.GetKey(KeyCode.D))
            {
               y = 135.0f;
               direction += Vector3.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
               y = -135.0f;
               direction += Vector3.left;
            }

            DashCheck();

            DefineMovingAxis(1.0f, true);

            playerAnimator.SetBool("WalkFront", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            y = 90.0f;
            direction = Vector3.right;

            if (Input.GetKey(KeyCode.W))
            {
                y = 45.0f;
                direction += Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                y = 180.0f;
                direction += Vector3.back;
            }

            DashCheck();

            DefineMovingAxis(1.0f, true);

            playerAnimator.SetBool("WalkFront", true);
        }
        else
        {
            DefineMovingAxis(0.0f, false);

            playerAnimator.SetBool("WalkFront", false);
            playerAnimator.SetBool("Dash", false);
        }

        //if (playerController.isGrounded)
        //{
        //    playerController.Move(direction * axis * speed * Time.deltaTime);
        //}

        PlayerRotate(x, y);
        playerController.Move(direction.normalized * axis * speed * Time.deltaTime);

    }

    // Function
    private void GetMousePosition()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
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

    private bool DefineMovingAxis(float defineAxis, bool moveCheck)
    {
        axis = defineAxis;
        moving = moveCheck;

        return true;
    }

    private void PlayerRotate(float x, float y)
    {
        //if (mouseX >= 0.0f)
        //{
        //    playerController.transform.Rotate(Vector3.up, mouseX * mouseRotate);
        //}
        //else
        //{
        //    playerController.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(x, y, 0.0f), keyboardRotate * Time.deltaTime);
        //}

        playerController.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0.0f, y, 0.0f), keyboardRotate * Time.deltaTime);
    }

    private void PlayerMoveAndRotate()
    {
        if (moving)
        {
            direction += (Vector3.right * mouseRotate);
        }
        else
        {
        }

        playerController.Move(direction.normalized * axis * speed * Time.deltaTime);
    }

    // getter
    public bool GetMoveState()
    {
        return moving;
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

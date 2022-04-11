using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController playerController;
    private Transform playerTransform;
    private Animator playerAnimator;

    private Vector3 direction = Vector3.zero;

    private float directionSpeed = 1.0f;

    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 1.0f;

    private float axis = 0.0f;
    private float rotateX;
    private float rotateY;

    private bool moving = false;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        directionSpeed = walkSpeed;
        GetMousePosition();

        if(playerController.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                DashCheck();
                PlayerRotate(0.0f, 0.0f, 0.0f);

                direction = Vector3.forward;

                if (Input.GetKey(KeyCode.D))
                {
                    PlayerRotate(0.0f, 90.0f, 0.0f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    PlayerRotate(0.0f, -90.0f, 0.0f);
                }

                DefineMovingAxis(1.0f, true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                DashCheck();
                PlayerRotate(0.0f, -90.0f, 0.0f);

                direction = Vector3.forward;

                if (Input.GetKey(KeyCode.W))
                {
                    PlayerRotate(0.0f, 90.0f, 0.0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    PlayerRotate(0.0f, -90.0f, 0.0f);
                }

                DefineMovingAxis(1.0f, true);
                playerAnimator.SetBool("WalkFront", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                DashCheck();
                PlayerRotate(0.0f, 180.0f, 0.0f);

                direction = Vector3.forward;

                if (Input.GetKey(KeyCode.D))
                {
                    PlayerRotate(0.0f, 90.0f, 0.0f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    PlayerRotate(0.0f, -90.0f, 0.0f);
                }

                DefineMovingAxis(1.0f, true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                DashCheck();

                PlayerRotate(0.0f, 90.0f, 0.0f);
                direction = Vector3.forward;

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    PlayerRotate(0.0f, 90.0f, 0.0f);
                }

                DefineMovingAxis(1.0f, true);
                playerAnimator.SetBool("WalkFront", true);
            }
            else
            {
                DefineMovingAxis(1.0f, true);

                playerAnimator.SetBool("WalkFront", false);
                playerAnimator.SetBool("Dash", false);
            }

            playerController.Move(direction * axis * directionSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        
    }

    // Function
    private void GetMousePosition()
    {
        rotateX = Input.GetAxis("Mouse X");
        rotateY = Input.GetAxis("Mouse Y");
    }

    private void PlayerRotate(float x, float y , float z)
    {
        if(moving)
        {
            playerTransform.Rotate(Vector3.up, rotateY * directionSpeed);
        }
        else
        {
            playerTransform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(x, y, z), Time.deltaTime * rotateSpeed);
        }
    }

    private void DashCheck()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("Dash", true);

            directionSpeed = dashSpeed;

            playerAnimator.SetBool("WalkFront", false);
        }
        else
        {
            playerAnimator.SetBool("WalkFront", true);

            directionSpeed = walkSpeed;

            playerAnimator.SetBool("Dash", false);
        }
    }

    private bool DefineMovingAxis(float defineAxis, bool moveCheck)
    {
        axis = defineAxis;
        moving = moveCheck;

        return true;
    }

    // getter
    public bool GetMoveState()
    {
        return moving;
    }

    public float GetMousePositionX()
    {
        return rotateX;
    }

    public float GetMousePositionY()
    {
        return rotateY;
    }
}

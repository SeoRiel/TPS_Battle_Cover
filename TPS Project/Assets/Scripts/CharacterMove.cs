using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Input is character move speed")]
    [SerializeField] private float originSpeed = 1.0f;
    [SerializeField] private float rotateSpeed = 1.0f;

    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float moveAxis = 1.0f;
    
    private float rotateX;
    private float rotateY;

    private Vector3 direction;

    private Transform characterTransform;
    private Animator characterAnimator;

    private void Awake()
    {
        characterTransform = GetComponent<Transform>();
        characterAnimator = GetComponent<Animator>();

        moveSpeed = originSpeed;
    }

    private void Update()
    {
        moveSpeed = 1.0f;
        rotateX = Input.GetAxis("Mouse X");
        rotateY = Input.GetAxis("Mouse Y");

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

            moveAxis = 1.0f;
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

            moveAxis = 1.0f;

            characterAnimator.SetBool("WalkFront", true);
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
            else if(Input.GetKey(KeyCode.A))
            {
                PlayerRotate(0.0f, -90.0f, 0.0f);
            }

            moveAxis = 1.0f;
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

            moveAxis = 1.0f;

            characterAnimator.SetBool("WalkFront", true);
        }
        else
        {
            moveAxis = 0.0f;

            characterAnimator.SetBool("WalkFront", false);
            characterAnimator.SetBool("Dash", false);
        }

        characterTransform.Translate(direction.normalized * moveAxis * moveSpeed * Time.deltaTime, Space.Self);
    }

    //private void OnCollisionExit(Collision medium)
    //{
    //    PlayerRotate(this.transform.rotation.x, 0.0f, 0.0f);
    //}

    private void PlayerRotate(float x, float y, float z)
    {
        if (moveAxis >= 1.0f)
        {
            characterTransform.Rotate(Vector3.up, rotateX * rotateSpeed);
        }
        else
        {
            characterTransform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x, y, z), Time.deltaTime * rotateSpeed);
        }
    }

    private void DashCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterAnimator.SetBool("Dash", true);

            moveSpeed = 5.0f;

            characterAnimator.SetBool("WalkFront", false);
        }
        else
        {
            characterAnimator.SetBool("WalkFront", true);

            moveSpeed = 1.0f;

            characterAnimator.SetBool("Dash", false);
        }
    }

    // getter
    public float GetMoveAxis()
    {
        return moveAxis;
    }

    public float GetMouseX()
    {
        return rotateX;
    }

    public float GetMouseY()
    {
        return rotateY;
    }
}
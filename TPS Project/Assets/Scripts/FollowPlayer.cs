using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Add is follow target for camera anchor")]
    [SerializeField] private Transform target;

    [Header("Input is character anchor position")]
    [SerializeField] private float x = 0.0f;
    [SerializeField] private float y = 0.0f;
    [SerializeField] private float z = 0.0f;

    [SerializeField] private float movingDamp = 1.0f;
    [SerializeField] private float rotateSpeed = 1.0f;

    [Header("Reference to PlayerMove Script")]
    [SerializeField] private GameObject player;

    [Header("Input Aim Setting")]
    [SerializeField] private float aimDistance = 1.0f;
    [SerializeField] private float baseFOV = 60.0f;
    [SerializeField] private float aimFOV = 1.0f;

    [SerializeField] private Camera cam;

    private Transform playerPosition;

    private bool moveCheck;
    private bool usingAim;

    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        moveCheck = false;
        usingAim = false;
    }

    private void Update()
    {
        moveCheck = player.GetComponent<PlayerMove>().GetMoveState();
        mouseX = player.GetComponent<PlayerMove>().GetMousePositionX();
        mouseY = player.GetComponent<PlayerMove>().GetMousePositionY();

        if (Input.GetMouseButton(1))
        {
            // Debug.Log("Press right button");
            usingAim = true;
        }
        else
        {
            // Debug.Log("Take off right button");
            usingAim = false;
        }
    }

    private void LateUpdate()
    {
        this.transform.position += new Vector3(x, y, z);
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, movingDamp);

        if (usingAim)
        {
            // Debug.Log("Cross hair activate");
            this.transform.position += new Vector3(aimDistance, 0.0f, 0.0f);
            cam.fieldOfView = aimFOV;
        }
        else
        {
            cam.fieldOfView = baseFOV;
        }


        if (this.transform.rotation.x <= 90.0f)
        {
            this.transform.Rotate(Vector3.up, mouseX * rotateSpeed);
            this.transform.Rotate(Vector3.right, mouseY * rotateSpeed);
        }
        else if (this.transform.rotation.x >= 90.0f)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(mouseX * rotateSpeed, 90.0f, 0.0f), Time.deltaTime * rotateSpeed);
        }
    }
}

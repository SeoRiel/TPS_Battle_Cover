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

    [Header("Input Aim distance")]
    [SerializeField] private float aimDistance = 1.0f;

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
        }
    }

    private void LateUpdate()
    {
        this.transform.position += new Vector3(x, y, z);
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, movingDamp);
        
        //if (usingAim)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0.0f, 0.0f, aimDistance), Time.deltaTime);
        //    // this.transform.position = Vector3.Lerp(player.transform.position, new Vector3(0.0f, 0.0f, aimDistance), Time.deltaTime);
        //}
        //else
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, target.position, movingDamp);
        //    // this.transform.position = Vector3.Lerp(player.transform.position, target.position, movingDamp);
        //}

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

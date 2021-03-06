using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporarily_FollowPlayer : MonoBehaviour
{
    [Header("Add is follow target for camera anchor")]
    [SerializeField] private Transform target;

    [Header("Input is character anchor position")]
    [SerializeField] private float x = 0.0f;
    [SerializeField] private float y = 0.0f;
    [SerializeField] private float z = 0.0f;

    [SerializeField] private float movingDamp = 1.0f;
    [SerializeField] private float rotateSpeed = 1.0f;

    [Header("Reference to CharacterMove Script")]
    public GameObject player;

    private bool moveCheck;
    private float mouseX;
    private float mouseY;

    private void Update()
    {
        moveCheck = player.GetComponent<Temporarily_PlayerMove>().GetMoveState();
        mouseX = player.GetComponent<Temporarily_PlayerMove>().GetMousePositionX();
        mouseY = player.GetComponent<Temporarily_PlayerMove>().GetMousePositionY();
    }

    private void LateUpdate()
    {
        this.transform.position += new Vector3(x, y, z);
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, movingDamp);

        if (moveCheck)
        {
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
}

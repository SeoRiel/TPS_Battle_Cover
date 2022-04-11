using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Setting for camera rotation speed")]
    [SerializeField] private float camSpeed = 1.0f;

    [Header("Camera distance & offset ")]
    [SerializeField] private float moveDamping = 15.0f;
    [SerializeField] private float rotateDamping = 10.0f;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 4.0f;
    [SerializeField] private float offset = 2.0f;

    [Header("Camera collider")]
    [SerializeField] private float radius = 1.8f;
    [SerializeField] private float overDamping = 5.0f;
    [SerializeField] private float originHeight;

    [Header("Camera obstacle")]
    [SerializeField] private float heightObstacle = 12.0f;
    [SerializeField] private float rayCastOffset = 1.0f;

    private Transform camTransform;

    private void Awake()
    {
        camTransform = GetComponent<Transform>();

        originHeight = height;
    }

    private void Update()
    {
        RaycastHit rayCastHit;

        Vector3 castTarget = target.position + (target.up * rayCastOffset);
        Vector3 castDirection = (castTarget - transform.position).normalized;

        if (Physics.CheckSphere(transform.position, radius))
        {
            height = Mathf.Lerp(height, heightObstacle, Time.deltaTime * overDamping);
        }
        else
        {
            height = Mathf.Lerp(height, originHeight, Time.deltaTime * overDamping);
        }

        if(Physics.Raycast(camTransform.position, castDirection, out rayCastHit, Mathf.Infinity))
        {
            height = Mathf.Lerp(height, heightObstacle, Time.deltaTime * overDamping);
        }
        else
        {
            height = Mathf.Lerp(height, originHeight, Time.deltaTime * overDamping);
        }
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = target.position - (target.forward * distance) + (target.up * height);

        camTransform.position = Vector3.Slerp(transform.position, cameraPosition, Time.deltaTime * moveDamping);
        camTransform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotateDamping);

        camTransform.LookAt(target.position + (target.up * offset));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + (target.up * offset), 0.1f);

        Gizmos.DrawLine(target.position + (target.up * offset), transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(target.position + (target.up * offset), transform.position);
    }
}

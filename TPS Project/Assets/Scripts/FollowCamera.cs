using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Reference Component")]
    [SerializeField] private GameObject player;

    [Header("Distance & Offset")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    [Header("Distance & Offset")]
    [SerializeField] private float moveDamping = 15.0f;
    [SerializeField] private float rotateDamping = 10.0f;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 4.0f;
    [SerializeField] private float offset = 2.0f;

    [Header("Collider Setting")]
    [SerializeField] private float radius = 1.8f;
    [SerializeField] private float overDamping = 5.0f;
    [SerializeField] private float originHeight;

    [Header("Obstacle")]
    [SerializeField] private float heightObstacle = 12.0f;
    [SerializeField] private float rayCastOffset = 1.0f;

    [Header("Ray Cast Range")]
    [SerializeField] private float rayRange = 1.0f;
    [SerializeField] private float offsetY = 1.0f;

    [Header("Cover Position")]
    [SerializeField] private GameObject coverObject;
    [SerializeField] private float coverHeight = 1.0f;

    private Transform camTransform;

    private Vector3 forward;
    private Vector3 rayOffset;

    private Ray ray;

    private int coverCount;

    private void Awake()
    {
        camTransform = GetComponent<Transform>();
        
        coverObject.SetActive(false);

        originHeight = height;
        coverCount = 0;
    }

    private void Update()
    {
        rayOffset = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z);
        ray = new Ray(rayOffset, this.transform.forward);

        // Object Hit
        RaycastHit objectFind;
        RaycastHit rayCastHit;

        Vector3 castTarget = target.position + (target.up * rayCastOffset);
        Vector3 castDirection = (castTarget - this.transform.position).normalized;

        forward = this.transform.TransformDirection(Vector3.forward) * 10;

        // Create cover position
        if(Physics.Raycast(ray, out objectFind, rayRange, 1<<8))
        {
            // Debug.Log("Object Hit");
            // 레이 캐스트 히트 판정을 오브젝트로 전달 -> 오브젝트에서 커버 생성
            // 레이 캐스트 히트 상태일 때, 레이 캐스트 히트 포인트 값을 오브젝트로 전달 -> 오브젝트에서 커버 생성

            Vector3 hitPosition = objectFind.point; // 이 값을 Getter로 생성 후, 오브젝트로 전달
            Vector3 coverPlace = new Vector3(hitPosition.x, coverHeight, hitPosition.z);
            coverObject.SetActive(true);
            coverObject.transform.position = new Vector3(coverPlace.x, coverHeight, coverPlace.z);
        }
        else if(!Physics.Raycast(ray, out objectFind, rayRange, 1 << 8))
        {
            coverObject.SetActive(false);
        }


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

        camTransform.position = Vector3.Slerp(this.transform.position, cameraPosition, Time.deltaTime * moveDamping);
        camTransform.rotation = Quaternion.Slerp(this.transform.rotation, target.rotation, Time.deltaTime * rotateDamping);

        camTransform.LookAt(target.position + (target.up * offset));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + (target.up * offset), 0.1f);

        Gizmos.DrawLine(target.position + (target.up * offset), this.transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(target.position + (target.up * offset), this.transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOffset, this.transform.forward * rayRange);
    }
}

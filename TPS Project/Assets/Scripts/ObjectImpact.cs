using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectImpact : MonoBehaviour
{
    private Rigidbody objectBody;
    private Vector3 objectVector;

    private void Awake()
    {
        objectBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // this.transform.position = objectVector;
        this.transform.Translate(this.transform.position.x, objectVector.y, this.transform.position.z);
    }

    private void OnTriggerStay(Collider mediumObject)
    {
        if (mediumObject.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("Floor trigger is activate");
            objectVector = new Vector3(0.0f, mediumObject.transform.position.y, 0.0f);
        }

        if (mediumObject.gameObject.layer == LayerMask.NameToLayer("Object"))
        {

        }
    }
}

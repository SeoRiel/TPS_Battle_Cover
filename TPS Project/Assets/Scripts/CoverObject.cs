using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObject : MonoBehaviour
{
    [SerializeField] private float radius = 1.0f;

    private CharacterMove character;

    private void Awake()
    {
        character = GetComponent<CharacterMove>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(character.transform.position, radius);
    }
}
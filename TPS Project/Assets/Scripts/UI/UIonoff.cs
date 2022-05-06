using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIonoff : MonoBehaviour
{
    [SerializeField] private GameObject crossHair;

    private void Awake()
    {
        crossHair.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            crossHair.SetActive(false);
        }
        else
        {
            crossHair.SetActive(true);
        }
    }
}

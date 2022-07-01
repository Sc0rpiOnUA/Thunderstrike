using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private Transform _camera;
    void Start()
    {
        _camera = Camera.main.transform;
    }

    
    void Update()
    {
        transform.LookAt(_camera, Vector3.up);    
    }
}

using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PlayerCanvas : NetworkBehaviour
{
    private Canvas _canvas;
    private Transform _camera;
    
    // Start is called before the first frame update
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _camera = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Ensure the health bar is always facing the camera
        transform.LookAt(transform.position + _camera.rotation * Vector3.forward,
            _camera.rotation * Vector3.up);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InvisibleOffScreen : MonoBehaviour
{
    private bool _isOffScreen;
    private Camera _camera;
    private float _screenWidth, _screenHeight;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _screenHeight = _camera.orthographicSize * 2f;
        _screenWidth = _screenHeight * _camera.aspect;
        
        CheckIfOffScreen();
    }

    // Currently not working
    private void CheckIfOffScreen()
    {
        if (transform.position.y < -_screenHeight)
        {
            Debug.Log("test");
            Debug.Log($"{gameObject.name} is invisible now");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera _vCam;
    private CinemachineBasicMultiChannelPerlin _vCamNoiseComponent;

    private void OnEnable()
    {
        GameManager.onGameOver += GameOver;
    }

    private void Awake()
    {
        _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        _vCamNoiseComponent = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void GameOver()
    {
        if (_vCamNoiseComponent.enabled == true)
        {
            _vCamNoiseComponent.enabled = false;
        }
    }
}

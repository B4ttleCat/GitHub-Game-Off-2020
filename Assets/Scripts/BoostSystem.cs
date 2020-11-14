using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class BoostSystem : MonoBehaviour
{
    [SerializeField] private GameObject _BoostBarPrefab;
    [SerializeField] private float _boostAmount;
    [SerializeField] private float _boostSpeed;

    private PlayerController _player;
    private bool _isBoosting;

    private void Awake()
    {
        _player = gameObject.GetComponent<PlayerController>();
    }

    private void Start()
    {
        // the underscore here denotes you don't need to pass anything into the Offence method
        References.Controls.Player.Offence.started += _ => BoostStarted();
        References.Controls.Player.Offence.performed += _ => BoostEnded();

        // If you want to pass in a value, use it like this
        // "context" is just a temp variable that could actually be called anything
        // _controls.Player.Offence.performed += context => Offence(cxt.ReadValue<float>());
    }

    public void BoostStarted()
    {
        if (_boostAmount <= 0) return;

        _isBoosting = true;
        _player.CurrentMoveSpeed = _boostSpeed;
        
        // Debug.Log($"isDashing = {_isBoosting}");
    }

    public void BoostEnded()
    {
        _isBoosting = false;
        _player.CurrentMoveSpeed = _player.BaseMoveSpeed;
        // Debug.Log($"isDashing = {_isBoosting}");
    }
}
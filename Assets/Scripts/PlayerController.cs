using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trailParticles;
    [SerializeField] private float _baseMoveSpeed;

    private Controls _controls = null;
    private Rigidbody2D _rb;
    
    private float _currentMoveSpeed;
    
    public float BaseMoveSpeed => _baseMoveSpeed;

public float CurrentMoveSpeed;

    private void Awake()
    {
        _controls = new Controls();
        References.Controls = _controls;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void Start()
    {
        _trailParticles.Play();
        _currentMoveSpeed = _baseMoveSpeed;
    }

    void Update()
    {
        Vector2 movementInput = ReadMovementInput();
        Move(movementInput);
        UpdateParticleSystem(movementInput);
    }

    private void UpdateParticleSystem(Vector2 movementInput)
    {
        ParticleSystem.EmissionModule emission = _trailParticles.emission;
        if (movementInput.y < 0)
        {
            emission.enabled = false;
        }

        else if (movementInput.y >= 0)
        {
            emission.enabled = true;
        }
    }

    private Vector2 ReadMovementInput()
    {
        Vector2 movementInput = _controls.Player.Move.ReadValue<Vector2>();

        Vector2 movement = new Vector2
        {
            x = movementInput.x,
            y = movementInput.y
        }.normalized;
        return movementInput;
    }

    public void Move(Vector2 movement)
    {
        transform.Translate(movement * CurrentMoveSpeed * Time.deltaTime);
    }
}
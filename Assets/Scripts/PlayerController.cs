using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trailParticles;
    [SerializeField] private ParticleSystem.MinMaxCurve slowParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve idleParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve regularParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve boostParticleSize;
   
    [SerializeField] private float _baseMoveSpeedMultiplier;
    public float BaseMoveSpeedMultiplier => _baseMoveSpeedMultiplier;
    [HideInInspector] public float CurrentMoveSpeed;
    
    private float _currentMoveSpeed;
    
    private Controls _controls = null;
    private Rigidbody2D _rb;

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
        _currentMoveSpeed = _baseMoveSpeedMultiplier;
    }

    void Update()
    {
        Vector2 movementInput = ReadMovementInput();
        Move(movementInput);
        UpdateParticleSystem(movementInput);
    }

    private void UpdateParticleSystem(Vector2 movementInput)
    {
        ParticleSystem.MainModule psMainModule = _trailParticles.main;
        
        if (movementInput.y < 0)
        {
            psMainModule.startSize = slowParticleSize;
        }
        
        else if (movementInput.y == 0)
        {
            psMainModule.startSize = idleParticleSize;
        }

        else if (movementInput.y > 0)
        {
            psMainModule.startSize = regularParticleSize;
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeedMultiplier;
    
    [Space]
    [Header("Particles")]
    [SerializeField] private ParticleSystem _trailParticles;

    [SerializeField] private ParticleSystem.MinMaxCurve slowParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve idleParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve regularParticleSize;
    [SerializeField] private ParticleSystem.MinMaxCurve boostParticleSize;

    private Controls _controls;
    private Rigidbody2D _rb;
    private BoostSystem _boost;

    private void Awake()
    {
        _controls = new Controls();
        References.Controls = _controls;
        _boost = GetComponentInChildren<BoostSystem>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        GameManager.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
        GameManager.onGameOver -= GameOver;
    }

    private void Start()
    {
        _trailParticles.Play();
    }

    void Update()
    {
        Vector2 movementInput = ReadMovementInput();
        
        Move(movementInput, _moveSpeedMultiplier);
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
        return movement;
    }

    public void Move(Vector2 movement, float speedMultiplier)
    {
        if (_boost.isBoosting)
        {
            speedMultiplier *= _boost.boostSpeedMultiplier;
        }
        
        Vector2 MultipledMovementVector = movement * speedMultiplier * References.GameSpeed;
        transform.Translate(MultipledMovementVector * Time.deltaTime);
    }

    private void GameOver()
    {
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }
}
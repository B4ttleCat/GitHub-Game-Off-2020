using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trailParticles;

    private Controls _controls = null;
    private float moveSpeed = 10f;
    private Rigidbody2D _rb;

    [SerializeField] private float _dashSpeed;
    private bool isDashing;

    private void Awake()
    {
        _controls = new Controls();
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
        // the underscore here denotes you don't need to pass anything into the Offence method
        _controls.Player.Offence.started += _ => OffenceStarted();
        _controls.Player.Offence.performed += _ => OffencePerformed();

        // If you want to pass in a value, use it like this
        // "context" is just a temp variable that could actually be called anything
        // _controls.Player.Offence.performed += context => Offence(cxt.ReadValue<float>());

        _trailParticles.Play();
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
            Debug.Log(_trailParticles.isPlaying);
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
        if (isDashing)
        {
            transform.Translate(movement * _dashSpeed * Time.deltaTime);
            Debug.Log("performing dash");
        }
        else
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }

    public void OffenceStarted()
    {
        isDashing = true;
        Debug.Log($"isDashing = {isDashing}");
    }

    private void OffencePerformed()
    {
        isDashing = false;
        Debug.Log($"isDashing = {isDashing}");
    }
}
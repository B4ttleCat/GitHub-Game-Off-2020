using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _inputMovement;
    private float moveSpeed = 10f;
    private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _trailParticles;

    #region DashCode

    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _startDashTime;
    private float _dashTime;
    private int _direction;
    private bool isDashing;

    #endregion

    private void Awake()
    {
    }

    private void Start()
    {
        _dashTime = _startDashTime;
        _trailParticles.Play();
    }

    void Update()
    {
        Move();

        ParticleSystem.EmissionModule emission = _trailParticles.emission;
        if (_inputMovement.y < 0)
        {
            emission.enabled = false;
        }
        else if (_inputMovement.y >= 0)
        {
            emission.enabled = true;
            Debug.Log(_trailParticles.isPlaying);
        }
    }

    public void Move()
    {
        Vector3 movement = new Vector3(_inputMovement.x, _inputMovement.y, 0) * moveSpeed * Time.deltaTime;

        if (isDashing)
        {
            transform.Translate(movement * _dashSpeed);
        }
        else
        {
            transform.Translate(movement);
        }
    }

    // Called when you move the left stick
    void OnMove(InputValue inputValue)
    {
        _inputMovement = inputValue.Get<Vector2>();
    }

    // Called on right trigger press
    void OnOffence()
    {
        // isDashing = true;
        Debug.Log("offensive action is on!");
    }

    void OnDefence()
    {
        Debug.Log("defensive action");
    }

    void OnAction()
    {
        Debug.Log("action");
    }
}
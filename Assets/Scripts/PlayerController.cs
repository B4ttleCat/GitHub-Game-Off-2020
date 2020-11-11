using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region newInput

    [SerializeField] private PlayerInput _playerInput;

    #endregion

    private Vector2 _inputMovement;
    private float moveSpeed = 10f;
    private Rigidbody2D _rigidbody2D;

    #region DashCode

    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _startDashTime;
    private float _dashTime;
    private int _direction;
    private bool isDashing;

    #endregion


    private void OnEnable()
    {
        _playerInput.Player.Move.performed += HandleMove;
        _playerInput.Player.Move.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= HandleMove;
        _playerInput.Player.Move.Disable();
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }

    private void Start()
    {
        _dashTime = _startDashTime;
    }

    void Update()
    {
        Move();
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
        isDashing = true;
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
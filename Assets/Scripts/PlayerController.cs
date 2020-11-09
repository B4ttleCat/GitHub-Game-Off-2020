using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _inputMovement;
    private float moveSpeed = 10f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(_inputMovement.x, _inputMovement.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void OnMove(InputValue inputValue)
    {
        _inputMovement = inputValue.Get<Vector2>();
    }
    
    void OnOffence()
    {
        
        Debug.Log("offensive action");
        
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

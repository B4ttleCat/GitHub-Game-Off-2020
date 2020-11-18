using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool _isTravelling;

    private Rigidbody2D _rb;

    void Awake()
    {
        References.Ball = this;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * References.GameSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            Vector3 playerPos = other.transform.position;
            
            // get direction of player to ball
            Vector2 angle = (transform.position - playerPos);
            // float directionToPlayer = Vector2.Angle(transform.position, playerPos);

            
            // apply force to ball in that direction
            _rb.AddForce(angle, ForceMode2D.Impulse);
            
            // bump it forward
        }
    }
}

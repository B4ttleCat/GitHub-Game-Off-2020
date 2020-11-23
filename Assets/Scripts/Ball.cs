using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool _isTravelling;

    [SerializeField]
    private float _hitForce;

    private Rigidbody2D _rb;
    private GameManager _gameManager;
    private ParticleSystem _ps;

    void Awake()
    {
        References.Ball = this;
        _rb = GetComponent<Rigidbody2D>();
        _ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * References.GameSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Get player position
            Vector3 playerPos = other.transform.position;

            // get direction of player to ball
            Vector2 angle = (transform.position - playerPos).normalized;

            // apply force in that direction to bump it forward
            _rb.AddForce(angle * _hitForce, ForceMode2D.Impulse);
        }

        if (other.gameObject.CompareTag("Moon"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _gameManager.EndGame();
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
        _ps.Stop();
    }
}
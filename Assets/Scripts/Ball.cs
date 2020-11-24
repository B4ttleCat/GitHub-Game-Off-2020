using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool _isTravelling;

    [SerializeField]
    private float _hitForce;

    private Rigidbody2D _rb;
    private GameManager _gameManager;
    private ParticleSystem _ps;
    private ScoreManager _scoreManager;

    // ball sprite
    private SpriteRenderer _ballSprite;

    [SerializeField]
    private Color p1Colour;

    [SerializeField]
    private Color p2Colour;

    void Awake()
    {
        References.Ball = this;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _rb = GetComponent<Rigidbody2D>();
        _ps = GetComponent<ParticleSystem>();
        _ballSprite = GetComponentInChildren<SpriteRenderer>();
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
        if (other.gameObject.CompareTag("Player1"))
        {
            _scoreManager.AddToScore(References.Player1);
            TintBall(p1Colour);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            _scoreManager.AddToScore(References.Player2);
            TintBall(p2Colour);
        }

        ShuntBall(other);

        if (other.gameObject.CompareTag("Moon"))
        {
            GameOver();
        }
    }

    private void ShuntBall(Collider2D other)
    {
        // Get player position
        Vector3 playerPos = other.transform.position;

        // get direction of player to ball
        Vector2 angle = (transform.position - playerPos).normalized;

        // apply force in that direction to bump it forward
        _rb.AddForce(angle * _hitForce, ForceMode2D.Impulse);
    }

    private void TintBall(Color playerColour)
    {
        _ballSprite.color = playerColour;
    }

    private void GameOver()
    {
        _gameManager.EndGame();
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
        _ps.Stop();
    }
}
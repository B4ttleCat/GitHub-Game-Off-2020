using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _moon;

    [SerializeField]
    private BoxCollider2D _endGameCollider;

    [SerializeField]
    private GameObject _endGameObjects;

    private float _gameTimePassed;
    private bool _isGameOver;

    public bool IsGameOver
    {
        get => _isGameOver;
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        // at the start of the game, place the end game objects
        // at (game SPEED x game TIME) away
        
        _endGameObjects.transform.position = new Vector2(
            Camera.main.transform.position.x,
        (References.GameSpeed * References.GameTimeInSeconds));
        _gameTimePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void EndGame()
    {
        _isGameOver = true;
        References.GameSpeed = 0f;
        Debug.Log("Game over, man. Game over.");
    }
}

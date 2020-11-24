using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Scores
    private float _p1Score;
    private float _p2Score;

    // UI
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    // Timer
    private float _timeSinceLastTouch;

    void Start()
    {
        _timeSinceLastTouch = 0f;
    }

    void Update()
    {
        _timeSinceLastTouch += Time.deltaTime;
    }

    public void AddToScore(PlayerController player)
    {
        // Figure out which player gets the score
        if (player == References.Player1)
        {
            // Add score
            _p1Score += _timeSinceLastTouch;
        }
        else if (player == References.Player2)
        {
            Debug.Log("player 2 scoring");
            // Add score
            _p2Score += _timeSinceLastTouch;
        }

        // reset timer
        _timeSinceLastTouch = 0f;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _scoreText.text = $"{_p1Score} P1   P2 {_p2Score}";
    }
}
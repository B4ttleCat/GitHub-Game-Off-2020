using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action onGameOver;

    [SerializeField]
    private GameObject _moon;

    [SerializeField]
    private BoxCollider2D _endGameCollider;

    [SerializeField]
    private GameObject _endGameObjects;

    [SerializeField]
    private PlayerController _player1;

    [SerializeField]
    private PlayerController _player2;

    [SerializeField]
    private float _gameLength;

    [Space]
    [SerializeField] private ParticleSystem[] _particleSystems;

    private float _gameTimePassed;
    private bool _isGameOver;
    private Controls _controls;

    public bool IsGameOver
    {
        get => _isGameOver;
    }

    private void Awake()
    {
        _gameLength = References.GameTimeInSeconds;
        _controls = References.Controls;
        References.Player1 = _player1;
        References.Player2 = _player2;
    }

    void Start()
    {
        _endGameObjects.transform.position = new Vector2(
            Camera.main.transform.position.x, (References.GameSpeed * References.GameTimeInSeconds));
        _gameTimePassed = 0f;
        References.Controls.Player.RestartGame.performed += _ => OnRestartGame();
    }

    private void Update()
    {
    }

    public void EndGame()
    {
        _isGameOver = true;
        References.GameSpeed = 0f;
        Debug.Log("Game over, man. Game over.");

        if (onGameOver != null)
        {
            onGameOver();

            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Pause();
            }
        }
    }

    private void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
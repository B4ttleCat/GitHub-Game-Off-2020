using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class References
{
    // Actors
    public static Ball Ball;
    public static PlayerController Player1;
    public static PlayerController Player2;
    
    // System
    public static Controls Controls;
    public static GameManager GameManager;
    public static ScoreManager ScoreManager;

    // Gameplay
    public static float GameSpeed = 7f;
    public static float GameTimeInSeconds = 5f;
}

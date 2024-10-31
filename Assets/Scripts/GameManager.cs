using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    // Validates if the ball is in motion
    public bool isMoving; 

    private PlayerController player;
    private BallController ball;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        ball = FindAnyObjectByType<BallController>();

        PlayerEvents();
    }

    #region Events

    // Calls the event if the player dies
    private void PlayerEvents()
    {
        if (player != null)
        {
            player.OnPlayerDeath += GameOver;
        }
    }

    #endregion

    private void GameOver()
    {
        print("Game Over");
    }
}
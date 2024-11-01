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

    private int score;

    // Validates if the ball is in motion
    public bool isMoving;
    
    private PlayerController player;
    private BallController ball;

    #region Events

    public event Action<int> OnScoreChanged;

    #endregion

    #region GET / SET

    public void SetScore(int score)
    {
        if (score != 0)
        {
            this.score += score;

            OnScoreChanged?.Invoke(this.score);
        }
    }
   
    public int GetScore() { return this.score; }

    #endregion

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        ball = FindAnyObjectByType<BallController>();

        PlayerEvents();
    }

    #region Call Events

    // Calls the event if the player dies
    private void PlayerEvents()
    {
        if (player != null)
        {
            player.OnPlayerDeath += GameOver;
        }
    }

    #endregion

    #region Methods

    private void GameOver()
    {
        print("Game Over");
    }

    #endregion
}

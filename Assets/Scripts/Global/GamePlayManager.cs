using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    #region Singleton

    public static GamePlayManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ResetValues();

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] private int maxLife = 3;
    [SerializeField] private int powerUpInterval = 5;

    private int currentLife;
    private int score;

    private int destroyedBlocks;

    // Validates if the ball is in motion
    public bool isBallMoving;

    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLifeChanged;
    public event Action OnPlayerDeath;
    public event Action<Transform> OnInstantiatePowerUp;
    public event Action OnBlocksAreGone;
    public event Action OnDisableAllPowerUps;

    #endregion

    #region GET / SET

    // Adds life to the player
    public void SetLife(int life)
    {
        if (life > 0)
        {
            currentLife += life;
            OnLifeChanged?.Invoke(currentLife);
        }
    }

    public int GetCurrentLife() { return currentLife; }

    // Adds points to the player
    public void SetScore(int score)
    {
        if (score != 0)
        {
            this.score += score;

            OnScoreChanged?.Invoke(this.score);
        }
    }

    // Adds the number of blocks destroyed
    public void SetDestroyedBlocks(Transform block)
    {
        destroyedBlocks ++;

        ActivePowerUp(block);

        BlocksAreGone();
    }

    public int GetDestroyedBlocks() { return destroyedBlocks; }

    public int GetScore() { return this.score; }

    #endregion

    private void Start()
    {
        ResetValues();

        FindAnyObjectByType<LevelManager>().OnChangedLevel += ResetValues;
    }

    #region Methods

    public void TakeDamage(int damage)
    {
        // Invoke the event only if the life has changed
        OnDisableAllPowerUps?.Invoke();

        currentLife -= damage;
        OnLifeChanged?.Invoke(currentLife);

        // Summons the event if the player runs out of lives
        if (currentLife <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    public void ResetValues()
    {
        score = 0;
        currentLife = maxLife;
        destroyedBlocks = 0;
        isBallMoving = false;

        OnScoreChanged?.Invoke(0);
        OnLifeChanged?.Invoke(currentLife);
    }

    public void BlocksAreGone()
    {
        if (destroyedBlocks >= FindAnyObjectByType<LevelManager>().GetBlocksCount())
        {
            destroyedBlocks = 0;
            OnBlocksAreGone?.Invoke();
        }
    }

    // Activate a power up if the condition is met
    public void ActivePowerUp(Transform block)
    {
        if (destroyedBlocks % powerUpInterval == 0)
        {
            OnInstantiatePowerUp?.Invoke(block);
        }
    }

    #endregion
}

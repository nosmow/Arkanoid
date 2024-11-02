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
        if (damage <= 0) return;

        currentLife -= damage;

        // Invoke the event only if the life has changed
        OnLifeChanged?.Invoke(currentLife);
        
        if (currentLife > 0) return ;

        // Summons the event if the player runs out of lives
        OnPlayerDeath?.Invoke();
    }

    public void ResetValues()
    {
        score = 0;
        currentLife = maxLife;

        OnScoreChanged?.Invoke(0);
        OnLifeChanged?.Invoke(currentLife);
    }

    // Activate a power up if the condition is met
    public void ActivePowerUp(Transform block)
    {
        if (destroyedBlocks >= powerUpInterval)
        {
            OnInstantiatePowerUp?.Invoke(block);
            destroyedBlocks = 0;
        }
    }

    #endregion
}

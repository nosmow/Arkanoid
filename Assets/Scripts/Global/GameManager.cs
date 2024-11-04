using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int selectedLevel;
    private bool loadCompleted;
    private bool pauseGame;

    #region GET / SET
    
    public int GetSelectedLevel() { return selectedLevel; }

    public bool GetPauseGame() { return pauseGame; }

    #endregion

    #region Event Handlers

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    
    #endregion

    #region Call Events

    // Calls the event if the player dies
    private void PlayerEvents()
    {
        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnPlayerDeath += GameOver;
        }
    }


    #endregion

    #region Game State Management

    // Pause and unpause the game
    public void TogglePause()
    {
        pauseGame = !pauseGame;
        UpdateTimeScale();
    }

    // It is executed if the game is lost
    public void GameOver()
    {
        pauseGame = true;
        UpdateTimeScale();
    }

    // It is executed if you want to restart the game
    public void RetryGame()
    {
        ChangedData();
    }

    #endregion

    #region Level Management

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Get a reference to the LevelManager after the scene has been loaded
        if (FindAnyObjectByType<LevelManager>() != null && !loadCompleted && SceneManager.GetActiveScene().buildIndex != 0)
        {
            InitializeLevel();
            loadCompleted = true;
            PlayerEvents();
        }
    }

    // Open a specific scene by indicating the index
    public void LoadSpecificScene(int index)
    {
        selectedLevel = 0;
        ResetValues();
        SceneManager.LoadScene(index);
    }

    // Load a specific level into the level scene
    public void LoadSpecificLevel(int level)
    {
        selectedLevel = level;
        ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Switch to the next level after winning the game
    public void ChagedDataNextLevel()
    {
        selectedLevel++;
        ChangedData();
    }

    // Load the level indicated
    private void ChangedData()
    {
        ResetValues();
        FindAnyObjectByType<LevelManager>().ChangedLevel(selectedLevel);
    }

    // Initializes the level when loading the scene
    private void InitializeLevel()
    {
        var levelManager = FindAnyObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.ChangedLevel(selectedLevel);
        }
    }

    #endregion

    #region Utility Methods

    // Reset fields when changing levels or scenes
    private void ResetValues()
    {
        pauseGame = false;
        loadCompleted = false;
        UpdateTimeScale();
        GamePlayManager.Instance?.ResetValues();
    }

    // Change the timescale value to 1 or 0
    private void UpdateTimeScale()
    {
        Time.timeScale = pauseGame ? 0f : 1f;
    }

    #endregion


}

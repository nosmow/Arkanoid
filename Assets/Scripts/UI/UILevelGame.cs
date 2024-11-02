using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class UILevelGame : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI textLivesPlayer;
    [SerializeField] private TextMeshProUGUI textLevelScore;

    private void Start()
    {
        PlayerEvents();
        GamePlayManagerEvents();
    }

    #region Call Events

    // Calls the event if the player dies
    private void PlayerEvents()
    {
        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnLifeChanged += UpdateLifesUI;
            UpdateLifesUI(GamePlayManager.Instance.GetCurrentLife());
        }
    }

    // Calls the events of the Game Manager
    private void GamePlayManagerEvents()
    {
        GamePlayManager.Instance.OnScoreChanged += UpdateLevelScoreUI;
    }

    #endregion

    #region Methods

    // Update player life in UI
    private void UpdateLifesUI(int currentLife)
    {
        textLivesPlayer.text = currentLife.ToString();
    }

    // Update level score in UI
    private void UpdateLevelScoreUI(int score)
    {
        textLevelScore.text = score.ToString();
    }

    #endregion
}

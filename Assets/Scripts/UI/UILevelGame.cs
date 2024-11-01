using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class UILevelGame : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI textLivesPlayer;
    [SerializeField] private TextMeshProUGUI textLevelScore;

    private PlayerController playerReference;

    private void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>();

        PlayerEvents();
        GameMangerEvents();
    }

    #region Call Events

    // Calls the event if the player dies
    private void PlayerEvents()
    {
        if (playerReference != null)
        {
            playerReference.OnLifeChanged += UpdateLifesUI;
            UpdateLifesUI(playerReference.GetCurrentLife());
        }
    }

    // Calls the events of the Game Manager
    private void GameMangerEvents()
    {
        GameManager.Instance.OnScoreChanged += UpdateLevelScoreUI;
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

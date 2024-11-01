using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class UILevelGame : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI textLivesPlayer;

    private PlayerController playerReference;

    private void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>();

        PlayerEvents();
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

    #endregion

    #region Methods

    // Update player life in UI
    private void UpdateLifesUI(int currentLife)
    {
        textLivesPlayer.text = currentLife.ToString();
    }

    #endregion
}

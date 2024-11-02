using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerEvents();
    }

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

    #region Methods

    private void GameOver()
    {
        print("Game Over");
    }

    #endregion


}

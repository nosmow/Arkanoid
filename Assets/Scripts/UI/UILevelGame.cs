using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class UILevelGame : MonoBehaviour
{
    [Header("Texts Settings")]
    [SerializeField] private TextMeshProUGUI textNumLevel;
    [SerializeField] private TextMeshProUGUI textLivesPlayer;
    [SerializeField] private TextMeshProUGUI textLevelScore;

    [Header("Panels Settings")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelGameOver;

    [Header("Buttons Settings")]
    [SerializeField] private Button nextLevelBtn;

    private void Start()
    {
        UpdateNumLevel();

        PlayerEvents();
        GamePlayManagerEvents();
        LevelManagerEvents();
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
        GamePlayManager.Instance.OnPlayerDeath += ViewPanelGameOver;

        GamePlayManager.Instance.OnScoreChanged += UpdateLevelScoreUI;
        GamePlayManager.Instance.OnBlocksAreGone += ViewPanelWin;
    }

    private void LevelManagerEvents()
    {
        FindAnyObjectByType<LevelManager>().OnChangedLevel += UpdateNumLevel;
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

    private void ViewPanelWin()
    {
        if (panelWin != null)
        {
            panelWin.SetActive(true);

            // Disable the continue button if I reach the last level
            if (GameManager.Instance.GetSelectedLevel() >= (FindAnyObjectByType<LevelManager>().GetLevelsCount() - 1))
            {
                nextLevelBtn.gameObject.SetActive(false);
            }
        }
    }

    public void DisablePanelWin()
    {
        panelWin.SetActive(false);
    }

    public void ViewPanelGameOver()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }

    public void DisablePanelGameOver()
    {
        panelGameOver.SetActive(false);
    }


    public void UpdateNumLevel()
    {
        textNumLevel.text = (FindAnyObjectByType<LevelManager>().GetCurrentLevel() + 1).ToString();
    }

    #endregion
}

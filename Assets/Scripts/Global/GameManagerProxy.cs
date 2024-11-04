using UnityEngine;

/// <summary>
/// It is used to interact between the Game Manager and 
/// the different scenes of the game.
/// </summary>

public class GameManagerProxy : MonoBehaviour
{
    public void OnChagedScene(int index)
    {
        GameManager.Instance.LoadSpecificScene(index);
    }

    public void OnChangedSceneSpecificLevel(int level)
    {
        GameManager.Instance.LoadSpecificLevel(level);
    }

    public void OnButtonNext()
    {
        GameManager.Instance?.ChagedDataNextLevel();
    }

    public void OnButtonPause()
    {
        GameManager.Instance?.TogglePause();
    }

    public void OnRetryGame()
    {
        GameManager.Instance?.RetryGame();
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}

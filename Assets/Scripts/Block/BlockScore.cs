using UnityEngine;

public class BlockScore : MonoBehaviour
{
    [Tooltip("Add the value you want to give to the player's score")]
    [SerializeField] private int addScore;

    //Method to add scores to the player
    public void AddScore()
    {
        GamePlayManager.Instance.SetScore(addScore);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ExtraLifePowerUp", menuName = "PowerUps/ExtraLife")]
public class ExtraLifePowerUp : PowerUpData
{
    [Header("Power Up Settings")]
    [SerializeField] private int extraLife = 1;

    #region Methods

    // Assigns the extra life to the player
    public override void ApplyEffect(GameObject target)
    {
        GamePlayManager.Instance.SetLife(extraLife);
    }

    public override void RemoveEffect(GameObject target)
    {
        //Future updates
    }

    #endregion
}

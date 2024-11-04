using UnityEngine;

[CreateAssetMenu(fileName = "EnlargeBallPowerUp", menuName = "PowerUps/EnlargeBall")]
public class EnlargeBallPowerUp : PowerUpData
{
    [SerializeField] private int enlargeSize = 2;

    #region Methods

    // Increase the size of the palette
    public override void ApplyEffect(GameObject target)
    {
        Enlarge(target, enlargeSize);
    }

    // Returns the palette size to normal
    public override void RemoveEffect(GameObject target)
    {
       Enlarge(target, -enlargeSize);

    }

    // Method to enlarge the ball
    private void Enlarge(GameObject target, float size)
    {
        if (target != null)
        {
            BallController ball = FindAnyObjectByType<BallController>();

            if (ball != null)
            {
                ball.IncreaseSize(size);
            }
        }
    }

    #endregion
}
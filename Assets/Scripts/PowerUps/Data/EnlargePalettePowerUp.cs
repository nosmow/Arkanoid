using UnityEngine;

[CreateAssetMenu(fileName = "EnlargePalettePowerUp", menuName = "PowerUps/EnlargePalette")]
public class EnlargePalettePowerUp : PowerUpData
{
    [SerializeField] private int enlargeSize = 4;

    #region Methods

    // Increase the size of the palette
    public override void ApplyEffect(GameObject target)
    {
        Enlarge(target, enlargeSize);
    }

    // Returns the palette size to normal
    public override void RemoveEffect(GameObject target)
    {
        Debug.Log("asasasasasasa");
        Enlarge(target, -enlargeSize);

    }

    // Method to lengthen the palette
    private void Enlarge(GameObject target, float size)
    {
        PaddleController paddle = target.GetComponent<PaddleController>();

        if (paddle != null)
        {
            paddle.IncreaseSizeX(size);
        }
    }

    #endregion
}

using UnityEngine;

[CreateAssetMenu(fileName = "ShieldPowerUp", menuName = "PowerUps/Shield")]
public class ShieldPowerUp : PowerUpData
{
    [Tooltip("Tag of the object in the scene")]
    [SerializeField] private string tagObjectConvert;

    [Tooltip("Tap created for the shield")]
    [SerializeField] private string shieldTagName;

    #region Methods

    // Turns the hidden object into a shield
    public override void ApplyEffect(GameObject target)
    {
        ChangeDataObject(tagObjectConvert, shieldTagName, true);
    }

    // Removes the shield and returns the hidden object to normal
    public override void RemoveEffect(GameObject target)
    {
        ChangeDataObject(shieldTagName, tagObjectConvert, false);
    }

    // Change the properties of the object
    private void ChangeDataObject(string tag1, string tag2, bool active)
    {
        var obj = GameObject.FindWithTag(tag1);
        obj.GetComponent<Renderer>().enabled = active;
        obj.tag = tag2;
    }

    #endregion
}

using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
    [Header("Power Up Settings")]
    public float durationTime;
    public float fallSpeed = 0.1f;

    // Abstract method that each power-up must implement
    public abstract void ApplyEffect(GameObject target);
    public abstract void RemoveEffect(GameObject target);
}

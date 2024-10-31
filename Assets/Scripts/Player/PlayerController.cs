using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxLife = 3;

    private int currentLife;

    public event Action OnPlayerDeath;

    private void Start()
    {
        currentLife = maxLife;
    }

    #region Methods

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0 )
        {
            // Summons the event if the player runs out of lives
            OnPlayerDeath?.Invoke();
        }
    }

    #endregion
}

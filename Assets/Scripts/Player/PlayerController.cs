using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxLife = 3;

    private int currentLife;

    #region Events
    
    public event Action<int> OnLifeChanged;
    public event Action OnPlayerDeath;

    #endregion

    #region GET / SET

    public int GetCurrentLife() { return currentLife; }

    #endregion

    private void Awake()
    {
        currentLife = maxLife;
    }

    private void Start()
    {
        FindAnyObjectByType<LevelManager>().OnChangedLevel += ResetLife;
    }

    #region Methods

    public void TakeDamage(int damage)
    {

        if (damage > 0)
        {
            currentLife -= damage;

            // Invoke the event only if the life has changed
            OnLifeChanged?.Invoke(currentLife);
        }

        if (currentLife <= 0 )
        {
            // Summons the event if the player runs out of lives
            OnPlayerDeath?.Invoke();
        }


    }

    public void ResetLife()
    {
        currentLife = maxLife;

        // Notify of life restoration
        OnLifeChanged?.Invoke(currentLife);
    }

    #endregion
}

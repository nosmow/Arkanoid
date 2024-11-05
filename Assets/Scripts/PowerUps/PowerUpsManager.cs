using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private ObjectPool pool;
    private int lastIndex = -1;

    private List<GameObject> powers = new List<GameObject>();

    private void Start()
    {
        pool = GetComponent<ObjectPool>();

        LoadData();

        GamePlayManager.Instance.OnInstantiatePowerUp += ActivePowerUp;
        GamePlayManager.Instance.OnDisableAllPowerUps += DisableAllPowerUps;
        GamePlayManager.Instance.OnPlayerDeath += DisableAllPowerUps;

        FindAnyObjectByType<LevelManager>().OnChangedLevel += DisableAllPowerUps;
    }

    #region Methods

    // Initializes power-ups and stores them in the `powers` list.
    private void LoadData()
    {
        powers.Clear();

        for (int i = 0; i < pool.prefabs.Length; i++)
        {
            var power = pool.GetObject(i);
            power.SetActive(false);
            powers.Add(power);
        }
    }

    /// <summary>
    /// Activate the power ups in the scene 
    /// using the object pool
    /// </summary>
    private void ActivePowerUp(Transform block)
    {
        // Check if there are disabled powerUps
        List<GameObject> inactivePowers = powers.Where(p => p != null && !p.activeSelf).ToList();

        // If there are inactive powerUps
        if (inactivePowers.Count > 0)
        {
            int index = Random.Range(0, inactivePowers.Count);
            GameObject powerUp = inactivePowers[index];

            if (powerUp != null)
            {
                powerUp.transform.position = block.position;
                powerUp.SetActive(true);
            }
        }
    }

    public void DisableAllPowerUps()
    {
        for (int i = 0; i < powers.Count; i++)
        {
            if (powers[i] != null && powers[i].activeSelf)  // Verifica que el objeto no sea null
            {
                powers[i].SetActive(false);
            }
        }
    }



    #endregion
}

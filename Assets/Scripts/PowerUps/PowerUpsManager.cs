using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private ObjectPool pool;
    private int lastIndex = -1;

    private void Start()
    {
        pool = GetComponent<ObjectPool>();

        GamePlayManager.Instance.OnInstantiatePowerUp += ActivePowerUp;
    }

    #region Methods

    /// <summary>
    /// Activate the power ups in the scene 
    /// using the object pool
    /// </summary>
    private void ActivePowerUp(Transform block)
    {
        int index;

        do
        {
            index = Random.Range(0, pool.GetPoolCount());
        }
        while (index == lastIndex);

        lastIndex = index;

        GameObject powerUp = pool.GetObject(index);

        if (powerUp != null)
        {
            powerUp.transform.position = block.position;
            powerUp.SetActive(true);
        }
    }

    #endregion
}

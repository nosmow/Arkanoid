using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefabs;
    [SerializeField] private int initialAmount = 10;

    [SerializeField] private Queue<GameObject>[] poolQueues;

    #region GET / SET

    public int GetPoolCount() { return poolQueues.Length; }

    #endregion

    private void Awake()
    {
        InitializePool();
    }

    #region Methods

    // Method called at startup, used to initialize the object pool.
    private void InitializePool()
    {
        poolQueues = new Queue<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            poolQueues[i] = new Queue<GameObject>();

            for (int j = 0; j < initialAmount; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                poolQueues[i].Enqueue(obj);
            }
        }
    }

    // Method to get an object of a specific type from the pool.
    public GameObject GetObject(int index)
    {
        GameObject obj;

        if (poolQueues[index].Count > 0)
        {
            obj = poolQueues[index].Dequeue();
        }
        else
        {
            obj = Instantiate(prefabs[index]);
        }

        obj.SetActive(true);
        return obj;
    }

    // Method to return an object to the pool when it is no longer needed in the scene.
    public void ReturnObject(int index, GameObject obj)
    {
        obj.SetActive(false);
        poolQueues[index].Enqueue(obj);
    }

    #endregion
}

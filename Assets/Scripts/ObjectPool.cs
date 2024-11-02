using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    [SerializeField] private int initialAmount = 10;

    [SerializeField]private Queue<GameObject>[] blockPools;

    private void Awake()
    {
        StartPool();
    }

    #region Methods

    // Method called at startup, used to initialize the object pool.
    private void StartPool()
    {
        blockPools = new Queue<GameObject>[blockPrefabs.Length];

        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            blockPools[i] = new Queue<GameObject>();

            for (int j = 0; j < initialAmount; j++)
            {
                GameObject block = Instantiate(blockPrefabs[i]);
                block.SetActive(false);
                blockPools[i].Enqueue(block);
            }
        }
    }

    // Method to get a block of a specific type from the pool.
    public GameObject GetBlock(int blockType)
    {
        GameObject block;
        if (blockPools[blockType].Count > 0)
        {
            block = blockPools[blockType].Dequeue();
        }
        else
        {
            block = Instantiate(blockPrefabs[blockType]);
        }

        block.SetActive(true);
        block.GetComponent<Block>().SetBlockType(blockType);
        return block;
    }

    // Method to return a block to the pool when it is no longer needed in the scene.
    public void ReturnBlock(int blockType, GameObject block)
    {
        block.SetActive(false);
        blockPools[blockType].Enqueue(block);
    }

    #endregion
}

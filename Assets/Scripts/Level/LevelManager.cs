using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;

    [SerializeField] private List<LevelData> levelDataList = new List<LevelData>();
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();

    private int currentLevel;
    private bool levelLoaded;

    #region Events

    public event Action OnChangedLevel;

    #endregion

    /// <summary>
    /// Switches to the specified level, resetting blocks, loading new data, and
    /// resetting the game values.
    /// </summary>
    public void ChangedLevel(int num)
    {
        LevelData level = levelDataList[num];

        if (level == null) return;

        // Sets the current level to the specified level number.
        currentLevel = num;

        // Return all current blocks to the pool for reuse.
        foreach (var block in blocks)
        {
            int blockType = block.GetComponent<Block>().GetBlockType();
            pool.ReturnBlock(blockType, block);
        }

        blocks.Clear();

        // Load the current level-specific data.
        LoadData(num);

        GamePlayManager.Instance.ResetValues();

        // Call the OnChangedLevel event to notify other systems that the level has changed.
        OnChangedLevel?.Invoke();

        levelLoaded = true;
    }

    private void LoadData(int num)
    {
        var levelData = levelDataList[num];
        if (levelData?.rows == null) return;

        
        // Iterate over each column at the level
        for (int col = 0; col < levelData.rows.Length; col++)
        {
            var columnData = levelData.rows[col];
            if (columnData?.positions == null) continue;

            // Increment that controls the horizontal scrolling
            int increment = 0;

            // Iterate over each row in the current column
            for (int row = 0; row < columnData.positions.Length; row++)
            {
                // If the value of the row is different from 0, add a block to the scene
                if (columnData.positions[row] != 0)
                {
                    // Gets the type of block to instantiate
                    int blockType = columnData.positions[row] - 1;

                    if (blockType >= 0 && blockType < pool.prefabs.Length)
                    {
                        Vector3 position = new Vector3((row + increment), -col, 0);

                        var block = pool.GetObject(blockType);
                        block.GetComponent<Block>().SetBlockType(blockType);
                        block.transform.position = position;
                        block.transform.parent = transform;
                        blocks.Add(block);
                    }
                }

                // Increases the position even if no block is instantiated in order to leave the space empty
                increment++;
            }
        }
    }
}

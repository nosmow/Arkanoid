using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData levelData;
    public GameObject[] blockPrefabs;

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (levelData == null || levelData.columns == null) return;
        
        // Iterate over each column at the level
        for (int col = 0; col < levelData.columns.Length; col++)
        {
            var columnData = levelData.columns[col];

            if (columnData == null || columnData.rows == null) continue;

            // Increment that controls the horizontal scrolling
            int increment = 0;

            // Iterate over each row in the current column
            for (int row = 0; row < columnData.rows.Length; row++)
            {
                // If the value of the row is different from 0, add a block to the scene
                if (columnData.rows[row] != 0)
                {
                    // Gets the type of block to instantiate
                    int blockType = columnData.rows[row] - 1;

                    if (blockType >= 0 && blockType < blockPrefabs.Length)
                    {
                        Vector3 position = new Vector3((row + increment), -col, 0);
                        Instantiate(blockPrefabs[blockType], position, Quaternion.identity);
                    }
                }

                // Increases the position even if no block is instantiated in order to leave the space empty
                increment++;
            }
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class RowData
{
    //Saves the rows of the column that will contain blocks
    [Tooltip("0 = No Block, 1 = Block Type 1, 2 = Block Type 2, etc.")]
    public int[] positions = new int[0];
}

// ScriptableObject to store the information of each level
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/levelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Level settings")]
    [Tooltip("Number of rows for the level")]
    public int numPositions;

    [Tooltip("Number of positions per row")]
    public int numRows;

    [Header("Level rows and rows")]
    public RowData[] rows;

    #region Methods

    // Validates that the number of rows and positions is correct every time it is modified
    private void OnValidate()
    {
        CheckPositions();
        CheckRows();
    }

    // Check and assign the correct number of positions
    private void CheckPositions()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].positions == null || rows[i].positions.Length != numPositions)
            {
                rows[i].positions = new int[numPositions];
            }
        }
    }

    // Check and assign the correct number of rows
    private void CheckRows()
    {
        if (rows == null || rows.Length != numRows)
        {
            RowData[] newRows = new RowData[numRows];
            for (int i = 0; i < numRows; i++)
            {
                if (i < rows?.Length)
                {
                    newRows[i] = rows[i];
                }
                else
                {
                    newRows[i] = new RowData();
                }
            }
            rows = newRows;
        }
    }

    #endregion
}

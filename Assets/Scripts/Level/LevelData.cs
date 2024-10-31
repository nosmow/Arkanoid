using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class RowData
{
    //Saves the rows of the column that will contain blocks
    [Tooltip("0 = No Block, 1 = Block Type 1, 2 = Block Type 2, etc.")]
    public int[] rows = new int[5];
}

// ScriptableObject to store the information of each level
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/levelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Level settings")]
    [Tooltip("Number of columns for the level")]
    public int numCols;

    [Tooltip("Number of rows per column")]
    public int numRows;

    [HideInInspector] public float maxScore;

    [Header("Level columns and rows")]
    public RowData[] columns;

    [Header("Level power-ups")]
    public GameObject[] powerups;


    #region Methods

    // Validates that the number of rows and columns is correct every time it is modified
    private void OnValidate()
    {
        CheckColumns();
        CheckRows();
    }

    // Check and assign the correct number of columns
    private void CheckColumns()
    {
        if (columns == null || columns.Length != numCols)
        {
            RowData[] newColumns = new RowData[numCols];

            for (int i = 0; i < numCols; i++)
            {
                newColumns[i] = (i < columns?.Length) ? columns[i] : new RowData();
            }
            columns = newColumns;
        }
    }

    // Check and assign the correct number of rows
    private void CheckRows()
    {
        foreach (RowData col in columns)
        {
            if (col.rows.Length != numRows)
            {
                int[] newRows = new int[numRows];

                for (int i = 0; i < numRows && i < col.rows.Length; i++)
                {
                    newRows[i] = col.rows[i];
                }
                col.rows = newRows;
            }
        }
    }

    #endregion
}

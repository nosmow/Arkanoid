# ARKANOID

![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgumR2U12m5olIrb4socA6zLUtK3pOKbzrlZErnnpfmbr0pmtC2n1VRczE6ToBo7q3QdU2leWIpTLxfhYLembiY232sv5Ghze46D9mPy7cIy-HHr4uAP03GZgVcnLfxHyA0wQ2pvggK_U-5eKGS4k-W24aiQ9hzHUaWwdsgUn3yS7BkwG0xWjbYYPnxiGI/s16000/Captura%20de%20pantalla%202024-11-04%20185045.png)

<p align="center" > 
  <img src="https://img.shields.io/badge/Unity%206-v.0.24-blue"/>
  <img src="https://img.shields.io/badge/C%20Sharp-v.8-purple"/>
</p>

## GAMEPLAY

`<YouTube>` : <https://youtu.be/CC_4s1ngZK8>

## DESCRIPTION

Welcome to Arkanoid Adventure, an exciting action-puzzle game that revives the classic brick-breaking experience! In this game, you control a horizontally scrolling platform to bounce a ball and destroy brightly colored blocks. Each level features a unique arrangement of bricks, challenging obstacles, and special power-ups to help you reach your goal.

## PROJECT STRUCTURE

### SCRIPTABLE OBJECTS

Scriptable Objects are used to define the configuration of levels, allowing levels to be created and edited without having to modify code. Each level can be represented as a Scriptable Object containing information about blocks and block types.

#### EXAMPLE

```c#
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
}
```

Blocks are generated and positioned in the level using Object Pooling, which allows reusing Objects and reducing memory consumption.

#### EXAMPLE

```c#
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
                   // block.transform.parent = transform;
                    blocks.Add(block);

                    // Increment the counter if the block is not of type "rock"
                    if (!block.GetComponent<Block>().GetIsRock())
                    {
                        nonRockBlockCount++;
                    }
                }
            }

            // Increases the position even if no block is instantiated in order to leave the space empty
            increment++;
        }
    }
```

## AUTHOR

|  [<img src="https://avatars.githubusercontent.com/u/165520012?v=4" width=115><br><sub>Nosmow</sub>](https://github.com/nosmow) |
| :---: |

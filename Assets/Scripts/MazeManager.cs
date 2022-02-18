using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A class that holds an array of maze cells.
/// </summary>
[Serializable]
class MazeRow
{
    public MazeCell[] mazeCells;
}

/// <summary>
/// A manager that's responsible for the maze.
/// </summary>
public class MazeManager : MonoBehaviour
{
    [SerializeField]
    private MazeRow[] mazeLayout;
    
    public Vector2 GetCellPosition(Vector2Int cellIndex)
    {

        var cell = this.GetCellByIndex(cellIndex);
        var cellPosition = cell.gameObject.transform.position;

        return cellPosition;
    }

    public MazeCell GetCellByIndex(Vector2Int cellIndex)
    {
        var row = this.mazeLayout[cellIndex.x];

        if (row == null)
        {
            Debug.Log("Row doesn't exist");
            return null;
        }

        var cell = row.mazeCells[cellIndex.y];
        if (cell == null)
        {
            Debug.Log("Cell doesn't exist");
            return null;
        }

        return cell;
    }

    public int GetMazeHeight()
    {
        return mazeLayout.Length;
    }

    public int GetMazeWidth()
    {
        return mazeLayout[0].mazeCells.Length;
    }

    public Vector2Int GetNewPositionIndex(Vector2Int currentIndex, MovementDirection direction)
    {
        Vector2Int newPositionIndex = currentIndex;
        if (direction == MovementDirection.Up)
        {
            newPositionIndex = new Vector2Int(currentIndex.x - 1, currentIndex.y);
        }
        else if (direction == MovementDirection.Down)
        {
            newPositionIndex = new Vector2Int(currentIndex.x + 1, currentIndex.y);
        }
        else if (direction == MovementDirection.Left)
        {
            newPositionIndex = new Vector2Int(currentIndex.x, currentIndex.y - 1);
        }
        else if (direction == MovementDirection.Right)
        {
            newPositionIndex = new Vector2Int(currentIndex.x, currentIndex.y + 1);
        }
        return newPositionIndex;
    }

}

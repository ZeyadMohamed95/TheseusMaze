using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityMovementState
{
    Moving,
    Still
}

/// <summary>
/// A base controller that's reponsible for controlling entities.
/// </summary>
public class EntityControllerBase : MonoBehaviour
{
    private Vector2Int currentCell;

    private Vector2Int newCell;
    
    private Vector2 previousPosition;

    private Vector2 newPosition;

    [SerializeField]
    private MazeManager mazeManager;


    public Vector2Int CurrentCell => this.currentCell;
    
    public EntityMovementState EntityMovementState = EntityMovementState.Still;

    private void Awake()
    {
        previousPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        if(this.EntityMovementState == EntityMovementState.Moving)
        {
            this.SmoothMovementTransition();
        }
        
    }

    public void MoveEntity(Vector2Int newCell, Vector2 positionToMove)
    {
        this.previousPosition = this.transform.position;
        this.newPosition = positionToMove;
        this.newCell = newCell;
        this.EntityMovementState = EntityMovementState.Moving;
    }

    private void SmoothMovementTransition()
    {
        //transform.position = Vector2.Lerp(this.transform.position, this.newPosition, Mathf.SmoothStep(0, 1, 0.4f));

        if (Vector2.Distance(transform.position, this.newPosition) <= 0.1f)
        {
            this.SetEntityPosition(this.newCell, this.newPosition);
            this.EntityMovementState = EntityMovementState.Still;
        }
    }

    public void SetEntityPosition(Vector2Int newCell , Vector2 newPosition)
    {
        this.transform.position = newPosition;
        this.currentCell = newCell;
    }

    public bool CanMove(Vector2Int currentIndex, MovementDirection direction)
    {
        var currentCell = this.mazeManager.GetCellByIndex(currentIndex);
        switch (direction)
        {
            case MovementDirection.Up:
                if (currentIndex.x == 0 || currentCell.MazeCellType == MazeCellType.North || currentCell.MazeCellType ==
                    MazeCellType.NorthEast || currentCell.MazeCellType == MazeCellType.NorthWest || currentCell.MazeCellType == MazeCellType.NorthWest)
                {
                    return false;
                }
                break;
            case MovementDirection.Down:
                var mazeHeight = this.mazeManager.GetMazeHeight();
                if (currentIndex.x == mazeHeight || currentCell.MazeCellType == MazeCellType.South || currentCell.MazeCellType ==
                    MazeCellType.SouthEast || currentCell.MazeCellType == MazeCellType.SouthWest)
                {
                    return false;
                }
                break;

            case MovementDirection.Left:
                if (currentIndex.y == 0 || currentCell.MazeCellType == MazeCellType.West || currentCell.MazeCellType ==
                    MazeCellType.NorthWest || currentCell.MazeCellType == MazeCellType.SouthWest)
                {
                    return false;
                }
                break;

            case MovementDirection.Right:
                var mazeWidth = this.mazeManager.GetMazeWidth();
                if (currentIndex.y == mazeWidth || currentCell.MazeCellType == MazeCellType.East || currentCell.MazeCellType ==
                    MazeCellType.NorthEast || currentCell.MazeCellType == MazeCellType.SouthEast)
                {
                    return false;
                }
                break;
        }
        return true;
    }
}

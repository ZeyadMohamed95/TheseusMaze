using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines Ai behavior in the game
/// </summary>
public class GameAiController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private MazeManager mazeManager;
    
    /// <summary>
    /// Decides enemy movement based on player position and surroundings.
    /// </summary>
    public void DecideNextMove()
    {
        var movesAvailable = 2;

        while(movesAvailable != 0)
        {
            if (this.CheckMoveInDirection(MovementDirection.Left, ref movesAvailable))
            {
                Debug.Log("Moved one time in direction: " + MovementDirection.Left);
            }
            else if(this.CheckMoveInDirection(MovementDirection.Right, ref movesAvailable))
            {
                Debug.Log("Moved one time in direction: " + MovementDirection.Right);
            }
            else if (this.CheckMoveInDirection(MovementDirection.Up, ref movesAvailable))
            {
                Debug.Log("Moved one time in direction: " + MovementDirection.Up);
            }
            else if (this.CheckMoveInDirection(MovementDirection.Down, ref movesAvailable))
            {
                Debug.Log("Moved one time in direction: " + MovementDirection.Down);
            }
            else
            {
                Debug.Log("Enemy can't Move");
                break;
            }
        }
    }

    private bool CheckMoveInDirection(MovementDirection direction, ref int movesAvailable)
    {
        var originalDistance = this.GetDistance(this.playerController.CurrentCell, this.enemyController.CurrentCell);

        var newEnemyIndex = this.mazeManager.GetNewPositionIndex(this.enemyController.CurrentCell, direction);

        if (originalDistance >= this.GetDistance(this.playerController.CurrentCell, newEnemyIndex) 
            && this.enemyController.CanMove(this.enemyController.CurrentCell, direction))
        {
            var newPosition = this.mazeManager.GetCellPosition(newEnemyIndex);
            this.enemyController.SetEntityPosition(newEnemyIndex, newPosition);

            movesAvailable--;
            return true;
        }
        else
        {
            Debug.Log("Couldn't move in direction: " + direction.ToString());
            return false;
        }

    }

    /// <summary>
    /// Gets distance in terms of number of cells
    /// </summary>
    /// <param name="playerCell"> The player cell</param>
    /// <param name="enemyCell"> The enemy cell</param>
    /// <returns> Distance </returns>
    private int GetDistance(Vector2Int playerCell, Vector2Int enemyCell)
    {
        var distance = Mathf.Abs(playerCell.x - enemyCell.x) + Mathf.Abs(playerCell.y - enemyCell.y);
        return (int)distance;
    }
}

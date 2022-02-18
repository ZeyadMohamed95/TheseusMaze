using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}

/// <summary>
/// Controls game behavior.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    private MazeManager mazeManager;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private EnemyController enemyController;
    
    [SerializeField]
    private Vector2Int startPlayerIndex;

    [SerializeField]
    private Vector2Int startEnemyIndex;

    [SerializeField]
    private GameAiController AiController;

    private GameState gameState = GameState.PlayerTurn;

    private Vector2Int playerCurrentPosition => playerController.CurrentCell;

    private Vector2Int enemyCurrentPosition => enemyController.CurrentCell;

    public static event Action GameLost;

    public static event Action GameWon;

    public static event Action StartGame;
    
    private void Start()
    {
        StartGame?.Invoke();
        var playerStartPosition = this.mazeManager.GetCellPosition(this.startPlayerIndex);
        this.playerController.SetEntityPosition(this.startPlayerIndex, playerStartPosition);

        var enemyStartPosition = this.mazeManager.GetCellPosition(this.startEnemyIndex);
        this.enemyController.SetEntityPosition(this.startEnemyIndex, enemyStartPosition);
    }

    private void Update()
    {
        this.ManageGame();
    }

    private void HandlePlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandlePlayerMovement(this.playerController, MovementDirection.Up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandlePlayerMovement(this.playerController, MovementDirection.Down);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HandlePlayerMovement(this.playerController, MovementDirection.Left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandlePlayerMovement(this.playerController, MovementDirection.Right);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameState = GameState.EnemyTurn;
        }
    }

    private void ManageGame()
    {
        if (this.gameState == GameState.PlayerTurn)
        {
            this.HandlePlayerTurn();
        }
        else if (this.gameState == GameState.EnemyTurn)
        {
            this.HandleEnemyTurn();
        }
    }

    private void HandlePlayerMovement(EntityControllerBase entity, MovementDirection direction)
    {
        if(!entity.CanMove(entity.CurrentCell, direction))
        {
            return;
        }

        var newPositionIndex = this.mazeManager.GetNewPositionIndex(entity.CurrentCell, direction);
        var newCellPosition = this.mazeManager.GetCellPosition(newPositionIndex);

        entity.SetEntityPosition(newPositionIndex, newCellPosition);

        if (!CheckIfWon())
        {
            this.gameState = GameState.EnemyTurn;
        }

    }

    private void HandlePlayerTurn()
    {
        this.HandlePlayerInput();
    }
    private void HandleEnemyTurn()
    {
        this.AiController.DecideNextMove();

        if(!CheckIfLost())
        {
            this.gameState = GameState.PlayerTurn;
        }
    }

    private bool CheckIfLost()
    {
        if(enemyController.CurrentCell == playerController.CurrentCell)
        {
            this.gameState = GameState.LostLevel;
            GameLost?.Invoke();
            Debug.Log("Game lost invoked");
            return true;
        }

        return false;
    }

    private bool CheckIfWon()
    {
        var currentPlayerCell = this.mazeManager.GetCellByIndex(this.playerController.CurrentCell);
        if(currentPlayerCell.MazeCellType == MazeCellType.Exit)
        {
            this.gameState = GameState.WonLevel;
            GameWon?.Invoke();
            Debug.Log("Game won invoked");

            return true;
        }

        return false;
    }
}

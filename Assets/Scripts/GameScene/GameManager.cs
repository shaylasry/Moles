using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static Action<bool> GameRunningStateDidChange;
    
    public Vector3[,] boardTilePositions { get; private set; }
    public HashSet<Vector3> currentEnemiesPositions { get; private set; } = new HashSet<Vector3>();
    public Enemy[] enemies { get; private set; }
    public Player player { get; private set; }
    
    [SerializeField] private BoardGenerator _boardGenerator;
    [SerializeField] private SimpleEnemiesGenerator _enemiesGenerator;
    [SerializeField] private PlayerGenerator _playerGenerator;
    
    public GameManagerState currentState { get; private set; } = GameManagerState.Idle;
    private int _numOfGrassBlade;
    
    private void Awake()
    {
        ChangeState(GameManagerState.Idle);
    }

    void Start()
    {
        ChangeState(GameManagerState.BoardGeneration);
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();   
    }

    private void Subscribe()
    {
        PlayerController.DidHitEnemy += PlayerHitEnemy;
        PlayerController.DidHitGrassBlade += PlayerHitGrassBlasde;
        GeneralInputManager.OnFirstButtonPress += OnFirstButtonPress;
    }

    private void Unsubscribe()
    {
        PlayerController.DidHitEnemy -= PlayerHitEnemy;
        PlayerController.DidHitGrassBlade -= PlayerHitGrassBlasde;
        GeneralInputManager.OnFirstButtonPress -= OnFirstButtonPress;
    }

    private void PlayerHitGrassBlasde()
    {
        _numOfGrassBlade--;
        if (_numOfGrassBlade <= 0)
        {
            ChangeState(GameManagerState.Win);
        }
    }
    
    private void PlayerHitEnemy()
    {
        ChangeState(GameManagerState.Lose);
    }
    
    private void OnFirstButtonPress()
    {
        ChangeState(GameManagerState.Game);
    }

    private bool ChangeState(GameManagerState newState)
    {
        bool didChange = false;
        switch (currentState)
        {
            case GameManagerState.Idle:
                if (newState != GameManagerState.BoardGeneration) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.BoardGeneration:
                if (newState != GameManagerState.EnemyGeneration) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.EnemyGeneration:
                if (newState != GameManagerState.PlayerGeneration) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.PlayerGeneration:
                if (newState != GameManagerState.PreGame) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.PreGame:
                if (newState != GameManagerState.Game) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.Game:
                if (newState != GameManagerState.Lose && newState != GameManagerState.Win) break;
                currentState = newState;
                didChange = true;
                break;
            case GameManagerState.Lose:
                break;
            case GameManagerState.Win:
                break;
        }
        
        if (didChange) OnStateChange();
        
        return didChange;
    }

    private void OnStateChange()
    {
        switch (currentState)
        {
            case GameManagerState.Idle:
                break;
            case GameManagerState.BoardGeneration:
                GenerateBoard();
                break;
            case GameManagerState.EnemyGeneration:
                GenerateEnemies();
                break;
            case GameManagerState.PlayerGeneration:
                GeneratePlayer();
                break;
            case GameManagerState.PreGame:
                break;
            case GameManagerState.Game:
                GameRunningStateDidChange?.Invoke(true);
                break;
            case GameManagerState.Lose:
                GameRunningStateDidChange?.Invoke(false);
                Debug.Log("Game Over :/");
                break;
            case GameManagerState.Win:
                GameRunningStateDidChange?.Invoke(false);
                Debug.Log("You Won!");
                break;
        }
    }
    
    private void GeneratePlayer()
    {
        player = _playerGenerator.GeneratePlayer();
        ChangeState(GameManagerState.PreGame);
    }
    
    private void GenerateEnemies()
    {
        enemies = _enemiesGenerator.GenerateEnemies(boardTilePositions, currentEnemiesPositions);
        ChangeState(GameManagerState.PlayerGeneration);
    }
    
    private void GenerateBoard()
    {
        boardTilePositions = _boardGenerator.GenerateBoard();
        _numOfGrassBlade = GameObject.FindGameObjectsWithTag("GrassBlade").Length;
        ChangeState(GameManagerState.EnemyGeneration);
    }
    
    public enum GameManagerState 
    {
        Idle,
        BoardGeneration,
        EnemyGeneration,
        PlayerGeneration,
        PreGame,
        Game,
        Lose,
        Win
    }
}
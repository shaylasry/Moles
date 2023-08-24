using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator _boardGenerator;
    [SerializeField] private EnemiesGenerator _enemiesGenerator;
    public Vector3[,] boardTilePositions { get; private set; }
    private GameObject _player;
    private int _numOfGrassBlade;
    
    
    private GameStateMachine _gameStateMachine;
    private RunningState _runningState;
    private PauseState _pauseState;
    private WinState _winState;
    private LoseState _loseState;
    private PreGameState _preGameState;
    
    private void Awake()
    {
        GenerateGame();
        InitializeStates();
    }

    private void InitializeStates()
    {
        _gameStateMachine = new GameStateMachine();
        _runningState = new RunningState(_gameStateMachine);
        _pauseState = new PauseState(_gameStateMachine);
        _winState = new WinState(_gameStateMachine);
        _loseState = new LoseState(_gameStateMachine);
        _preGameState = new PreGameState(_gameStateMachine);
    }

    private void GenerateGame()
    {
        boardTilePositions = _boardGenerator.GenerateBoard();
        _numOfGrassBlade = GameObject.FindGameObjectsWithTag("GrassBlade").Length;
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
        PlayerController.onPopGrass += UpdateNumOfGrassBlade;
        PlayerController.onPlayerLose += PlayerLose;
        GeneralInputManager.onStartGame += GameDidStart;
    }

    private void Unsubscribe()
    {
        PlayerController.onPopGrass -= UpdateNumOfGrassBlade;
        PlayerController.onPlayerLose -= PlayerLose;
        GeneralInputManager.onStartGame -= GameDidStart;
    }
    
    private void PlayerLose()
    {
        _gameStateMachine.SetState(_loseState);
        Debug.Log("Game Over!");
    }

    private void UpdateNumOfGrassBlade()
    {
        _numOfGrassBlade--;

        if (_numOfGrassBlade <= 0)
        {
            _gameStateMachine.SetState(_winState);
            Debug.Log("Win!");
        }
    }
    
    private void GameDidStart()
    {
        _gameStateMachine.SetState(_runningState);
        _enemiesGenerator.GenerateMoles(boardTilePositions);
    }
    
    void Start()
    {
        _gameStateMachine.SetState(_preGameState);
    }
}
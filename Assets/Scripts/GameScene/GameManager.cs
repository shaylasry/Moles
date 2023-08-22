using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator _boardGenerator;
    private GameObject _player;
    private int _numOfGrassBlade;
    
    private GameStateMachine _gameStateMachine;
    private RunningState _runningState;
    private PauseState _pauseState;
    private WinState _winState;
    private LoseState _loseState;
    
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
    }

    private void GenerateGame()
    {
        _boardGenerator.GenerateBoard();
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
    }

    private void Unsubscribe()
    {
        PlayerController.onPopGrass -= UpdateNumOfGrassBlade;
    }
    
    private void PlayerLose()
    {
        _gameStateMachine.ChangeState(_loseState);
        Debug.Log("Game Over!");
    }

    private void UpdateNumOfGrassBlade()
    {
        _numOfGrassBlade--;

        if (_numOfGrassBlade <= 0)
        {
            _gameStateMachine.ChangeState(_winState);
            Debug.Log("Win!");
        }
    }
    void Start()
    {
        _gameStateMachine.ChangeState(_runningState);
    }
}
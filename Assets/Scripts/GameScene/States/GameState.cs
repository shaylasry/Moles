using System;

public class GameState
{
    protected GameStateMachine gameStateMachine;
    public bool isGameRunning { get; protected set; }
   
    public GameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public void EnterState()
    {
    }
    public void ExitState()
    {
    }
}

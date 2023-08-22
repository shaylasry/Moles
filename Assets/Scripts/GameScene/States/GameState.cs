using System;

public class GameState
{
    protected GameStateMachine gameStateMachine;
    protected bool isGameRunning;
    public static Action<bool> onGameStateChange;
    public GameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public void EnterState()
    {
        onGameStateChange?.Invoke(isGameRunning);
    }
}

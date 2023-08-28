public class WinState : GameState
{
    public WinState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
        this.isGameRunning = false;
    }
}
public class RunningState : GameState
{
    public RunningState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
        this.isGameRunning = true;
    }
}
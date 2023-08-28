public class PauseState : GameState
{
    public PauseState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
        this.isGameRunning = false;
    }
}
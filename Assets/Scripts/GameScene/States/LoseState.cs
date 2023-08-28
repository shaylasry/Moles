public class LoseState : GameState
{
    public LoseState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
        this.isGameRunning = false;
    }
}
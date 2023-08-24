public class PreGameState : GameState
{
    public PreGameState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
        this.isGameRunning = false;
    }
}
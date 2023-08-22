
public class GameStateMachine
{
	public GameState currentGameState;
	
	public void ChangeState(GameState newState)
	{
		currentGameState = newState;
		currentGameState.EnterState();
	}
}


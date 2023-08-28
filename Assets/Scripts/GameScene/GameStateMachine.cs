
using System;

public class GameStateMachine
{
	public GameState currentGameState;
	public static Action<bool> onGameStateChange;
	public void SetState(GameState newState)
	{
		if (currentGameState != null)
			currentGameState.ExitState();

		currentGameState = newState;
		onGameStateChange?.Invoke(currentGameState.isGameRunning);
		currentGameState.EnterState();
	}
}


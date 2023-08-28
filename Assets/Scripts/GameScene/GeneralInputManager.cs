using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralInputManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public static event Action<InputAction.CallbackContext> onMove;
    public static event Action OnFirstButtonPress;
    
    public void StartGame(InputAction.CallbackContext context)
    {
        if (_gameManager.currentState == GameManager.GameManagerState.PreGame)
        {
            OnFirstButtonPress?.Invoke();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (_gameManager.currentState == GameManager.GameManagerState.Game)
        {
            onMove?.Invoke(context);
        }
    }
}
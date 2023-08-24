using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralInputManager : MonoBehaviour
{
    public static event Action<InputAction.CallbackContext> onMove;
    public static event Action onStartGame;
    private bool _isGameStarted = false;
        
    public void StartGame(InputAction.CallbackContext context)
    {
        if (!_isGameStarted)
        {
            _isGameStarted = true;
            onStartGame?.Invoke();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (_isGameStarted)
        {
            onMove?.Invoke(context);
        }
    }
}
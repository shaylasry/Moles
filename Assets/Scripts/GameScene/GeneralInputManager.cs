using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralInputManager : MonoBehaviour
{
    public static event Action<InputAction.CallbackContext> onMove;
        
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(("[TEST]: received move!"));
        onMove?.Invoke(context);
    }
}
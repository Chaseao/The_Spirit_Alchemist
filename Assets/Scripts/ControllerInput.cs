using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    
    bool UIEnabled = false;

    public delegate void ToggleUI(bool UIEnabled);
    public static event ToggleUI toggleUI;

    public void OnToggleUI(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIEnabled = !UIEnabled;

            if (UIEnabled)
            {
                playerInput.SwitchCurrentActionMap("UI");
            }
            else
            {
                playerInput.SwitchCurrentActionMap("Player");
            }

            toggleUI?.Invoke(UIEnabled);
        }
    }

    #region Gameplay
    public delegate void Move(float movementDirection);
    public static event Move move;
    public delegate void Jump();
    public static event Jump jump;
    public delegate void Climb(float climbDirection);
    public static event Climb climb;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            move?.Invoke(context.ReadValue<float>());
        }

        if (context.canceled)
        {
            move?.Invoke(0);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump?.Invoke();
        }
    }

    public void OnClimb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            climb?.Invoke(context.ReadValue<float>());
        }

        if (context.canceled)
        {
            climb?.Invoke(0);
        }
    }
    #endregion

    #region UI

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
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
}

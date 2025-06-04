using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    string[] JoystickNames;

    private bool usingGamepad = false;
    private Gamepad gamepad;
    private Keyboard keyboard;

    public InputManager()
    {
        gamepad = Gamepad.current;
        if (gamepad != null)
        {
            usingGamepad = true;
        }
    }

    public bool StartButtonPressed()
    {
        if (usingGamepad)
        {
            return gamepad.startButton.wasPressedThisFrame;
        }
        else
        {
            return Input.GetButtonDown("Pause");
        }
    }
    public Vector2 GetLeftStickValues()
    {
        if (usingGamepad)
        {
            return gamepad.leftStick.ReadValue();
        }
        else
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    public Vector2 GetRightStickValues()
    {
        if (usingGamepad)
        {
            return gamepad.rightStick.ReadValue();
        }
        else
        {
            // TODO: Create input controls for mouse/keyboard inventory selection
            return Vector2.zero;
        }
    }

    public bool JumpButtonPressed()
    {
        if (usingGamepad)
        {
            return gamepad.crossButton.isPressed || gamepad.circleButton.isPressed;
        }
        else
        {
            return Input.GetButton("Jump");
        }
    }

    public float FireButtonPressed()
    {
        if (usingGamepad)
        {
            return gamepad.rightTrigger.ReadValue();
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                return 1;
            }
            return 0;
        }
    }

    public bool GrabButtonPressed()
    {
        if (usingGamepad)
        {
            return gamepad.rightShoulder.isPressed;
        }
        else
        {
            return Input.GetButton("Grab");
        }
    }

    public bool DashButtonPressed()
    {
        if (usingGamepad)
        {
            return gamepad.leftShoulder.isPressed;
        }
        else
        {
            return Input.GetButton("Dash");
        }
    }
}

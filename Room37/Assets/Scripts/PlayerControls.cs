using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputButton
{
    Interact
}

public enum InputCommand
{
    InterfaceInteractLeft,
    InterfaceInteractRight
}

public class PlayerControls : MonoBehaviour 
{
    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;

    public int PlayerNumber;
    public int ControllerIndex;
    public ControllerType ControllerType;

    public event Action<Vector2> RawAnalogInput;
    public event Action<InputButton> ButtonPressed;
    public event Action<InputCommand> InputCommandStream;

    readonly KeyCode[] gamepadButton0 = new KeyCode[]
    {
        KeyCode.Joystick1Button0,
        KeyCode.Joystick2Button0,
        KeyCode.Joystick3Button0,
        KeyCode.Joystick4Button0,
    };

    readonly KeyCode[] keyboardButton0 = new KeyCode[]
    {
        KeyCode.Space,
        KeyCode.Return
    };

    void Update() 
    {
        if (ControllerType == ControllerType.Gamepad)
        {
            analogueInput.x = Input.GetAxis("Horizontal" + (ControllerIndex + 1));
            analogueInput.y = Input.GetAxis("Vertical" + (ControllerIndex + 1));
        }
        else
        {
            analogueInput.x = Input.GetAxis("Horizontal");
            analogueInput.y = Input.GetAxis("Vertical");
        }
        RaiseRawAnalogInput();

        bool isInteract = false;
        if (ControllerType == ControllerType.Gamepad)
        {
            isInteract = Input.GetKeyDown(gamepadButton0[ControllerIndex]);
        }
        else
        {
            isInteract = Input.GetKeyDown(keyboardButton0[0]);
            isInteract = isInteract || Input.GetKeyDown(keyboardButton0[1]);
        }

        if (isInteract)
        {
            RaiseButtonPressed(InputButton.Interact);
        }

        if (analogueInput.x > 0.25f)
        {
            RaiseButtonPressed(InputCommand.InterfaceInteractRight);
        }

        if (analogueInput.x < -0.25f)
        {
            RaiseButtonPressed(InputCommand.InterfaceInteractLeft);
        }
    }

    void RaiseButtonPressed(InputButton inputButton)
    {
        if (this.ButtonPressed != null)
        {
            this.ButtonPressed(inputButton);
        }
    }

    void RaiseButtonPressed(InputCommand commandStream)
    {
        if (this.InputCommandStream != null)
        {
            this.InputCommandStream(commandStream);
        }
    }

    void RaiseRawAnalogInput()
    {
        if (this.RawAnalogInput != null)
        {
            this.RawAnalogInput(this.analogueInput);
        }
    }
}

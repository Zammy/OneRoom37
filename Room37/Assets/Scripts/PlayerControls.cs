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
    private PlayerMovement player;

    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;

    public int PlayerNumber;

    public event Action<InputButton> ButtonPressed;
    public event Action<InputCommand> InputCommandStream;

    readonly KeyCode[] button0 = new KeyCode[]
    {
        KeyCode.Joystick1Button0,
        KeyCode.Joystick2Button0,
        KeyCode.Joystick3Button0,
        KeyCode.Joystick4Button0,
    };

    void Update() 
    {
        analogueInput.x = Input.GetAxis("Horizontal" + (PlayerNumber + 1));
        analogueInput.y = Input.GetAxis("Vertical" + (PlayerNumber + 1));
        player.MoveRequest(analogueInput);

        bool isInteract = Input.GetKeyDown(button0[PlayerNumber]);

        if (isInteract)
        {
            RaiseButtonPressed(InputButton.Interact);
        }

        if (analogueInput.x > 0.5f)
        {
            RaiseButtonPressed(InputCommand.InterfaceInteractRight);
        }

        if (analogueInput.x < -0.5f)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputButton
{
    Interact
}

public class PlayerControls : MonoBehaviour 
{
    [SerializeField]
    private PlayerMovement player;

    public int PlayerNumber;

    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;

    public event Action<InputButton> ButtonPressed;

    readonly KeyCode[] keyCodes = new KeyCode[]
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

        bool isInteract = Input.GetKeyDown(keyCodes[PlayerNumber]);   

        if (isInteract)
        {
            RaiseButtonPressed(InputButton.Interact);
        }
    }

    void RaiseButtonPressed(InputButton inputButton)
    {
        if (this.ButtonPressed != null)
        {
            this.ButtonPressed(inputButton);
        }
    }
}

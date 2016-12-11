using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputButton
{
    Interact, Motive, Opportunity, Means
}

public class PlayerControls : MonoBehaviour 
{
    [SerializeField]
    private PlayerMovement player;

    public int PlayerNumber;

    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;

    Dictionary<InputButton, string> BUTTONS_TO_AXIS;
    Dictionary<InputButton, bool> buttonPressedState = new Dictionary<InputButton, bool>()
    {
        {InputButton.Interact, false},
        {InputButton.Motive, false},
        {InputButton.Opportunity, false},
        {InputButton.Means, false},
    };

    public event Action<InputButton> ButtonPressed;

    void Awake()
    {
        BUTTONS_TO_AXIS = new Dictionary<InputButton, string>();
        var inputButtons = (InputButton[]) Enum.GetValues(typeof(InputButton));
        foreach (var inputButton in inputButtons)
        {
            BUTTONS_TO_AXIS[inputButton] = inputButton.ToString();
        }
    }


    // Update is called once per frame
    void Update() 
    {
        analogueInput.x = Input.GetAxis("Horizontal" + (PlayerNumber + 1));
        analogueInput.y = Input.GetAxis("Vertical" + (PlayerNumber + 1));
        player.MoveRequest(analogueInput);


        foreach(var kvp in BUTTONS_TO_AXIS)
        {
            InputButton inputButton = kvp.Key;
            string axisName = kvp.Value;
            bool isPressed = Input.GetAxisRaw(axisName) > 0;
            bool wasPressed = buttonPressedState[inputButton];
            if (isPressed && !wasPressed)
            {
                RaiseButtonPressed(inputButton);
            }
            buttonPressedState[inputButton] = isPressed;
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

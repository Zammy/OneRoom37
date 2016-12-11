using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour 
{
    [SerializeField]
    private PlayerMovement player;

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;


    // Update is called once per frame
    void Update() 
    {
        analogueInput.x = Input.GetAxis("Horizontal");
        analogueInput.y = Input.GetAxis("Vertical");
        player.MoveRequest(analogueInput);
    }
}

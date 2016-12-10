using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private PlayerMovement player;

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private Vector2 analogueInput = Vector3.zero;
    
    void Awake () {
        player = GetComponent<PlayerMovement>();
    }

    // Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        analogueInput.x = Input.GetAxis("Horizontal");
        analogueInput.y = Input.GetAxis("Vertical");
        if (analogueInput.magnitude > 0f) {
            player.MoveRequest(analogueInput);
        }
    }
}

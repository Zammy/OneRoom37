using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Transform visual;
    private Rigidbody2D rigidBody;

    public float minWalkingSpeed;
    public float maxWalkingSpeed;

    public float WalkingSpeedRange { get; private set; }

    void Awake() {
        visual = transform.Find("Visual").transform;
        rigidBody = transform.Find("Visual").GetComponent<Rigidbody2D>();

        WalkingSpeedRange = maxWalkingSpeed - minWalkingSpeed;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveRequest(Vector2 analogueInput) {
        float rotAngle = Mathf.Atan2(analogueInput.y, analogueInput.x) * Mathf.Rad2Deg;
        rigidBody.MoveRotation(rotAngle);
        rigidBody.velocity = analogueInput.normalized * (minWalkingSpeed + WalkingSpeedRange * analogueInput.magnitude);
    }
}

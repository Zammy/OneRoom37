using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Transform visual;
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float minWalkingSpeed;
    [SerializeField]
    private float maxWalkingSpeed;
    [SerializeField]
    private float inputMinimum = 0.5f;

    public float WalkingSpeedRange { get; private set; }

    void Awake()
    {
        WalkingSpeedRange = maxWalkingSpeed - minWalkingSpeed;
    }

    public void MoveRequest(Vector2 analogueInput) 
    {
        bool isMoving = analogueInput.magnitude > inputMinimum;
        if (!isMoving)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }
        float rotAngle = Mathf.Atan2(analogueInput.y, analogueInput.x) * Mathf.Rad2Deg;
        rigidBody.MoveRotation(rotAngle);
        rigidBody.velocity = analogueInput.normalized * (minWalkingSpeed + WalkingSpeedRange * analogueInput.magnitude);
    }
}

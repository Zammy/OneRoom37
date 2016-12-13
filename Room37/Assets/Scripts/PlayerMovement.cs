using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    PlayerControls playerControls;

    [SerializeField]
    private float minWalkingSpeed;
    [SerializeField]
    private float maxWalkingSpeed;
    [SerializeField]
    private float inputMinimum = 0.5f;

    public float WalkingSpeedRange { get; private set; }

    public bool IsInteracting
    {
        get;
        set;
    }

    void Awake()
    {
        WalkingSpeedRange = maxWalkingSpeed - minWalkingSpeed;
        playerControls.RawAnalogInput += OnRawAnalogInput;
    }

    void OnDestroy()
    {
        playerControls.RawAnalogInput -= OnRawAnalogInput;
    }

    void OnRawAnalogInput(Vector2 analogueInput) 
    {
        if (IsInteracting)
        {
            return;
        }
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

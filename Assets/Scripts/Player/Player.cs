using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool jumpButtonPressed;
    private bool standing;

    private float absVelocityX = 0f;
    private float absVelocityY = 0f;

    public float jumpSpeed = 240f;
    public float forwardSpeeed = 20f;

    private float standingThreshold = 1f;

    private Rigidbody2D playerBody;


    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        jumpButtonPressed = Input.anyKeyDown;
	}

    void FixedUpdate()
    {
        standing = IsStanding();
        if (standing && jumpButtonPressed)
        {
            Jump();
        }
    }

    bool IsStanding()
    {
        absVelocityX = System.Math.Abs(playerBody.velocity.x);
        absVelocityY = System.Math.Abs(playerBody.velocity.y);

        return absVelocityY <= standingThreshold;
    }

    public bool IsRunning()
    {
        if (absVelocityX > 0 && absVelocityY < standingThreshold)
            return true;
        else
            return false;
    }

    void Jump()
    {
        playerBody.velocity = new Vector2(transform.position.x < 0 ? forwardSpeeed : 0, jumpSpeed);
    }
}

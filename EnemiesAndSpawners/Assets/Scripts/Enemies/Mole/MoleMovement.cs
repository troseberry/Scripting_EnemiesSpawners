using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMovement : MonoBehaviour
{

    public float moveSpeed;
    private float currentMoveSpeed;
    private float halfMoveSpeed;

    public bool movingUpward;

    private Vector2 moveVector;
    private Vector2 directionVector;

    private GroundCheck moleGroundCheck;

    void Start()
    {
        moleGroundCheck = GetComponentInChildren<GroundCheck>();
        halfMoveSpeed = moveSpeed / 2;
        directionVector = movingUpward ? Vector2.up : Vector2.down;
    }

    void Update ()
	{
        Debug.Log("Grounded: " + moleGroundCheck.IsGrounded());
        Debug.Log("Colliding: " + moleGroundCheck.isColliding);

        currentMoveSpeed = moleGroundCheck.IsGrounded() ? halfMoveSpeed : moveSpeed;
        moveVector = new Vector2(transform.position.x, currentMoveSpeed) * directionVector;

        transform.Translate(moveVector);
	}
}

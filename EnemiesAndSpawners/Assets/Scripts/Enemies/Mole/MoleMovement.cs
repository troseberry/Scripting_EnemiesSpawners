﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMovement : MonoBehaviour
{
    public float moveSpeed;
    private float currentMoveSpeed;
    private float halfMoveSpeed;

    private bool movingUpward;

    private Vector2 directionVector;

    private GroundCheck moleGroundCheck;

    void Start()
    {
        moleGroundCheck = GetComponentInChildren<GroundCheck>();
        halfMoveSpeed = moveSpeed / 2;

        SetMoveDirection();
    }

    void Update ()
	{
        //Debug.Log("Grounded: " + moleGroundCheck.IsGrounded());
        //Debug.Log("Colliding: " + moleGroundCheck.isColliding);

        currentMoveSpeed = moleGroundCheck.IsGrounded() ? halfMoveSpeed : moveSpeed;
        transform.Translate(directionVector * (currentMoveSpeed * Time.deltaTime));
	}

    void SetMoveDirection()
    {
        float playerTransformY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        movingUpward = (transform.position.y < playerTransformY);

        directionVector = movingUpward ? Vector2.up : Vector2.down;

        GetComponentInChildren<SpriteRenderer>().flipY = !movingUpward;
    }
}

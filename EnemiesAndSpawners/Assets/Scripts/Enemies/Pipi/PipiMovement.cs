using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipiMovement : MonoBehaviour
{
    public float moveSpeed;

    private bool movingLeft;

    private Vector2 moveVector;
    private Vector2 directionVector;


	void Start ()
	{
        SetMoveDirection();
    }

	void Update ()
	{
        moveVector = new Vector2(moveSpeed, transform.position.y) * directionVector;

        transform.Translate(moveVector);
	}

    void SetMoveDirection()
    {
        float playerTransformX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        movingLeft = (transform.position.x > playerTransformX);

        directionVector = movingLeft ? Vector2.left : Vector2.right;

        GetComponentInChildren<SpriteRenderer>().flipX = movingLeft;
    }
}

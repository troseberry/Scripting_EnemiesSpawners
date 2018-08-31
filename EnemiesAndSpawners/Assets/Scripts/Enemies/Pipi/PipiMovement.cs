using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipiMovement : MonoBehaviour
{

    public float moveSpeed;

    private Vector2 moveVector;


	void Start ()
	{
		
	}

	void Update ()
	{
        moveVector = new Vector2(moveSpeed, transform.position.y) * Vector2.left;

        transform.Translate(moveVector);
	}
}

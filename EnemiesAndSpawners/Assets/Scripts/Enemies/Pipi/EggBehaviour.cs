using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    private GroundCheck eggGroundCheck;

    public float dropSpeed;
    private bool canMove;
    private Vector2 moveVector;

	void Start ()
	{
        eggGroundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Update()
    {
        if (canMove)
        {
            moveVector = new Vector2(transform.position.x, dropSpeed) * Vector2.down;
            transform.Translate(moveVector);
        }

        if (eggGroundCheck.IsGrounded()) DestroyOnLanding();
    }

    public void ReleaseEgg()
    {
        transform.parent = null;
        canMove = true;
    }

    void DestroyOnLanding()
    {
        Destroy(gameObject);
    }
}

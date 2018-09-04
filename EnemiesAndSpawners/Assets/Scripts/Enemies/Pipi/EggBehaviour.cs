using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    private GroundCheck eggGroundCheck;
    private bool initiatedDestroy = false;

    public float dropSpeed;
    private bool canMove;
    private Vector2 moveVector;

    public GameObject copipiPrefab;
    public int copipiSpawnCount;

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
        if (!initiatedDestroy)
        {
            for (int i = 0; i < copipiSpawnCount; i++)
            {
                Instantiate(copipiPrefab, transform.position, Quaternion.identity);
            }
            //Destroy(gameObject);
            initiatedDestroy = true;
        }
    }
}

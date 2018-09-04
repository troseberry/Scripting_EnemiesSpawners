using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    private GroundCheck eggGroundCheck;
    //private bool initiatedDestroy = false;

    public float dropSpeed;
    private bool canMove;
    private Vector2 moveVector;

    public GameObject copipiPrefab;
    public int copipiSpawnCount;
    private int copipiInstantiatedCount;
    private bool hasBrokenEgg = false;

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

        if (eggGroundCheck.IsGrounded()) StartCoroutine(DestroyEggOnLanding()); //DestroyOnLanding();
    }

    public void ReleaseEgg()
    {
        transform.parent = null;
        canMove = true;
    }

    IEnumerator DestroyEggOnLanding()
    {
        if (!hasBrokenEgg)
        {
            hasBrokenEgg = true;
            //Debug.Log("Coroutine");
            for (int i = 0; i < copipiSpawnCount; i++)
            {
                Instantiate(copipiPrefab, transform.position, Quaternion.identity);
                copipiInstantiatedCount++;
            }

            yield return new WaitUntil(() => copipiInstantiatedCount == copipiSpawnCount);
            Destroy(gameObject);
        }
    }
}

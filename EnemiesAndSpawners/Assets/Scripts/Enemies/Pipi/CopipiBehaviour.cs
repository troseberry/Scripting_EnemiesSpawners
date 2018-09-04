using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopipiBehaviour : MonoBehaviour
{
    public float moveSpeed;

    public float scatterMinDistance;
    public float scatterMaxDistance;
    private float scatterDistance;

    private Vector2 scatterDestination;
    private Vector2 scatterDirection;
    private bool doScatter = false;
    
    private bool attackMegaman = false;
    private Vector2 megamanDestination;
    private Vector2 megamanDirection;

	void Start ()
	{
        Scatter();
    }

	void Update ()
	{
        //Debug.Log("Distance: " + Vector2.Distance((Vector2)transform.position, scatterDestination));

        if (Vector2.Distance((Vector2)transform.position, scatterDestination) > 0.05 && doScatter)
        {
            transform.Translate(scatterDirection * (moveSpeed * Time.deltaTime));
        }
        else
        {
            doScatter = false;

            if (!attackMegaman)
            {
                TargetMegaman();
            }
            else
            {
                transform.Translate(megamanDirection * (moveSpeed * Time.deltaTime));
                //Debug.Log("Going to Megaman (Dir): " + megamanDirection);
            }
        }
    }

    void Scatter()
    {
        scatterDistance = Random.Range(scatterMinDistance, scatterMaxDistance);

        float destinationX = Random.Range(transform.position.x - scatterDistance, transform.position.x + scatterDistance);
        float destinationY = transform.position.y + scatterDistance;

        scatterDestination = new Vector2(destinationX, destinationY);
        scatterDirection = (scatterDestination - (Vector2)transform.position);

        //Debug.Log("Destination: " + scatterDestination);
        //Debug.Log("Distance: " + Vector2.Distance((Vector2)transform.position, scatterDestination));

        doScatter = true;
    }

    void TargetMegaman()
    {
        megamanDestination = GameObject.FindGameObjectWithTag("Player").transform.position;

        //Debug.Log("Megaman Destination: " + megamanDestination);
        //Debug.Log("Copipi Starting Position: " + transform.position);

        megamanDirection = (megamanDestination - (Vector2)transform.position);

        attackMegaman = true;
    }

    IEnumerator MoveTowardMegman()
    {
        yield return null;
    }
}

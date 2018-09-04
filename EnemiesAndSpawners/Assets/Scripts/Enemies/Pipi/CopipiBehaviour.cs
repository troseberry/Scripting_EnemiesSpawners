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

    private bool doScatter = false;
    private bool isScattering = false;

    private float scatterDuration = .2f;
    private float scatterTimer = 0f;

    private bool attackMegaman = false;
    private Vector2 megamanDestination;
    private Vector2 swoopStartPos;

    private Vector2 megamanDirection;

	void Start ()
	{
        Scatter();
    }

	void Update ()
	{
        if (scatterTimer < scatterDuration)
        {
            if (doScatter)
            {
                scatterTimer += Time.deltaTime;
                transform.Translate(scatterDestination * moveSpeed);
            }
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
                transform.Translate(megamanDirection * moveSpeed, Space.World);
                //transform.position = Vector2.MoveTowards(swoopStartPos, megamanDestination, moveSpeed * Time.deltaTime);
                Debug.Log("Going to Megaman");
            }
            //Debug.Log("Should Target: " + (megamanDestination * moveSpeed));
        }


        
    }

    void Scatter()
    {
        scatterDistance = Random.Range(scatterMinDistance, scatterMaxDistance);

        //float randomX = Random.Range(scatterMinDistance, scatterMaxDistance);

        float destinationX = transform.position.x + scatterDistance;
        float destinationY = transform.position.y + scatterDistance;
        scatterDestination = new Vector2(destinationX, destinationY);

        //Debug.Log("Destination: " + scatterDestination);

        doScatter = true;

    }

    void TargetMegaman()
    {
        megamanDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
        //megamanDestination = new Vector2(-2, 4);
        Debug.Log("Megaman Position: " + megamanDestination);
        swoopStartPos = new Vector2(transform.position.x, transform.position.y);

        megamanDirection = (megamanDirection - swoopStartPos).normalized;



        attackMegaman = true;
    }

    IEnumerator MoveTowardMegman()
    {
        yield return null;
    }
}

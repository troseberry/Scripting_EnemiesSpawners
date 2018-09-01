using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopipiBehaviour : MonoBehaviour
{
    public float moveSpeed;

    public float scatterMinDistance;
    public float scatterMaxDistance;
    private float scatterDistance;

	void Start ()
	{
        Scatter();
	}

	void Update ()
	{
		
	}

    void Scatter()
    {
        scatterDistance = Random.Range(scatterMinDistance, scatterMaxDistance);
        float destinationX = 0f;
        float destinationY = 0f;
        Vector2 destination = new Vector2(destinationX, destinationY);

        Vector2.MoveTowards(transform.position, destination, scatterMaxDistance);
    }
}

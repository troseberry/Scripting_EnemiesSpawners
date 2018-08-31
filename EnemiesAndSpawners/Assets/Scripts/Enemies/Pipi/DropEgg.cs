using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEgg : MonoBehaviour
{
    public EggBehaviour childEgg;
    private bool hasEgg = true;

    private Transform playerTransform;

	void Start ()
	{
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void Update ()
	{
        if (hasEgg && IsInDropRange())
        {
            childEgg.ReleaseEgg();
            hasEgg = false;
        }
    }

    bool IsInDropRange()
    {
        return (transform.position.x <= playerTransform.position.x + 3
           && transform.position.x >= playerTransform.position.x - 3);
    }
}

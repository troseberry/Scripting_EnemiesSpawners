﻿using System.Collections;
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

    private ColliderDoesDamage copipiDoesDamage;
    private bool doRetreat = false;
    private Vector2 retreatDirection;

    void Start ()
	{
        copipiDoesDamage = GetComponentInChildren<ColliderDoesDamage>();

        Scatter();
    }

	void Update ()
	{
        if (Vector2.Distance(transform.position, scatterDestination) > 0.05 && doScatter)
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
            }
        }

        if (copipiDoesDamage.DidDamagePlayer())
        {
            doRetreat = true;
            CalculateRetreatDirection();
        }

        if (doRetreat) RetreatOnDamage();
    }

    void Scatter()
    {
        scatterDistance = Random.Range(scatterMinDistance, scatterMaxDistance);
        scatterDestination = Random.insideUnitCircle * scatterDistance;
        scatterDirection = (scatterDestination - (Vector2)transform.position);

        doScatter = true;
    }

    void TargetMegaman()
    {
        megamanDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
        megamanDirection = (megamanDestination - (Vector2)transform.position);

        attackMegaman = true;
    }

    void CalculateRetreatDirection()
    {
        Vector2 randomRetreatPos = Random.insideUnitCircle;
        retreatDirection = (randomRetreatPos - (Vector2)transform.position);
    }

    void RetreatOnDamage()
    {
        transform.Translate(retreatDirection * (moveSpeed * Time.deltaTime));
    }
}

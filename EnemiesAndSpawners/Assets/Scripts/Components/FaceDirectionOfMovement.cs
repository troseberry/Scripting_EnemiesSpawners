using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can assume sprites are authored to face "right" 
public class FaceDirectionOfMovement : MonoBehaviour
{
    public GameObject objectToFlip;
    private float facingDir = 1f;

    private Vector2 moveVector;
    private float pastMoveX;
    private float currentMoveX;

    void Start ()
	{
        if (!objectToFlip) objectToFlip = gameObject;

        pastMoveX = moveVector.x;
        currentMoveX = moveVector.x;
    }

	void Update ()
	{
        moveVector = transform.position;

        pastMoveX = currentMoveX;
        currentMoveX = moveVector.x;

        if (DetectedDirChange()) FlipTransform();
    }
    

    // Returns 1.0f if facing right, -1.0f if facing left. 
    public float GetFacing()
    {
        return facingDir;
    }

    bool DetectedDirChange()
    {
        if (pastMoveX != currentMoveX)
        {
            facingDir = pastMoveX > currentMoveX ? 1f : -1f;
            return true;
        }
        return false;
    }

    void FlipTransform()
    {
        float rotY = facingDir < 0 ? 0 : 180;
        transform.rotation = new Quaternion(transform.rotation.x, rotY, 0, 0);
    }
}

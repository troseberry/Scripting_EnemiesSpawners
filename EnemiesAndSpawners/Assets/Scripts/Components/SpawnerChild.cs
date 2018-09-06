using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChild : MonoBehaviour 
{
    public Spawner spawner;
    public SpawnOncoming oncomingSpawner;

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.ObjectDestroyed();
        }
        else
        {
            oncomingSpawner.ObjectDestroyed();
        }
    }
}

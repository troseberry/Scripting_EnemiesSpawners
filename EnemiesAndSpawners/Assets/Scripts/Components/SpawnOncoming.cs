using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOncoming : MonoBehaviour
{
    public List<GameObject> prefabList = new List<GameObject>();

    public float spawnerSize = 1.0f;
    public bool spawnOnlyWhenVisible = true;

    private bool enemyDoesExist;

    public Bounds leftSideSpawn;
    public Bounds rightSideSpawn;
    private Vector2 spawnLocation;

    private Transform playerSpriteTransform;


    void Start ()
	{
        if (prefabList.Count == 0)
        {
            Debug.LogWarning("No prefabs set for spawner: " + gameObject.name);
        }

        playerSpriteTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("visual");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerPhyx"))
        {
            if (!enemyDoesExist) Spawn(playerSpriteTransform.localScale.x);
        }
    }

    void Spawn(float facingDir)
    {
        if (prefabList.Count == 0)
        {
            return;
        }

        // -1 left/ 1 right
        spawnLocation = (facingDir > 0) ? rightSideSpawn.center : leftSideSpawn.center;

        int index = Random.Range(0, prefabList.Count);
        GameObject prefab = prefabList[index];
        GameObject spawnedEnemy = Instantiate(prefab, spawnLocation, Quaternion.identity);

        SpawnerChild child = spawnedEnemy.AddComponent<SpawnerChild>();
        child.oncomingSpawner = this;

        enemyDoesExist = true;
    }

    public void ObjectDestroyed()
    {
        enemyDoesExist = false;
    }

    public void OnDrawGizmos()
    {

        Camera c = Camera.main;
        Vector3 size = new Vector3(spawnerSize, spawnerSize, spawnerSize);
        Bounds b = new Bounds(transform.position, size);
        if (c.IsVisible(b))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }


        Gizmos.DrawCube(transform.position, size);

        Gizmos.DrawIcon(transform.position, "neko.png");
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(leftSideSpawn.center, leftSideSpawn.extents);
        Gizmos.DrawWireCube(rightSideSpawn.center, rightSideSpawn.extents);
    }
}

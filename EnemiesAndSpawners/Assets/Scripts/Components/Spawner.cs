using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
   // Controls what the spawner WILL spawn
   public List<GameObject> prefabList = new List<GameObject>(); 

   // Will control how many and how often; 
   public uint maxAliveAtOnce = 1; 
   public float initialDelay = 0.0f; 
   public float delayBetweenSpawns = 0.0f; 

   // Controls when the object will spawn; 
   public bool spawnOnlyWhenVisible = true; 
   public float spawnerSize = 1.0f; 

   // internal controls for the system
   private uint aliveCount = 0; 
   private Timer spawnTimer = new Timer(); 

   //------------------------------------------------------------------------
   void Start() 
   {
      // let people know when they forgot to set this up; 
      if (prefabList.Count == 0) {
         Debug.LogWarning( "No prefabs set for spawner: " + gameObject.name ); 
      }

      spawnTimer.Start(initialDelay); 
   }

   //------------------------------------------------------------------------
   bool ShouldSpawn()
   {
      return spawnTimer.HasElapsed()
         && (aliveCount < maxAliveAtOnce)
         && IsVisible(); 
   }

   //------------------------------------------------------------------------
   bool IsVisible()
   {
      if (!spawnOnlyWhenVisible) {
         return true; // always visible; 
      }

      Camera c = Camera.main; 
      Vector3 size = new Vector3(spawnerSize, spawnerSize, spawnerSize);
      Bounds b = new Bounds( transform.position, size ); 
      return c.IsVisible(b); 
   }

   //------------------------------------------------------------------------
   void Update()
   {
      if (ShouldSpawn()) {
         Spawn(); 
      }
   }

   //------------------------------------------------------------------------
   void Spawn()
   {
      if (prefabList.Count == 0) {
         return; 
      }
      
      ++aliveCount; 

      // TODO: Give options for HOW it spawns; 
      int idx = Random.Range( 0, prefabList.Count ); 
      GameObject prefab = prefabList[idx]; 
      GameObject go = GameObject.Instantiate( prefab, transform.position, Quaternion.identity ); 

      SpawnerChild child = go.AddComponent<SpawnerChild>(); 
      child.spawner = this; 

      spawnTimer.Start( delayBetweenSpawns ); 
   }

   //------------------------------------------------------------------------
   public void ObjectDestroyed()
   {
      if (aliveCount > 0) {
         aliveCount--; 
      }
   }

   //------------------------------------------------------------------------
   // Displays information to the Scene view - will show
   // if it is currently visible, as well as an Icon to visualize this
   // "hidden" object; 
   public void OnDrawGizmos()
   {
      if (spawnOnlyWhenVisible) {
         Camera c = Camera.main; 
         Vector3 size = new Vector3(spawnerSize, spawnerSize, spawnerSize);
         Bounds b = new Bounds( transform.position, size ); 
         if (c.IsVisible(b)) {
            Gizmos.color = Color.green; 
         } else {
            Gizmos.color = Color.red; 
         } 

         Gizmos.DrawCube( transform.position, size ); 
      }

      Gizmos.DrawIcon( transform.position, "neko.png" ); 
   }
}

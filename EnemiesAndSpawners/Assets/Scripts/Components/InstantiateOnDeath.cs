using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[RequireComponent(typeof(Health))]
public class InstantiateOnDeath : MonoBehaviour 
{
   public GameObject prefabToSpawn; 
   public uint spawnCount = 1; 
   public Transform spawnLocation; // optional - otherwise it'll spawn locally; 
   public bool spawnAsChild = false; 

   void Start() 
   {
      Health h = GetComponent<Health>(); 
      h.onDeath.AddListener( OnDeath );

      // set this up - so I can just assume spawn location is always non-null; 
      if (spawnLocation == null) {
         spawnAsChild = false; 
         spawnLocation = transform; 
      }
   }

   void OnDeath()
   {
      if (prefabToSpawn == null) {
         return; 
      }

       // set this up - so I can just assume spawn location is always non-null; 
      if (spawnLocation == null) {
         spawnAsChild = false; 
         spawnLocation = transform; 
      }

      for (uint i = 0; i < spawnCount; ++i) {
         if (spawnAsChild) {
            GameObject.Instantiate( prefabToSpawn, spawnLocation ); 
         } else {
            GameObject.Instantiate( prefabToSpawn, spawnLocation.position, spawnLocation.rotation ); 
         }
      }
   }
}

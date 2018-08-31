using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class InstantiateOnDamage : MonoBehaviour 
{
   public GameObject prefabToInstantiate; 
   public Transform attachPoint; 
   public bool spawnAttached = false; 

   void Start() 
   {
	   Health health = GetComponent<Health>(); 
      health.onDamaged.AddListener( OnDamaged ); 
   }

   void OnDamaged( float d )
   {
      Transform attach = attachPoint; 
      if (attach == null) {
         attach = transform; 
      }

      if (spawnAttached) {
         GameObject.Instantiate( prefabToInstantiate, attach );
      } else {
         GameObject.Instantiate( prefabToInstantiate, attach.position, attach.rotation ); 
      }
   }
}

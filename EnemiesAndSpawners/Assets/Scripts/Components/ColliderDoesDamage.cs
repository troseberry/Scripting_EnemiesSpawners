using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDoesDamage : MonoBehaviour 
{
   public float damage = 1.0f; 

   public bool destroySelfOnHit = false; 
   public GameObject objectRoot = null;

    private bool didDamagePlayer = false;

   private void ApplyDamage( GameObject go )
   {
      Health health = go.GetComponentInParent<Health>();
      if (health != null) {
         health.Damage(damage);
            didDamagePlayer = true;
      }

      if (destroySelfOnHit) {
         GameObject objToDestroy = objectRoot;
         if (objectRoot == null) {
            objToDestroy = gameObject;
         }

         GameObject.Destroy(objToDestroy);
      }
   }

   private void OnTriggerEnter2D( Collider2D collision )
   {
      ApplyDamage( collision.gameObject ); 
   }

   private void OnTriggerStay2D( Collider2D collision )
   {
      ApplyDamage( collision.gameObject ); 
   }

    private void OnTriggerExit2D(Collider2D collision)
    {
        didDamagePlayer = false;
    }

    public bool DidDamagePlayer()
    {
        return didDamagePlayer;
    }
}
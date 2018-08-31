using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Health : MonoBehaviour 
{
   public float health = 1.0f; 
   public bool destroyOnDeath = true; // may want to just set him to a dead state - so make it optional; 
   
   public FloatEvent onDamaged = new FloatEvent(); 
   public UnityEvent onDeath = new UnityEvent();

   private int invulnerableCount = 0;

   public bool IsAlive()
   {
      return (health > 0.0f); 
   }

   public bool IsVulnerable()
   {
      return invulnerableCount == 0; 
   }

   public void PushInvulnerable( float time = 0.0f )
   {
      ++invulnerableCount;
      if (time > 0.0f) {
         StartCoroutine( StopAfter(time) ); 
      }
   }

   IEnumerator StopAfter( float t )
   {
      yield return new WaitForSeconds(t); 
      PopInvulnerable(); 
   }

   public void PopInvulnerable()
   {
      if (invulnerableCount > 0) {
         invulnerableCount--; 
      }
   }

   public void Damage( float d )
   {
      if (IsVulnerable() && IsAlive()) {
         health = Mathf.Max( 0.0f, health - d ); 
         onDamaged.Invoke(d); 
         if (health == 0.0f) {
            Kill(); 
         }
      }
   }

   public void Kill()
   {
      // just to be sure; 
      health = 0.0f; 
      onDeath.Invoke(); 

      if (destroyOnDeath) {
         GameObject.Destroy(gameObject); 
      }
   }

}

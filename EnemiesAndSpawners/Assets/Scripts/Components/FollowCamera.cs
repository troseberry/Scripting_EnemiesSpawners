using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temporary Camera - just so we can create larger levels (horizontal scrolling only)
// We'll get more advanced later; 
public class FollowCamera : MonoBehaviour 
{
   public GameObject target; 

   public bool fixX; 
   public bool fixY;

   void Update() 
   {
      if (target != null) {
         Vector2 pos = target.transform.position; 
         Vector3 cur_pos = transform.position; 

         if (!fixX) {
            cur_pos.x = pos.x; 
         }
         if (!fixY) {
            cur_pos.y = pos.y; 
         }

         transform.position = cur_pos; 
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.blue;
      Bounds b = Camera.main.GetWorldBounds(); 
      Gizmos.DrawWireCube( b.center, b.size ); 
   }
}

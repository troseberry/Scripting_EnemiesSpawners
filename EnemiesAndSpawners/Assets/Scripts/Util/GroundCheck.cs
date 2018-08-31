using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour 
{
   public Vector2 size;
   public LayerMask mask   = 1; 
   
   public bool isColliding = false;

   private void FixedUpdate()
   {
      Collider2D c = Physics2D.OverlapBox( transform.position, size, 0.0f, mask ); 
      isColliding = (c != null); 
   }

   public bool IsGrounded()
   {
      return isColliding; 
   }

   public bool IsAirborn()
   {
      return !isColliding; 
   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green; 
      Gizmos.DrawWireCube( transform.position, size );  
   }
}
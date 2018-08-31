using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
   // how much off screen do they have to be?
   public float radius = 1.0f; 

   // Update is called once per frame
   void Update() 
   {
      Camera mc = Camera.main; 
      Bounds b = mc.GetWorldBounds(); 

      Vector3 pos = transform.position;
      if (b.Contains(pos)) {
         return; 
      }

      Vector3 nearest = b.ClosestPoint(pos);
      float d2 = (nearest - pos).sqrMagnitude; 
      if (d2 > (radius * radius)) {
         GameObject.Destroy(gameObject); 
      }
   }
}
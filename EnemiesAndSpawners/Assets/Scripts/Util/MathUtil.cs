using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{ 
   //------------------------------------------------------------------------
   // Snaps a position to the closest multiple of rnd;
   public static float RoundToNearest( float x, float rnd ) 
   {
      if (rnd == 0.0f) {
         return x; 
      } else {
         float v = Mathf.Round(x / rnd) * rnd; 
         return v; 
      }
   }

   //------------------------------------------------------------------------
   public static float GetVelocityToReachHeight( float h, float g )
   {
      return Mathf.Sqrt( -2.0f * h * g ); 
   }
}

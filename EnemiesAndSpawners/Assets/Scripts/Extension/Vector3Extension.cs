using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension 
{ 
   public static Vector3 RoundToNearest( this Vector3 v, float x, float y, float z )
   {
      return new Vector3( MathUtil.RoundToNearest( v.x, x ), 
         MathUtil.RoundToNearest( v.y, y ), 
         MathUtil.RoundToNearest( v.z, z ) );  
   }

   public static Vector2 XY( this Vector3 v )
   {
      return new Vector2( v.x, v.y ); 
   }
}

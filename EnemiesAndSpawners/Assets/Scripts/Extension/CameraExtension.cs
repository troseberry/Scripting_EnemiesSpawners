using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtension
{
   //------------------------------------------------------------------------
   public static Bounds GetWorldBounds( this Camera c )
   {
      Matrix4x4 view = c.transform.worldToLocalMatrix; 

      // this is getting me the correct
      // bounds - but mirrored to the backside of the camera... :/
      // So adding in a flipz - not sure quite Y this is needed - wonder if Unity is adding 
      // a z depth flip for depth buffer purposes (inverted Z?)
      Matrix4x4 flipz = Matrix4x4.identity; 
      flipz[3,3] = -1.0f; 

      Matrix4x4 viewProj = c.projectionMatrix * flipz * view; 
      Matrix4x4 invViewProj = viewProj.inverse;

      Vector3[] points = new Vector3[] {
         new Vector4( -1.0f, -1.0f, -1.0f ),
         new Vector4(  1.0f, -1.0f, -1.0f ),
         new Vector4( -1.0f,  1.0f, -1.0f ),
         new Vector4(  1.0f,  1.0f, -1.0f ),
         new Vector4( -1.0f, -1.0f,  1.0f ),
         new Vector4(  1.0f, -1.0f,  1.0f ),
         new Vector4( -1.0f,  1.0f,  1.0f ),
         new Vector4(  1.0f,  1.0f,  1.0f )
      }; 

      for (uint i = 0; i < points.Length; ++i) {
         points[i] = invViewProj.MultiplyPoint(points[i]); 
      }

      Bounds b = new Bounds( points[0], Vector3.zero ); 
      for (uint i = 1; i < points.Length; ++i) {
         b.Encapsulate( points[i] ); 
      }

      return b; 
   }

   //------------------------------------------------------------------------
   public static bool IsVisible( this Camera c, Bounds b )
   {
      // most spawners will use this - so may be useful to cache off the planes; 
      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(c); 
      return GeometryUtility.TestPlanesAABB( planes, b ); 
   }
}
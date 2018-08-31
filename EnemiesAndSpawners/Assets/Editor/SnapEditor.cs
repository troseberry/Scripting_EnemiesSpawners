using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(SnapToGrid))]
public class SnapEditor : Editor 
{
   private void OnDisable()
   {
      Snap(); 
   }

   private void OnSceneGUI()
   {
      if (Event.current.type == EventType.MouseUp) {
         Snap(); 
      }
   }

   private void Snap()
   {
      SnapToGrid snap = target as SnapToGrid; 
      if (snap == null) {
         return; 
      }

      Vector3 position = snap.transform.position;
      Vector3 offset = new Vector3( snap.offsetX, snap.offsetY, 0.0f ); 
      position -= offset; 
      position = position.RoundToNearest( snap.snapX, snap.snapY, 0.0f ); 
      position += offset; 

      snap.transform.position = position; 
   }
}

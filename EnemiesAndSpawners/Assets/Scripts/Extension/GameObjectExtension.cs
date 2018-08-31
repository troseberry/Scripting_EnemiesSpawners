using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
   public static T GetOrCreateComponent<T>( this GameObject go ) where T : Component
   {
      T comp = go.GetComponent<T>(); 
      if (comp == null) {
         comp = go.AddComponent<T>(); 
      }

      return comp; 
   }
}

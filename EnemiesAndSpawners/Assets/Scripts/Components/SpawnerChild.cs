using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChild : MonoBehaviour 
{
   public Spawner spawner;

   private void OnDestroy()
   {
      spawner.ObjectDestroyed(); 
   }
}

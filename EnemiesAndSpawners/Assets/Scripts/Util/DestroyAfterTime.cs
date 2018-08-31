using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour 
{
   public float seconds = 3.0f; 
   private float lifetime = 0.0f; 
   
   // Update is called once per frame
   void Update() 
   {
      lifetime += Time.deltaTime; 
      if (lifetime >= seconds) {
         GameObject.Destroy(gameObject); 
      }
   }
}
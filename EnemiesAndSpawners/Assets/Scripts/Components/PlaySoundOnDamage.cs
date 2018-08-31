using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlaySoundOnDamage : MonoBehaviour 
{
   public AudioClip clip; 
   public bool playOnlyWhenAlive = true; // Clip will only play if the damage didn't kill the unit; 

   private Health health; 

   //------------------------------------------------------------------------
   void Start() 
   {
      health = GetComponent<Health>(); 
      health.onDamaged.AddListener( OnDamaged ); 
   }
   
   //------------------------------------------------------------------------
   void OnDamaged( float d )
   {
      if ((clip != null) 
         && (!playOnlyWhenAlive || health.IsAlive())) {

         AudioUtil.PlayOneOffAt( transform, clip ); 
      }
   }
}

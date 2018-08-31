using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyOnAudioFinished : MonoBehaviour 
{
   private AudioSource source; 

   private void Start()
   {
      source = GetComponent<AudioSource>(); 
   }

   private void Update()
   {
      if ((source == null) || (!source.isPlaying)) {
         GameObject.Destroy(gameObject); 
      }
   }
}
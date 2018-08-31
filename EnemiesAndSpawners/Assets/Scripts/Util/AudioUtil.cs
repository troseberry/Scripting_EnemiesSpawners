using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUtil 
{
   // Will play an audio clip located at the supplied world position
   public static void PlayOneOffAt( Vector3 position, AudioClip clip )
   {
      if (clip == null) {
         return; 
      }

      GameObject go = new GameObject( "__audio[" + clip.name + "]" ); 
      go.transform.SetPositionAndRotation( position, Quaternion.identity ); 

      AudioSource source = go.AddComponent<AudioSource>(); 
      source.clip = clip;
      source.Play(); 

      go.AddComponent<DestroyOnAudioFinished>(); 
   }

   // Will play an audio clip located at where the transform is; 
   public static void PlayOneOffAt( Transform pos, AudioClip clip )
   {
      PlayOneOffAt( pos.position, clip ); 
   }
}
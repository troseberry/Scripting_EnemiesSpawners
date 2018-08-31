using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour 
{
   public AudioClip clipToPlay;

   public void Start()
   {
      AudioUtil.PlayOneOffAt( transform, clipToPlay ); 
   }
}

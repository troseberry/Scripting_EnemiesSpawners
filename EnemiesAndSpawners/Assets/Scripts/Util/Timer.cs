using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
   public float duration; 
   public float endTime; 

   public Timer()
   {
      duration = -1.0f; 
      endTime = -1.0f; 
   }

   public void Stop()
   {
      duration = -1.0f; 
   }

   public bool IsValid()
   {
      return duration >= 0.0f; 
   }

   public void Start( float seconds )
   {
      duration = seconds;
      endTime = Time.time + seconds; 
   }
   
   public bool HasElapsed()
   {
      return IsValid() && (Time.time >= endTime); 
   }

   public bool IsRunning()
   {
      return IsValid() && (Time.time < endTime); 
   }
}
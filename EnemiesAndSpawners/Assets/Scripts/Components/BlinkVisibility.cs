using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BlinkVisibility : MonoBehaviour 
{
   public float onTimeSeconds = .1f; 
   public float offTimeSeconds = .1f; 

   private Timer swapTimer = new Timer(); 
   private Timer durationTimer = new Timer(); 

   private new Renderer renderer; 
   private bool isVisible = true;

   void Awake() 
   {
      renderer = GetComponent<Renderer>(); 
   }

   private void OnDestroy()
   {
      ToggleVisibility(true); 
   }

   void Update() 
   {
      if (swapTimer.HasElapsed()) {
         ToggleVisibility(); 
      }

      if (durationTimer.HasElapsed()) {
         swapTimer.Stop(); 
         durationTimer.Stop(); 
         ToggleVisibility(true); 
      }
   }

   void ToggleVisibility()
   {
      if (isVisible) {
         swapTimer.Start(offTimeSeconds); 
      } else {
            swapTimer.Start(onTimeSeconds); 
      }
      ToggleVisibility(!isVisible); 
   }

   void ToggleVisibility( bool vis )
   {
      isVisible = vis; 
      renderer.enabled = isVisible; 
   }

   public void SetOptions( float duration, float blinkTime )
   {
      durationTimer.Start(duration); 
      offTimeSeconds = blinkTime; 
      onTimeSeconds = blinkTime; 

      ToggleVisibility(); 
   }

   public static void OnObject( GameObject go, float duration, float blinkTime )
   {
      Renderer r = go.GetComponent<Renderer>(); 
      if (r != null) {
         BlinkVisibility blink = go.GetOrCreateComponent<BlinkVisibility>(); 
         blink.SetOptions( duration, blinkTime ); 
      }
   }
}

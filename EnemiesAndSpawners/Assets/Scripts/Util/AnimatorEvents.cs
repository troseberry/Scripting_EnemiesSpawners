using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorEvents : MonoBehaviour 
{
   private Animator animator; 
   
   public void DestroyObject()
   {
      GameObject.Destroy(gameObject); 
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class, or "type" of object.  
// Can be given any name, in this case MoveTowardPlayer. 
// Try to pick a name that describes what the
// object is or does; 
// (some schools teach classes the the "noun" and methods as the "verb", 
// but Unity's Entity/Component model subverts that rule)
public class MoveTowardPlayer : MonoBehaviour // : MonoBehaviour means this class "inherits" from MonoBehaviour - or has all the properties of that as well as what I type; 
                                              //   Being a MonoBehaviour allows it to be attached to objects in Unity and receive game specific events; 
{
   // I am a comment - I don't affect the script at all, but
   // am here to annotate - these help future you understand what 
   // you were doing - especially useful when code it cryptic.

   // MEMBERS - variables used to control this script

   // public - access modifier -> for now
   // know public will make it visible in the inspector, private will hide it;
   public float speed = 1.0f; 
   public bool moveManhattan = false; 

   // TODO: can make this easier to use; 
   // https://answers.unity.com/questions/1378822/list-of-tags-in-the-inspector.html
   public string targetTag = "Player"; 

   // does not show up in inspector (and not visible to other classes)
   private GameObject player; 

   //------------------------------------------------------------------------
   // This is a method (usually a verb or action the object can do)
   // In Unity, some methods are special, in that Unity will call them for us
   // For example, this one is called when an object first appears in the scene (active and enabled)
   // Nowadays - Visual Studio will highlight these special methods in "blue" to signify 
   // they're special. 
   void Start() 
   {  // everything inside curly braces has the "scope". In this case, everything between this curly
      // brace and the one that matches it is the "body" of Start (belongs to Start)
      
      // save off the player we want to follow.
      // I'm using a Unity function to search for an object with a specific "tag", or 
      // annotation on the object to help us identify it.

      // or a way to markup a scene; 
      player = GameObject.FindGameObjectWithTag(targetTag); 

   } // Ends Start()
      
   //------------------------------------------------------------------------
   // Called every frame while active & enabled.  
   void Update() 
   {
      Vector2 target = GetTarget();
      
      Vector2 diff = target - transform.position.XY();  
      Vector2 dir = diff.normalized; 

      Vector2 moveDir = dir; 
      if (moveDir == Vector2.zero) {
         return; 
      }

      // convert to cardinal direction - math be a useful extension of vector2...
      if (moveManhattan) {
         if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y)) {
            moveDir.x /= Mathf.Abs(moveDir.x); 
            moveDir.y = 0.0f; 
         } else {
            moveDir.y /= Mathf.Abs(moveDir.y); 
            moveDir.x = 0.0f; 
         }
      }

      Vector2 disp = moveDir * speed * Time.deltaTime; 
      transform.position = transform.position + new Vector3(disp.x, disp.y, 0.0f); 
   }
   
   //------------------------------------------------------------------------
   // A method we wrote.  
   // It returns a position in the world for where this script
   // wants to go.
   Vector2 GetTarget()
   {
      if (player == null) { // if we don't have a player to follow...
         return Vector2.zero;  // then move toward the center of the world
      } else { // else...
         return player.transform.position;  // move toward the player's position.
      }
   }
}

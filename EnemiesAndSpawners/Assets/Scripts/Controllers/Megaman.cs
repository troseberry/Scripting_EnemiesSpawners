using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))] // forces any object with this component to also have an Animator component; 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Megaman : MonoBehaviour 
{
   // Design controls
   public uint maxProjectiles       = 3; 
   public float projectileSpeed     = 10.0f; 
   public float shootDuration       = .125f;    // how long to hold a shoot pose
   public float runSpeed            = 2.0f;
   public float jumpHeight          = 3.0f; 
   public float fallGravityScale    = 2.5f; 
   public float maxFallSpeed        = 10.0f; 
   public float hurtTimeSeconds     = .5f; 
   public float invulnerableTimeSeconds = 1.0f; 

   public Transform shotLocation;
   public Transform centerAnchor; 
   public GameObject lemonPrefab; 

   public AudioClip landSound; 
   public AudioClip shotSound; 

   public GameObject damageEffect; 

   public Transform visual; 
   public GroundCheck groundCheck; 

   public float knockbackSpeed = 2.0f; 
   public float knockbackLift = .5f; 

   // usually try to keep flags where "false" can be the default case
   // as that is what they will default to if people forget to set them; 
   // public only for debug purposes
   public bool isRunning      = false;
   public bool isAirborn      = false;
   public bool isHurt         = false; 

   // controlling variables
   // Instead of reading input directly I will usually
   // update these, and move based on them - allows 
   // me to to use this with other kinds of input controllers later;
   public float moveX         = 0.0f; 
   public bool jump           = false; 
   public bool shoot          = false; 

   // Used to save off information
   private Animator animator;
   private int shootingBlendID;
   private int isAirbornID;
   private int isWalkingID; 
   private int isHurtID; 
   private int hurtTriggerID; 

   private new Rigidbody2D rigidbody;  // new here hides Unity's "rigidbody" declaration at this scope; 

   // Gameplay helpers - used to detect state of the game that I can't get in a single frame;
   private Vector3 positionLastFrame; // used to detect if I'm actually moving (don't run if you're running into a wall)
   private bool wasAirborn = false; 
   private bool canJump = false; 
   private bool isJumping = false; 
   private bool isLaunched = false; 

   private bool forceVelocity = false; 
   private float forcedVelocity = 0.0f; 

   private List<GameObject> projectiles; 

   private Timer shotTimer; 
   private Timer hurtTimer; 
   private Timer invulnerableTimer; 


   private void Awake()
   {
      animator = GetComponent<Animator>(); 
      isAirbornID = Animator.StringToHash("IsAirborn");
      isHurtID = Animator.StringToHash("IsHurt"); 
      hurtTriggerID = Animator.StringToHash("HurtTrigger"); 
      isWalkingID = Animator.StringToHash("IsWalking"); 
      shootingBlendID = Animator.StringToHash("ShootBlend");

      rigidbody = GetComponent<Rigidbody2D>(); 

      projectiles = new List<GameObject>(); 
      shotTimer = new Timer(); 
      hurtTimer = new Timer(); 
      invulnerableTimer = new Timer(); 
   }

   private void Start() 
   {
      Health health = GetComponent<Health>(); 
      health.onDamaged.AddListener( OnDamaged ); 
   }

   void OnDamaged( float d )
   {
      Health health = GetComponent<Health>();

      if (health.IsAlive()) {

         // set the animation control variables
         // is hurt also disables player input (hack)
         isHurt = true;
         animator.SetTrigger( hurtTriggerID ); 
         animator.SetBool( isHurtID, true ); 

         // dictate how hurt I am; 
         hurtTimer.Start( hurtTimeSeconds ); 

         health.PushInvulnerable();
         invulnerableTimer.Start( invulnerableTimeSeconds ); 
         BlinkVisibility.OnObject( visual.gameObject, invulnerableTimeSeconds, .15f ); 

         // on hit - knock the player the oppsoite direction to what they're facing; 
         float launchDir = -GetFacing(); 
         float vel_y = MathUtil.GetVelocityToReachHeight( knockbackLift, Physics2D.gravity.y ); 
         Vector2 launchVel = new Vector2( launchDir * knockbackSpeed, vel_y ); 
         Launch( launchVel ); 
         ForceVelocity( launchVel.x ); 
      }
   }

   IEnumerator DoDamageEffect()
   {
      Health health = GetComponent<Health>();
      health.PushInvulnerable(); 

      // disable input
      isHurt = true; 

      // flash visibility 
      // push back along where we're going
      yield return new WaitForSeconds(.5f);
      
      isHurt = false;
      health.PopInvulnerable();
   }

   private void Update() 
   {
      // disable hurt once we're grounded and 
      if (isHurt) {
         if (hurtTimer.HasElapsed()) {
            StopForcedVelocity(); 
            hurtTimer.Stop(); 
            isHurt = false;
         }
      }

      if (invulnerableTimer.HasElapsed()) {
         Health h = GetComponent<Health>(); 
         h.PopInvulnerable(); 
      }

      UpdateInputs(); 
      // figure out if the player is moving
      // we will consider them moving if 

      // let the animation system know I should play a running animation; 
      UpdateFacing(); 
         
      if (shoot) {
         Shoot(); 
      }
      
      // so how do I know if I can jump
      // jump has been toggled true since landing
      canJump = !isAirborn && canJump; 
      bool wasJumping = isJumping; 

      if (!canJump) {
         canJump = !isAirborn && !jump; 
      } else {
         isJumping = canJump && jump; 
      }

      // you're jumping as long as you hold jump
      // then you're stuck not jumping again until canJump is true; 
      isJumping = isJumping && jump; 

      if (isJumping && (isJumping != wasJumping)) {
         StartJump(); 
      }

      UpdateAnimator(); 	
   }

   

   void StartJump()
   {
      
      float g = Physics2D.gravity.y * 1.1f; // nudge; math is right but simulation will leave us short - so pretend we're fighting slightly stronger gravity; 
      float vel_y = MathUtil.GetVelocityToReachHeight( jumpHeight, g ); 

      Vector2 vel = rigidbody.velocity;
      vel.y = vel_y; 
      rigidbody.velocity = vel; 
   }

   void Launch( Vector2 vel )
   {
      isLaunched = true; 
      rigidbody.velocity = vel; 
   }

   void Landed()
   {
      AudioUtil.PlayOneOffAt( transform, landSound ); 
      
      // we can jump as son as jump is pressed
      // and it has been released since landing
      canJump = !jump; 
      isLaunched = false; 
   }

   int GetProjectileCount()
   {
      for (int i = projectiles.Count - 1; i >= 0; --i) {
         if (projectiles[i] == null) {
            projectiles[i] = projectiles[projectiles.Count - 1]; 
            projectiles.RemoveAt(projectiles.Count - 1); 
         }
      }

      return projectiles.Count; 
   }

   void Shoot()
   {
      int projectileCount = GetProjectileCount(); 
      if (projectileCount >= maxProjectiles) {
         return; 
      }

      AudioUtil.PlayOneOffAt( transform, shotSound ); 
      GameObject lemon = GameObject.Instantiate( lemonPrefab, shotLocation.transform.position, Quaternion.identity ); 

      Rigidbody2D rb = lemon.GetComponent<Rigidbody2D>(); 
      rb.velocity = new Vector2( GetFacing() * projectileSpeed, 0.0f ); 

      projectiles.Add( lemon ); 
      shotTimer.Start(shootDuration); 
   }

   public void ForceVelocity( float vx )
   {
      forceVelocity = true; 
      forcedVelocity = vx; 
   }

   public void StopForcedVelocity()
   {
      forceVelocity = false; 
   }

   private void FixedUpdate()
   {
      // actually updating physics I'll do in the FixedUpdate
      // as it'll be more consistent
      float vx = moveX; 
      vx *= runSpeed; 

      // if velocity is being forced by an outside force
      // add it here; 
      if (forceVelocity) {
         vx = forcedVelocity; 
      }

      // TODO: Add environmental forces here (wind/terrain/etc)

      // will deal with jumping in a bit;  For now, keep his current velocity
      float vy = rigidbody.velocity.y; 
      vy = Mathf.Max( vy, -maxFallSpeed ); 


      isAirborn = groundCheck.IsAirborn() || (vy > 0.0f); 
      if ((isAirborn != wasAirborn) && wasAirborn) {
         Landed(); 
      }

      if (isAirborn) {
         if (vy < 0.0f) {
            rigidbody.gravityScale = fallGravityScale; 
         } else if (!isJumping && !isLaunched) {
            vy *= .01f;
         }
      } else {
         rigidbody.gravityScale = 1.0f; 
      }

      rigidbody.velocity = new Vector2(vx, vy); 
      wasAirborn = isAirborn; 

      // update information for animation;
      float hx = moveX; 
      Vector3 movement = transform.position - positionLastFrame; 
      isRunning = (hx != 0.0f) && (Mathf.Abs(movement.x) > 0.001f); 
      positionLastFrame = transform.position; 
   }

   private void UpdateInputs()
   {
      // no input while hurt; 
      if (isHurt) {
         moveX = 0.0f;
         jump = false; 
         shoot = false; 
         return; 
      }

      moveX = Input.GetAxis("Horizontal"); 
      // we move or we don't.  If we're not holding the control hard - just stop movement.
      // otherwise normalize the movement; 
      if (Mathf.Abs(moveX) < .25f) {
         moveX = 0.0f; 
      } else {
         moveX /= Mathf.Abs(moveX); 
      }

      shoot = Input.GetButtonDown("Fire1"); 
      jump = Input.GetButton("Jump"); 
   }

   private void UpdateFacing()
   {
      if (moveX < -0.0001f) {
         FaceDirection( -1.0f );  
      } else if (moveX > 0.0001f) {
         FaceDirection( 1.0f ); 
      }
   }

   private float GetFacing()
   {
      return visual.localScale.x; 
   }

   private void FaceDirection( float dir )
   {
      Vector3 scale = visual.localScale;
      scale.x = dir * Mathf.Abs(scale.x); 
      visual.localScale = scale; 
   }

   private void UpdateAnimator()
   {
      animator.SetBool( isAirbornID, isAirborn ); 
      animator.SetBool( isWalkingID, isRunning ); 
      animator.SetBool( isHurtID, isHurt ); 

      // I can only blend on floats, so going to convert a bool to a float
      // used a "turnary" operator, which is a short hand if/else statement; 
      // (technically, you could cast a bool to a float and it would accomplish 
      // the same thing, but this is more explicit)
      bool isShooting = shotTimer.IsRunning(); 
      animator.SetFloat( shootingBlendID, isShooting ? 1.0f : 0.0f ); 
   }
}

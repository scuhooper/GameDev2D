using UnityEngine;
using System.Collections;

public class BexPlayer : MonoBehaviour {
	public float speed;	// horizontal force applied every frame while character is moving
	public float jumpForce;	// upward force applied when character jumps
	public float dashSpeed;	// horizontal force applied when character dashes
	public float jumpKickSpeed; // controls how fast the kick goes in both horizontal and downward direction
	public float fireRate;	// how many bullets can be fired per second
	public GameObject bullet;	// hook for bullet prefab

	Rigidbody2D rb;	// rigidbody2d component
	Animator anim;	// animator component
	bool bIsFacingRight;	// bool for keeping track of which direction the sprite is facing (true = right; false = left)
	bool bIsMoving;	// bool for if the character is moving
	bool bIsGrounded;	// bool for if the character is currently on the ground
	bool bIsDashing;	// bool for dashing
	bool bIsShooting;	// bool for shooting
	bool bIsKicking;    // bool for kicking
	float timeLastFired;	// keep track of last time a bullet was fired

	// Use this for initialization
	void Start () {
		Init();	// call function to initialize private variables
	}
	
	// Update is called once per frame
	void Update () {
		MoveCharacter();
	}

	// Initialize all private variables of BexPlayer
	void Init()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		bIsFacingRight = true;	// character starts off facing the right
		bIsMoving = false;
		bIsGrounded = true;
		bIsDashing = false;
		bIsShooting = false;
		bIsKicking = false;
		timeLastFired = float.MinValue;
	}

	void MoveCharacter()
	{
		if ( bIsKicking || bIsDashing )	// if we are kicking or dashing, player cannot perform other actions or change their speed or direction
			return;
		else
		{
			// handle input for moving
			if ( Input.GetKey( KeyCode.A ) )
			{
				// flip the sprite if we start moving left and are facing right
				if ( bIsFacingRight )
					FlipSprite();

				// rb.AddForce( new Vector2( -speed, 0 ) );
				rb.velocity = new Vector2( -speed, rb.velocity.y );
				anim.SetBool( "bIsMoving", true );  // start the animator to show movement
				bIsMoving = true;
			}
			else if ( Input.GetKey( KeyCode.D ) )
			{
				// flip the sprite if we start moving right and are facing left
				if ( !bIsFacingRight )
					FlipSprite();

				// rb.AddForce( new Vector2( speed, 0 ) );
				rb.velocity = new Vector2( speed, rb.velocity.y );
				anim.SetBool( "bIsMoving", true );  // start the animator to show movement
				bIsMoving = true;
			}
			else
			{
				anim.SetBool( "bIsMoving", false ); // tell animator we are no longer moving
				bIsMoving = false;
				rb.velocity = new Vector2( 0, rb.velocity.y );
			}

			// special movements
			if ( Input.GetKeyDown( KeyCode.Space ) )
			{
				Jump();
			}

			if ( Input.GetKeyDown( KeyCode.LeftShift ) && bIsGrounded && !bIsDashing )
			{
				Dash();
			}

			if ( Input.GetMouseButton( 0 ) && !bIsKicking && !bIsDashing )
			{
				ShootGun();
			}
			else if ( Input.GetMouseButtonDown( 1 ) && !bIsDashing )
			{
				Kick(); // this will not run if player does not stop shooting for minimum 1 frame
			}
			else
			{
				bIsShooting = false;
				anim.SetBool( "bIsShooting", false );
			}
		}
	}

	void Jump()
	{
		// check to see if character is on the ground
		if ( bIsGrounded )
		{
			// rb.AddForce( new Vector2( 0, jumpForce ) ); // apply the jumpforce
			rb.velocity = new Vector2( rb.velocity.x, jumpForce );
			anim.SetBool( "bIsJumping", true );	// start the animation for jumping
		}
	}

	void Dash()
	{
		if ( bIsFacingRight )	// check direction of sprite
		{
			rb.velocity = new Vector2( dashSpeed, 0 );	// add dash speed to characted
		}
		else
		{
			rb.velocity = new Vector2( -dashSpeed, 0 );	// add dash speed to characted
			// rb.AddForce( new Vector2( -dashSpeed, 0 ) );
		}

		anim.SetBool( "bIsDashing", true ); // start animation for dashing
		bIsDashing = true;  // set boolean to prevent changes in direction or speed
		Invoke( "IsDashing", .5f ); // call a function to reset dash-related variables after .5 seconds
	}

	void ShootGun()
	{
		bIsShooting = true;	// we are currently shooting
		anim.SetBool( "bIsShooting", true );    // tell animator we are shooting
		if ( Time.time > timeLastFired + 1 / fireRate )	// fire a new bullet if the fire rate allows
		{
			if ( bIsFacingRight)	// change spawn position based on direction sprite is facing
				Instantiate( bullet, transform.position + new Vector3( 1.011f, .833f ), transform.rotation );	// spawn bullet in game, has offset to appear to be fired from weapon
			else
				Instantiate( bullet, transform.position + new Vector3( -1.011f, .833f ), transform.rotation );	// spawn bullet in game, has offset to appear to be fired from weapon
			timeLastFired = Time.time;	// update time last fired to current time
		}
	}
	void Kick()
	{
		if ( bIsKicking )	// leave function if already kicking
			return;

		bIsKicking = true;	// start kicking

		if ( bIsGrounded )	// check if we are on the ground
		{
			anim.SetTrigger( "Kick" );	// start animator for ground kick
			Invoke( "IsKicking", .33f );	// call function to exit kicking state after .33 seconds (approx. time of ground kick)
		}
		else // we are in the air/jumping
		{
			anim.SetBool( "bIsKicking", true );	// tell animator to start jump kick animation
			if ( bIsFacingRight )   // check direction of sprite and set the velocity of our kick
				rb.velocity = new Vector2( jumpKickSpeed, -jumpKickSpeed );
			else
				rb.velocity = new Vector2( -jumpKickSpeed, -jumpKickSpeed );
		}

		if ( bIsMoving && bIsGrounded )	// if we are moving and are on the ground, stop all movement to perform kick
		{
			rb.velocity = Vector2.zero;
		}
	}

	void ResetKickTrigger()
	{
		anim.ResetTrigger( "Kick" );
	}
	// make sure sprite faces the direction moving
	void FlipSprite()
	{
		if ( bIsFacingRight )	// if we are facing right, we need to change
			GetComponent<SpriteRenderer>().flipX = true;
		else
			GetComponent<SpriteRenderer>().flipX = false;	// turn back to the right
		bIsFacingRight = !bIsFacingRight;	// switch the bool back and forth everytime the function is called
	}

	public void SetIsGrounded( bool val )
	{
		bIsGrounded = val;
		if ( bIsGrounded )  // if we are on the ground, make sure animator knows we are no longer jumping
		{
			anim.SetBool( "bIsJumping", false );
			IsKicking();
		}
	}

	void IsDashing()
	{
		anim.SetBool( "bIsDashing", false );	// stop dashing animation
		bIsDashing = false;	// inform game logic we are no longer dashing
	}

	public void IsKicking()
	{
		bIsKicking = false;
		// stop kick animation
		anim.SetBool( "bIsKicking", false );
	}

	void ResetKick()
	{
		anim.ResetTrigger( "Kick" );	// reset kick trigger
	}

	/// <summary>
	/// Returns the value of bIsFacingRight
	/// </summary>
	/// <returns></returns>
	public bool IsFacingRight()
	{
		return bIsFacingRight;
	}
}

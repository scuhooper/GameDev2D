using UnityEngine;
using System.Collections;

public class BexPlayer : MonoBehaviour {
	public float speed;
	public float jumpForce;
	public float dashSpeed;

	Rigidbody2D rb;
	Animator anim;
	bool bIsFacingRight;
	bool bIsMoving;
	bool bIsGrounded;
	bool bIsDashing;
	bool bIsShooting;
	bool bIsKicking;
	Vector2 newVelocity;

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		MoveCharacter();
	}

	void Init()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		bIsFacingRight = true;
		bIsMoving = false;
		bIsGrounded = true;
		bIsDashing = false;
		bIsShooting = false;
		bIsKicking = false;
	}

	void MoveCharacter()
	{
		// handle input for moving
		if ( Input.GetKey( KeyCode.A ) )
		{
			// flip the sprite if we start moving left and are facing right
			if ( bIsFacingRight )
				FlipSprite();

			rb.AddForce( new Vector2( -speed, 0 ) );
			anim.SetBool( "bIsMoving", true );  // start the animator to show movement
			bIsMoving = true;
		}
		else if ( Input.GetKey( KeyCode.D ) )
		{
			// flip the sprite if we start moving right and are facing left
			if ( !bIsFacingRight )
				FlipSprite();

			rb.AddForce( new Vector2( speed, 0 ) );
			anim.SetBool( "bIsMoving", true );  // start the animator to show movement
			bIsMoving = true;
		}
		else
		{
			anim.SetBool( "bIsMoving", false ); // tell animator we are no longer moving
			bIsMoving = false;
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
			Kick();
		}
		else
		{
			bIsShooting = false;
			anim.SetBool( "bIsShooting", false );
		}
	}

	void Jump()
	{
		// check to see if character is on the ground
		if ( bIsGrounded )
		{
			rb.AddForce( new Vector2( 0, jumpForce ) );	// apply the jumpforce
			anim.SetBool( "bIsJumping", true );	// start the animation for jumping
		}
	}

	void Dash()
	{
		if ( bIsFacingRight )
		{
			rb.AddForce( new Vector2( dashSpeed, 0 ) );
			anim.SetBool( "bIsDashing", true );
			bIsDashing = true;
			Invoke( "IsDashing", .5f );
		}
		else
		{
			rb.AddForce( new Vector2( -dashSpeed, 0 ) );
			anim.SetBool( "bIsDashing", true );
			bIsDashing = true;
			Invoke( "IsDashing", .5f );
		}
	}

	void ShootGun()
	{
		bIsShooting = true;
		anim.SetBool( "bIsShooting", true );
	}
	void Kick()
	{
		if ( bIsKicking )	// leave function if already kicking
			return;

		bIsKicking = true;

		if ( bIsGrounded )
		{
			anim.SetTrigger( "Kick" );
		}
		else
			anim.SetBool( "bIsKicking", true );

		if ( bIsMoving && bIsGrounded )
		{
			rb.velocity = Vector2.zero;
		}
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
		anim.SetBool( "bIsDashing", false );
		bIsDashing = false;
	}

	public void IsKicking()
	{
		bIsKicking = false;
		// stop kick animation
		anim.SetBool( "bIsKicking", false );
	}

	void ResetKick()
	{
		anim.ResetTrigger( "Kick" );
	}
}

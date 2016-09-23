using UnityEngine;
using System.Collections;

public class BexPlayer : MonoBehaviour {
	public float speed;
	public float jumpForce;

	Rigidbody2D rb;
	Animator anim;
	bool bIsFacingRight;
	bool bIsGrounded;
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
		bIsGrounded = true;
	}

	void MoveCharacter()
	{
		//newVelocity = rb.velocity;
		if ( Input.GetKey( KeyCode.A ) )
		{
			if ( bIsFacingRight )
				FlipSprite();

			rb.AddForce( new Vector2( -speed, 0 ) );
			// rb.MovePosition( new Vector3( -speed * Time.deltaTime, 0, 0 ) + transform.position );
			anim.SetBool( "bIsMoving", true );
		}
		else if ( Input.GetKey( KeyCode.D ) )
		{
			if ( !bIsFacingRight )
				FlipSprite();

			rb.AddForce( new Vector2( speed, 0 ) );
			//rb.MovePosition( new Vector3( speed * Time.deltaTime, 0, 0 ) + transform.position );
			anim.SetBool( "bIsMoving", true );
		}
		else
		{
			anim.SetBool( "bIsMoving", false );
		}

		if ( Input.GetKeyDown( KeyCode.Space ) )
		{
			Jump();
		}

		//rb.velocity = newVelocity;
	}

	void Jump()
	{
		if ( bIsGrounded )
		{
			rb.AddForce( new Vector2( 0, jumpForce ) );
			anim.SetBool( "bIsJumping", true );
		}
	}

	void FlipSprite()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		bIsFacingRight = !bIsFacingRight;
	}

	void OnCollisionEnter2D( Collision2D col )
	{
		if ( col.gameObject.CompareTag( "Ground" ) )
		{
			bIsGrounded = true;
			anim.SetBool( "bIsJumping", false );
		}
	}

	void OnCollisionExit2D( Collision2D col )
	{
		bIsGrounded = false;
	}
}

using UnityEngine;
using System.Collections;

public class Forklift : MonoBehaviour {
	public int health;
	public int damage;
	public GameObject player;	// hook for player character in editor
	public float chargeDuration;	// how long does the charge last
	public float timeBetweenCharges;	// how often can we start a charge
	public float chargeSpeed;   // how fast is the charge
	public float maxChargeDistance;	// how close does the player need to be to charge at them

	// basic state booleans
	bool bIsActive;
	bool bIsFacingLeft;
	bool bIsCharging;
	bool bIsTired;

	// component variables
	Rigidbody2D rb;
	SpriteRenderer sprite;

	float timeIdle;	// amount of time spent idle

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if ( bIsActive && !bIsCharging )	// code only runs if the player is inside the trigger collider and the forklift is not actively charging
		{
			Debug.Log( "Forklift is active!" );
			float distance = Vector3.Distance( transform.position, player.transform.position );	// calculate the distance between the forklift and the player
			if ( distance < maxChargeDistance && !bIsTired )	// is the player within x units of this object and have we recovered from charging 
			{
				if ( transform.position.x - player.transform.position.x > 0 )	// if the difference between x values is positive, the player is to the left
				{
					if ( !bIsFacingLeft )	// change direction if unit is facing the wrong way
						ChangeDirection();
				}
				else    // player is to the right
				{
					if ( bIsFacingLeft )	// change direction if unit is facing the wrong way
						ChangeDirection();
				}
				Charge();	// charge at the player
			}
			else    // player is either too far away or unit is recovering
			{
				Idle();	// just do normal behavior
			}
		}
	}

	// initialize variables
	void Init()
	{
		bIsActive = false;
		bIsFacingLeft = true;
		bIsCharging = false;
		bIsTired = false;
		timeIdle = 0;
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Just changes the bool of bIsActive
	/// </summary>
	void Activate()
	{
		bIsActive = !bIsActive;
	}

	/// <summary>
	/// Charge in the player's horizontal direction
	/// </summary>
	void Charge()
	{
		// determine which direction we are facing and move that direction
		if ( bIsFacingLeft )
			rb.velocity = new Vector2( -chargeSpeed, 0 );
		else
			rb.velocity = new Vector2( chargeSpeed, 0 );

		// update state booleans
		bIsCharging = true;
		bIsTired = true;
		Invoke( "IsCharging", chargeDuration );	// call function to reset bIsCharging after designer-specified delay
		Invoke( "IsTired", timeBetweenCharges );    // call function to reset bIsTired after designer-specified delay
	}

	/// <summary>
	/// Run basic idle logic
	/// </summary>
	void Idle()
	{
		if ( timeIdle > 2 )	// if we have been idle for more than 2 seconds
		{
			// reset the time time idle to 0 and swap the direction being moved
			timeIdle = 0;
			ChangeDirection();
		}
		else
		{
			timeIdle += Time.deltaTime;	// increment the time idle
			// move slightly based on the direction facing
			if ( bIsFacingLeft )
				rb.velocity = new Vector2( -1, 0 );
			else
				rb.velocity = new Vector2( 1, 0 );
		}
	}

	// reset bIsCharging to false
	void IsCharging()
	{
		bIsCharging = false;
	}

	// reset bIsTired to false
	void IsTired()
	{
		bIsTired = false;
	}

	/// <summary>
	/// Changes bIsFacingLeft and flips the sprite renderer to face the new direction
	/// </summary>
	void ChangeDirection()
	{
		bIsFacingLeft = !bIsFacingLeft;
		sprite.flipX = !sprite.flipX;
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if (col.gameObject.tag == "Player")	// check if the player is in the trigger
			Activate();	// activate the object to run it's logic
	}

	void OnTriggerExit2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )	// if the player leaves the trigger volume
		{
			Activate();	// deactivate the object and stop running it's logic
			Debug.Log( "Forklift is inactive!" );
		}
	}
}

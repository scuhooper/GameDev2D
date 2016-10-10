using UnityEngine;
using System.Collections;

public class Forklift : MonoBehaviour, IKillable, IDamageable {
	public int health;
	public int damage;
	public GameObject player;	// hook for player character in editor
	public float chargeDuration;	// how long does the charge last
	public float timeBetweenCharges;	// how often can we start a charge
	public float chargeSpeed;   // how fast is the charge
	public float maxChargeDistance; // how close does the player need to be to charge at them
	public float blinkTime; // how long does the object blink
	public AudioSource deathClip;	// editor hook for death noise

	// basic state booleans
	bool bIsActive;
	bool bIsFacingLeft;
	bool bIsCharging;
	bool bIsTired;
	bool bDamagedRecently;

	// component variables
	Rigidbody2D rb;
	AudioSource source;

	float timeIdle; // amount of time spent idle
	Color alphaOne, alphaHalf;  // hold alpha values for colors
	float blinkSpeed;   // how fast do we want to blink
	float blinkStartTime;   // when did the blinking start

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if ( bDamagedRecently )
			Blink();

		if ( bIsActive && !bIsCharging )	// code only runs if the player is inside the trigger collider and the forklift is not actively charging
		{
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
		source = GetComponent<AudioSource>();
		alphaOne = new Color( 1, 1, 1, 1 ); // Opaque alpha value
		alphaHalf = new Color( 1, 1, 1, .5f );  // half transparent alpha value
		blinkSpeed = 10;
		blinkStartTime = Time.time;
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
		source.Play();
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
		if ( bIsFacingLeft )
			transform.localScale = new Vector3( 2, 2, 1 );
		else
			transform.localScale = new Vector3( -2, 2, 1 );
	}

	// Got help with this function from Cameron Asbury. He talked me through how he did it in our Shmup project, I implemented it on my own in this project
	void Blink()
	{
		if ( Time.time < blinkStartTime + blinkTime )   // are we within the timeframe for blinking
			GetComponent<SpriteRenderer>().color = Color.Lerp( alphaHalf, alphaOne, Mathf.PingPong( Time.time * blinkSpeed, 1 ) );  // bounce the alpha between half transparency and opaque
		else
		{
			bDamagedRecently = false;   // reset the damage boolean so we will stop blinking
			GetComponent<SpriteRenderer>().color = alphaOne;    // reset the alpha to be opaque
		}
	}

	void OnCollisionStay2D( Collision2D col )
	{
		if ( col.gameObject.tag == "Player" )
		{
			col.gameObject.GetComponent<IDamageable>().TakeDamage( damage );
		}
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
		}
	}

	public void TakeDamage( int dmg )
	{
		health -= dmg;  // lower health by incoming damage
		blinkStartTime = Time.time;
		bDamagedRecently = true;	// set damaged to true
		if ( health <= 0 )	// if health drops below 0
			Kill();	// object is dead
	}

	public void Kill()
	{
		// start kill animation and audio
		deathClip.Play();
		GetComponent<Animator>().SetTrigger( "ForkliftDestroyed" );
		GetComponent<PolygonCollider2D>().enabled = false;	// disable the collider
		transform.localScale = new Vector3( .5f, .5f );	// explosion animation is too big by default, sets it to be half size
		rb.isKinematic = true;	// makes the rigidbody2D be kinematic so it doesn't fall through the ground
		Destroy( gameObject, 1 );	// destroy object after 1 second delay
	}
}

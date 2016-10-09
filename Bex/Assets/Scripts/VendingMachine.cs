using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour, IKillable, IDamageable {
	public int health;
	public float fireRate;	// how many times per second can this fire
	public float maxFireDistance;   // how close does the player need to be to shoot at them
	public float blinkTime;	// how long does the object blink
	public GameObject player;	// editor hook for player
	public GameObject[] projectile;	// editor hook for projectiles, uses an array so we can choose between multiple types of cans

	// state booleans
	bool bIsActive;
	bool bDamagedRecently;

	// component variables
	AudioSource audioSource;

	float lastTimeFired;    // last time object fired a projectile
	Vector3 offset; // offset of player's center from feet
	Color alphaOne, alphaHalf;	// hold alpha values for colors
	float blinkSpeed;	// how fast do we want to blink
	float blinkStartTime;	// when did the blinking start

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if ( bIsActive )
		{
			Vector3 target = transform.position - ( player.transform.position + offset );	// find the vector from this object to the player + offset
			if ( target.magnitude < maxFireDistance && Time.time - lastTimeFired > 1 / fireRate )	// if this object is within distance, fire the can cannon
			{
				Fire( target );	// call fire function
			}
		}
		if ( bDamagedRecently )	// if we have taken damage recently
		{
			Blink();	// blink the alpha of our sprite
		}
	}

	/// <summary>
	/// Initialize private variables
	/// </summary>
	void Init()
	{
		bIsActive = false;
		bDamagedRecently = false;
		lastTimeFired = float.MinValue;
		offset = new Vector3( 0, 1, 0 );    // player sprite is centered at the bottom of the sprite, offset brings center of player up to chest
		audioSource = GetComponent<AudioSource>();
		alphaOne = new Color( 1, 1, 1, 1 );	// Opaque alpha value
		alphaHalf = new Color( 1, 1, 1, .5f );	// half transparent alpha value
		blinkSpeed = 10;
		blinkStartTime = Time.time;
	}

	void Activate()
	{
		bIsActive = !bIsActive;
	}

	/// <summary>
	/// Fire projectiles down a target vector
	/// </summary>
	/// <param name="target">Vector3 between this object and what you want to fire at</param>
	void Fire( Vector3 target )
	{
		if ( Time.time - lastTimeFired > 1 / fireRate ) // has enough time passed since we last fired that we can shoot again
		{
			lastTimeFired = Time.time;	// update time last fired to current time
			float angle = Mathf.Atan2( target.y, target.x ) * Mathf.Rad2Deg;    // get the angle in degrees from this object's right to the player
			Instantiate( projectile[ Random.Range( 0, projectile.Length ) ], transform.position, Quaternion.Euler( 0, 0, angle ) ); // create the projectile facing down the angle found
			audioSource.Play();
		}
	}

	// Got help with this function from Cameron Asbury. He talked me through how he did it in our Shmup project, I implemented it on my own in this project
	void Blink()
	{
		if ( Time.time < blinkStartTime + blinkTime )	// are we within the timeframe for blinking
			GetComponent<SpriteRenderer>().color = Color.Lerp( alphaHalf, alphaOne, Mathf.PingPong( Time.time * blinkSpeed, 1 ) );	// bounce the alpha between half transparency and opaque
		else
		{
			bDamagedRecently = false;	// reset the damage boolean so we will stop blinking
			GetComponent<SpriteRenderer>().color = alphaOne;	// reset the alpha to be opaque
		}
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )	// if the player is inside our trigger collider
		{
			Activate();	// set the object to active
		}
	}

	void OnTriggerExit2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )	// if the player leaves our trigger collider
			Activate();	// deactivate our object
	}

	public void TakeDamage( int dmg )
	{
		health -= dmg;
		bDamagedRecently = true;
		blinkStartTime = Time.time;
		Blink();

		if ( health <= 0 )
			Kill();
	}

	public void Kill()
	{
		// object has died
		Destroy( gameObject );
	}
}

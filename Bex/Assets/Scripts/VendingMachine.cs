using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour {
	public int health;
	public float fireRate;	// how many times per second can this fire
	public float maxFireDistance;   // how close does the player need to be to shoot at them
	public GameObject player;	// editor hook for player
	public GameObject[] projectile;	// editor hook for projectiles, uses an array so we can choose between multiple types of cans

	// state booleans
	bool bIsActive;

	// component variables
	AudioSource audioSource;

	float lastTimeFired;	// last time object fired a projectile

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if ( bIsActive )
		{
			Vector3 target = transform.position - player.transform.position;	// find the vector from this object to the player
			if ( target.magnitude < maxFireDistance && Time.time - lastTimeFired > 1 / fireRate )	// if this object is within distance, fire the can cannon
			{
				Fire( target );	// call fire function
			}
		}
	}

	/// <summary>
	/// Initialize private variables
	/// </summary>
	void Init()
	{
		bIsActive = false;
		lastTimeFired = float.MinValue;

		audioSource = GetComponent<AudioSource>();
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

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )
		{
			Activate();
		}
	}

	void OnTriggerExit2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )
			Activate();
	}
}

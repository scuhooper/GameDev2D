using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	// variables available to be set in editor
	public GameObject laser;	// links to laser prefab
	public GameObject missile;	// links to missile prefab
	public float rateOfFire;
	public AudioSource upgradeSFX;	// audio source for playing upgrade collected sound
	public Text missileText;	// UI element to display number of missiles
	public int numberOfMissiles;

	AudioSource source;	// audio source for playing game over sound
	float timeLastFired;
	ViscountessGame game;	// main game object
	bool gameOver;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rateOfFire = 5;	// can fire this many times a second
		timeLastFired = float.MinValue;	// makes sure you can fire as soon as the game starts
		game = FindObjectOfType<ViscountessGame>();	// find the game object
		source = GetComponent<AudioSource>();	// get audio component for gameover
		gameOver = false;
		numberOfMissiles = 0;
		rb = GetComponent<Rigidbody2D>();	// returns reference to rigidbody2d component
	}
	
	// Update is called once per frame
	void Update () {
		// run while game isn't paused or lost
		if ( !game.isPaused && !gameOver )
		{
			// swapped from transform.position to moveposition for smoother collisions with the bounds
			// checks for going up and down screen with a and d, respectively
			if ( Input.GetKey( KeyCode.A ) )
			{
				rb.MovePosition( rb.position + new Vector2( 0, .1f ) );
				Debug.Log( name + " is moving up." );
			}
			if ( Input.GetKey( KeyCode.D ) )
			{
				rb.MovePosition( rb.position + new Vector2( 0, -.1f ) );
				Debug.Log( name + " is moving down" );
			}

			// check mouse input for firing weapons
			if ( Input.GetMouseButton( 0 ) )
			{
				Fire();
			}
			if ( Input.GetMouseButton( 1 ) )
			{
				FireMissile();
			}
			missileText.text = "Missiles: " + numberOfMissiles;	// update UI tile with number of missiles
		}
    }

	void Fire()
	{
		// shoot lasers with respect to firerate
		if ( Time.time - timeLastFired > 1 / rateOfFire )
		{
			// instantiate two separate lasers off the end of each weapon on ship
			GameObject LeftLaser = ( GameObject )Instantiate( laser, transform.position + new Vector3( .76f, .254f, 0 ), transform.rotation );	// add vector to position so laser appears at end of cannon
			GameObject RightLaser = ( GameObject )Instantiate( laser, transform.position + new Vector3( .76f, -.254f, 0 ), transform.rotation );
			timeLastFired = Time.time;
		}
	}

	void FireMissile()
	{
		// shoot missiles with firerate as long as player have missiles
		if ( numberOfMissiles > 0 && Time.time - timeLastFired > 1 / rateOfFire )
		{
			GameObject newMissile = ( GameObject )Instantiate( missile, transform.position + new Vector3( 1.17f, 0, 0 ), transform.rotation );	// sets the missile to launch from right in front of player
			timeLastFired = Time.time;
			numberOfMissiles--;
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if ( coll.gameObject.tag == "Enemy" )	// collide with an enemy
		{
			source.Play();	// play game over sound
			Debug.Log( "Player crashed with " + coll.gameObject.name.ToString() );
			game.GameOver();	// call game's gameover function
			gameOver = true;
		}
	}

	// collect powerups. can be made more modular once we have more powerups to have the powerup control what is given to the player
	void OnTriggerEnter2D( Collider2D coll )
	{
		numberOfMissiles += 5;
		Destroy( coll.gameObject );	// destroy collected powerup
		upgradeSFX.Play();
	}
}

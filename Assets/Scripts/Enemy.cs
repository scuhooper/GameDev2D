using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// variables available in editor
	public int health;
	public float speed;
	public int pointsWorth;

	Animator anim;	// animator for explosion
	bool explode;
	AudioSource source;	// audo for explosion sound
	// Use this for initialization
	void Start () {
		health = 100;
		speed = 5;
		pointsWorth = 25;
		anim = GetComponent<Animator>();	// get animator component
		anim.SetBool( "isDestroyed", false );	// set transition constraint to false
		explode = false;
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!explode)	// move until unit is destroyed
			transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
	}

	void TakeDamage( int dmg )
	{
		health -= dmg;
		if ( health <= 0 )	// if unit is killed
		{
			anim.SetBool( "isDestroyed", true );	// start explosion animation by setting transition constraint
			GetComponent<Collider2D>().enabled = false;	// turn off collider so lasers will not be stopped
			explode = true;
			ViscountessGame game = FindObjectOfType<ViscountessGame>();	// get the main game object
			game.SendMessage( "UpdateScore", pointsWorth );	// send point value to update score of the game
			game.currentNumberOfEnemies--;
			source.Play();	// play the destruction sound
			Destroy( gameObject, 1 );	// destroy enemy after 1 second has elapsed
		}
	}
	
	// if the enemy hits the death field
	void OnTriggerEnter2D()
	{
		ViscountessGame game = FindObjectOfType<ViscountessGame>();	// get game object
		game.currentNumberOfEnemies--;
		Destroy( gameObject );	// destroy enemy
	}
}

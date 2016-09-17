using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject laser;
	public GameObject missile;
	public float rateOfFire;
	public AudioSource upgradeSFX;
	public Text missileText;
	public int numberOfMissiles;

	AudioSource source;
	float timeLastFired;
	ViscountessGame game;
	bool gameOver;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rateOfFire = 5;
		timeLastFired = float.MinValue;
		game = FindObjectOfType<ViscountessGame>();
		source = GetComponent<AudioSource>();
		gameOver = false;
		numberOfMissiles = 0;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( !game.isPaused && !gameOver )
		{
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
			if ( Input.GetMouseButton( 0 ) )
			{
				Fire();
			}
			if ( Input.GetMouseButton( 1 ) )
			{
				FireMissile();
			}
			missileText.text = "Missiles: " + numberOfMissiles;
		}
    }

	void Fire()
	{
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
		if ( numberOfMissiles > 0 && Time.time - timeLastFired > 1 / rateOfFire )
		{
			GameObject newMissile = ( GameObject )Instantiate( missile, transform.position + new Vector3( 1.17f, 0, 0 ), transform.rotation );
			timeLastFired = Time.time;
			numberOfMissiles--;
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if ( coll.gameObject.tag == "Enemy" )
		{
			source.Play();
			Debug.Log( "Player crashed with " + coll.gameObject.name.ToString() );
			game.GameOver();
			gameOver = true;
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		numberOfMissiles += 5;
		Destroy( coll.gameObject );
		upgradeSFX.Play();
	}
}

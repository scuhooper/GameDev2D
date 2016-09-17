using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject laser;
	public float rateOfFire;

	AudioSource source;
	float timeLastFired;
	ViscountessGame game;
	bool gameOver;

	// Use this for initialization
	void Start () {
		rateOfFire = 5;
		timeLastFired = float.MinValue;
		game = FindObjectOfType<ViscountessGame>();
		source = GetComponent<AudioSource>();
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ( !game.isPaused && !gameOver )
		{
			if ( Input.GetKey( KeyCode.A ) )
			{
				transform.position += new Vector3( 0, .1f, 0 );
				Debug.Log( name + " is moving up." );
			}
			if ( Input.GetKey( KeyCode.D ) )
			{
				transform.position += new Vector3( 0, -.1f, 0 );
				Debug.Log( name + " is moving down" );
			}
			if ( Input.GetMouseButton( 0 ) )
			{
				Fire();
			}
		}
    }

	void Fire()
	{
		if ( Time.time - timeLastFired > 1 / rateOfFire )
		{
			// instantiate two separate lasers off the end of each weapon on ship
			GameObject LeftLaser = ( GameObject )Instantiate( laser, transform.position + new Vector3( .76f, .254f, 0 ), transform.rotation );
			GameObject RightLaser = ( GameObject )Instantiate( laser, transform.position + new Vector3( .76f, -.254f, 0 ), transform.rotation );
			timeLastFired = Time.time;
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
}

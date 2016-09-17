using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	// variables available to be set in the editor
	public float speed;
	public int lifetime;
	public int damage;

	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;	// set spawn time so we know when to destroy
		speed = 10;
		lifetime = 2;
		damage = 25;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( new Vector3( speed * Time.deltaTime, 0, 0 ) );	// moves the laser right with respect to speed and time since last frame
		if ( Time.time - spawnTime > lifetime )
		{
			Destroy( gameObject );
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		Debug.Log( "Collision with Laser" );
		if ( coll.gameObject.tag == "Enemy" )	// check if we hit an enemy with collider
		{
			coll.gameObject.SendMessage( "TakeDamage", damage );	// damage enemy and call their take damage function
		}
		Destroy( gameObject );	// destroys laser
	}
}

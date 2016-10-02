using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;	// how fast the bullet moves
	public float lifeTime;	// how long until we destroy the bullet if it doesn't collide with any objects
	public int damage;  // how much damage should the bullet do
	public BexPlayer player;

	float timeSpawned;	// holds the spawn time for each bullet

	// Use this for initialization
	void Start () {
		timeSpawned = Time.time;    // initialize spawn time to current time
		player = FindObjectOfType<BexPlayer>();
		if ( !player.IsFacingRight() )	// if character is not facing right, change speed to go in opposite direction
			speed = -speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3( speed * Time.deltaTime, 0 );	// keep moving projectile

		if ( Time.time > timeSpawned + lifeTime )	// if the lifetime has elapsed
			Destroy( gameObject );	// destroy the bullet
	}
}

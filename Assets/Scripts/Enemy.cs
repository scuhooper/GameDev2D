using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health = 100;
	public float speed = 5;
	public int pointsWorth = 25;

	float spawnTime = float.MinValue;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
	}

	void TakeDamage( int dmg )
	{
		health -= dmg;
		if ( health <= 0 )
		{
			Destroy( gameObject );
			ViscountessGame game = FindObjectOfType<ViscountessGame>();
			game.SendMessage( "UpdateScore", pointsWorth );
			game.currentNumberOfEnemies--;
		}
	}

	void OnTriggerEnter2D()
	{
		ViscountessGame game = FindObjectOfType<ViscountessGame>();
		game.currentNumberOfEnemies--;
		Destroy( gameObject );
	}
}

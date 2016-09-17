using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health;
	public float speed;
	public int pointsWorth;

	Animator anim;
	bool explode;
	AudioSource source;
	// Use this for initialization
	void Start () {
		health = 100;
		speed = 5;
		pointsWorth = 25;
		anim = GetComponent<Animator>();
		anim.SetBool( "isDestroyed", false );
		explode = false;
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!explode)
			transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
	}

	void TakeDamage( int dmg )
	{
		health -= dmg;
		if ( health <= 0 )
		{
			anim.SetBool( "isDestroyed", true );
			GetComponent<Collider2D>().enabled = false;
			explode = true;
			ViscountessGame game = FindObjectOfType<ViscountessGame>();
			game.SendMessage( "UpdateScore", pointsWorth );
			game.currentNumberOfEnemies--;
			source.Play();
			Destroy( gameObject, 1 );
		}
	}

	void OnTriggerEnter2D()
	{
		ViscountessGame game = FindObjectOfType<ViscountessGame>();
		game.currentNumberOfEnemies--;
		Destroy( gameObject );
	}
}

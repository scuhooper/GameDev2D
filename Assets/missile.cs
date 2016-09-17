using UnityEngine;
using System.Collections;

public class missile : MonoBehaviour {
	public float speed;
	public int lifetime;
	public int damage;

	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		speed = 10;
		lifetime = 2;
		damage = 100;   // this currently one shot kills all enemies regardless of their health
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( new Vector3( speed * Time.deltaTime, 0, 0 ) );
		if ( Time.time - spawnTime > lifetime )
		{
			Destroy( gameObject );
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		Debug.Log( "Collision with missile" );
		if ( coll.gameObject.tag == "Enemy" )
		{
			coll.gameObject.SendMessage( "TakeDamage", damage );
			Destroy( gameObject );
		}
	}
}

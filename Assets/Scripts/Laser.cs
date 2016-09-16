using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public float speed = 1;
	public int lifetime = 2;
	public int damage = 25;

	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
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
		Debug.Log( "Collision with Laser" );
		if ( coll.gameObject.tag == "Enemy" )
		{
			coll.gameObject.SendMessage( "TakeDamage", damage );
		}
		Destroy( gameObject );
	}
}

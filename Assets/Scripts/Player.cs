using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject laser;
	public float rateOfFire = 5;
	float timeLastFired;

	// Use this for initialization
	void Start () {
		timeLastFired = float.MinValue;
	}
	
	// Update is called once per frame
	void Update () {
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
			Debug.Log( "Player crashed with " + coll.gameObject.name.ToString() );
			Destroy( gameObject );
		}
	}
}

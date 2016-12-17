using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 moveVector = new Vector3( 0, 0, 0 );
		if ( Input.GetKey( KeyCode.W ) )
			moveVector += Vector3.up;
		if ( Input.GetKey( KeyCode.S ) )
			moveVector += Vector3.down;
		if ( Input.GetKey( KeyCode.A ) )
			moveVector += Vector3.left;
		if ( Input.GetKey( KeyCode.D ) )
			moveVector += Vector3.right;

		moveVector.Normalize();
		if ( moveVector != Vector3.zero )
			transform.Translate( moveVector * speed * Time.deltaTime );
	}
}

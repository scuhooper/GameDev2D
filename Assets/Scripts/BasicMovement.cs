using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKey( KeyCode.A ) )
        {
            transform.position += new Vector3( -.1f, 0, 0 );
            Debug.Log( this.name + " is moving left." );
        }
        if ( Input.GetKey( KeyCode.D ) )
        {
            transform.position += new Vector3( .1f, 0, 0 );
            Debug.Log( this.name + " is moving right." );
        }
        if ( Input.GetKey( KeyCode.W ) )
        {
            transform.position += new Vector3( 0, .1f, 0 );
            Debug.Log( this.name + " is moving up." );
        }
        if ( Input.GetKey( KeyCode.S ) )
        {
            transform.position += new Vector3( 0, -.1f, 0 );
            Debug.Log( this.name + " is moving down" );
        }
    }
}

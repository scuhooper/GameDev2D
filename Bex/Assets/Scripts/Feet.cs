using UnityEngine;
using System.Collections;

public class Feet : MonoBehaviour {
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<BexPlayer>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D( Collision2D col )
	{
		if ( col.gameObject.CompareTag( "Ground" ) )
		{
			player.GetComponent<BexPlayer>().SetIsGrounded( true );
		}
	}

	void OnCollisionExit2D( Collision2D col )
	{
		player.GetComponent<BexPlayer>().SetIsGrounded( false );
	}
}

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
		player.GetComponent<BexPlayer>().SetIsGrounded( true );
	}

	void OnCollisionStay2D( Collision2D col )
	{
		if( !player.GetComponent<BexPlayer>().IsGrounded() )
			player.GetComponent<BexPlayer>().SetIsGrounded( true );
	}

	void OnCollisionExit2D( Collision2D col )
	{
		player.GetComponent<BexPlayer>().SetIsGrounded( false );
	}
}

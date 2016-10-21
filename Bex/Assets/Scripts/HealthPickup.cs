using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	public int healAmount;

	BexPlayer player;
	AudioSource source;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<BexPlayer>();
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject == player.gameObject )
		{
			player.RefillHealth( healAmount );
			Destroy( gameObject, 1 );
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;
			source.Play();
		}
	}
}

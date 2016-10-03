using UnityEngine;
using System.Collections;

public class Forklift : MonoBehaviour {
	public int health;
	public int damage;

	bool bIsActive;
	// Use this for initialization
	void Start () {
		bIsActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ( bIsActive)
			Debug.Log( "Forklift is active!" );
	}

	/// <summary>
	/// Just changes the bool of bIsActive
	/// </summary>
	void Activate()
	{
		bIsActive = !bIsActive;
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if (col.gameObject.tag == "Player")
			Activate();
	}

	void OnTriggerExit2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )
		{
			Activate();
			Debug.Log( "Forklift is inactive!" );
		}
	}
}

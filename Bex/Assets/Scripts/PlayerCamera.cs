using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	public GameObject player;
	public int offsetX;
	public int offsetY;

	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		targetPos = new Vector3( player.transform.position.x + offsetX, player.transform.position.y + offsetY, transform.position.z );
		transform.position = targetPos;
	}
	
	// Update is called once per frame
	void Update () {
		// change offset depending on direction player is facing
		if ( player.GetComponent<BexPlayer>().IsFacingRight() == true)
			targetPos = new Vector3( player.transform.position.x + offsetX, player.transform.position.y + offsetY, transform.position.z );
		else
			targetPos = new Vector3( player.transform.position.x - offsetX, player.transform.position.y + offsetY, transform.position.z );

		transform.position = Vector3.Lerp( transform.position, targetPos, .05f );
	}
}

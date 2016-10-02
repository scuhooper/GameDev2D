using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	public GameObject player;

	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		targetPos = new Vector3( player.transform.position.x, player.transform.position.y, transform.position.z );
		transform.position = targetPos;
	}
	
	// Update is called once per frame
	void Update () {		
		targetPos = new Vector3( player.transform.position.x, player.transform.position.y, transform.position.z );
		transform.position = Vector3.Slerp( transform.position, targetPos, .1f );
	}
}

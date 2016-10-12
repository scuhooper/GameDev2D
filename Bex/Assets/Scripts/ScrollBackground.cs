using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
	public float scrollSpeed;

	float bgwidth;
	Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		bgwidth = 132;
	}
	
	// Update is called once per frame
	void Update () {
		float newPos = Mathf.Repeat( Time.time * scrollSpeed, bgwidth );
		transform.position = startPos + new Vector3( -newPos, 0, 0 );
	}
}

using UnityEngine;
using System.Collections;

public class RotateProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate( Vector3.forward, 10 );	// just rotate the can to make it easier to spot on the screen
	}
}

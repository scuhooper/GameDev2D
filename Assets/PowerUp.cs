using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
	}

	void OnTriggerEnter2D()
	{
		Destroy( gameObject );
	}
}

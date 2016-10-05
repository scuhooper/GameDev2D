using UnityEngine;
using System.Collections;

public class Can : MonoBehaviour {
    public float speed;
    public float lifeSpan;

    float timeSpawned;

	// Use this for initialization
	void Start () {
        timeSpawned = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time > timeSpawned + lifeSpan )
            Destroy( gameObject );
        else
            transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
	}
}

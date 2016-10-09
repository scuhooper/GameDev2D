using UnityEngine;
using System.Collections;

public class Can : MonoBehaviour {
    public float speed;
    public float lifeSpan;
	public int damage;

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

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" && col.isTrigger == false )
		{
			col.gameObject.GetComponent<BexPlayer>().TakeDamage( damage );
		}
		Destroy( gameObject );
	}
}

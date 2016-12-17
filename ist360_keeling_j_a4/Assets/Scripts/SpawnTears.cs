using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTears : MonoBehaviour {
	public GameObject tears;

	// Use this for initialization
	void Start () {
		StartCoroutine( MakeTears() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MakeTears()
	{
		while ( true )
		{
			yield return new WaitForSeconds( Random.Range( 1.5f, 3 ) );
			GameObject go = Instantiate( tears, transform.position, transform.rotation );
		}
	}
}

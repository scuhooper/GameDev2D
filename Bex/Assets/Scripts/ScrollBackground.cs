using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
	public float scrollSpeed;
	public GameObject bg;

	float bgwidth;
	Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		bgwidth = 136;
	}
	
	// Update is called once per frame
	void Update () {
		float newPos = Mathf.Repeat( Time.time * scrollSpeed, bgwidth );
		Debug.Log( newPos );
		if ( newPos > 131 )
		{
			StartCoroutine( FadeToBlack() );
		}
		else
			bg.GetComponent<SpriteRenderer>().color = new Color( 0, 0, 0, 0 );

		transform.position = startPos + new Vector3( -newPos, 0, 0 );
	}

	IEnumerator FadeToBlack()
	{
		while ( bg.GetComponent<SpriteRenderer>().color != Color.black )
		{
			bg.GetComponent<SpriteRenderer>().color = Color.Lerp( bg.GetComponent<SpriteRenderer>().color, new Color( 0, 0, 0, 1 ), .05f );
			yield return null;
		}
	}
}

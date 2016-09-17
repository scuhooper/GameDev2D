using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ViscountessGame : MonoBehaviour {
	int maxNumberOfEnemies = 20;
	float timeLastSpawned;
	Quaternion quat;
	Collider2D col;

	public float timeSurvived;
	public int score;
	public int currentNumberOfEnemies = 0;
	public GameObject[] enemy = new GameObject[10];
	public float SpawnRate = 4;
	public GameObject spawnArea;
	public Text scoreText;
	public Text timeText;

	// Use this for initialization
	void Start () {
		timeLastSpawned = float.MinValue;
		quat = new Quaternion( 0, 0, 0, 0 );
		UnityEngine.Random.Range( 0, 1 );
		col = spawnArea.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSurvived += Time.deltaTime;
		if ( currentNumberOfEnemies < maxNumberOfEnemies && Time.time - timeLastSpawned > 1 / SpawnRate )
		{
			SpawnEnemy();
		}

		scoreText.text = "Score: " + score;
		timeText.text = "Time: " + timeSurvived.ToString( "F2" );
	}

	void UpdateScore( int points )
	{
		score += points;
	}

	void SpawnEnemy()
	{
		GameObject newEnemy = (GameObject)Instantiate( enemy[ UnityEngine.Random.Range( 0, 10 ) ],
			new Vector3(UnityEngine.Random.Range( col.bounds.min.x, col.bounds.max.x ),
			UnityEngine.Random.Range( col.bounds.min.y, col.bounds.max.y ), 0),
			new Quaternion( 0, 0, 0, 0 ) );
		currentNumberOfEnemies++;
		timeLastSpawned = Time.time;
	}

	void GameOver()
	{
		float finalTime = timeSurvived;
	}
}

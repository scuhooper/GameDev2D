using UnityEngine;
using System.Collections;
using System;

public class ViscountessGame : MonoBehaviour {
	float timeSurvived;
	int score;
	int maxNumberOfEnemies = 20;
	float timeLastSpawned;
	Quaternion quat;

	public int currentNumberOfEnemies = 0;
	public GameObject enemy;
	public float SpawnRate = 4;
	public GameObject spawnArea;

	// Use this for initialization
	void Start () {
		timeLastSpawned = float.MinValue;
		quat = new Quaternion( 0, 0, 0, 0 );
	}
	
	// Update is called once per frame
	void Update () {
		timeSurvived += Time.deltaTime;
		if ( currentNumberOfEnemies < maxNumberOfEnemies && Time.time - timeLastSpawned > 1 / SpawnRate )
		{
			SpawnEnemy();
		}
	}

	void UpdateScore( int points )
	{
		score += points;
	}

	void SpawnEnemy()
	{
		GameObject newEnemy = (GameObject)Instantiate( enemy, new Vector3(0, 0, 0), new Quaternion( 0, 0, 0, 0 ) );
		currentNumberOfEnemies++;
		timeLastSpawned = Time.time;
	}
}

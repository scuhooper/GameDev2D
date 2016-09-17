using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViscountessGame : MonoBehaviour {
	float timeLastSpawned;
	Quaternion quat;
	Collider2D col;
	float lastPowerUp;
	float nextPowerUp;

	public int maxNumberOfEnemies;
	public bool isPaused;
	public float timeSurvived;
	public float minPowerUpDelay;
	public float maxPowerUpDelay;
	public int score;
	public int currentNumberOfEnemies;
	public GameObject[] enemy = new GameObject[10];
	public float SpawnRate;
	public GameObject spawnArea;
	public Text scoreText;
	public Text timeText;
	public Text finalScoreText;
	public Text finalTimeText;
	public GameObject pauseScreen;
	public GameObject GameOverScreen;
	public GameObject gameRunningScreen;
	public GameObject powerUp;
	public GameObject player;

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if ( !isPaused )
		{
			timeSurvived += Time.deltaTime;
			if ( currentNumberOfEnemies < maxNumberOfEnemies && Time.time - timeLastSpawned > 1 / SpawnRate )
			{
				SpawnEnemy();
			}

			scoreText.text = "Score: " + score;
			timeText.text = "Time: " + timeSurvived.ToString( "F2" );

			if ( Input.GetKey( KeyCode.Escape ) )
			{
				OnPauseGame();
			}

			if ( Time.time > lastPowerUp + nextPowerUp )
			{
				SpawnPowerUp();
			}
		}
	}

	void Init()
	{
		maxNumberOfEnemies = 20;
		currentNumberOfEnemies = 0;
		SpawnRate = 4;
		Time.timeScale = 1;
		timeSurvived = 0;
		timeLastSpawned = float.MinValue;
		col = spawnArea.GetComponent<Collider2D>();
		pauseScreen.SetActive( false );
		GameOverScreen.SetActive( false );
		isPaused = false;
		minPowerUpDelay = 3;
		maxPowerUpDelay = 10;
		lastPowerUp = Time.time;
		nextPowerUp = UnityEngine.Random.Range( minPowerUpDelay, maxPowerUpDelay );
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

	public void GameOver()
	{
		isPaused = true;
		Time.timeScale = 0;
		GameOverScreen.SetActive( true );
		gameRunningScreen.SetActive( false );
		float finalTime = timeSurvived;
		finalScoreText.text = "Final Score: " + score;
		finalTimeText.text = "Time Alive: " + finalTime.ToString( "F2" );
	}

	void OnPauseGame()
	{
		Time.timeScale = 0;
		isPaused = true;
		pauseScreen.SetActive( true );
	}

	public void OnResumeGameClicked()
	{
		pauseScreen.SetActive( false );
		Time.timeScale = 1;
		isPaused = false;
	}

	public void OnExitGameClicked()
	{
		Application.Quit();
	}

	public void OnMainMenuClicked()
	{
		SceneManager.LoadScene( "Main Menu" );
	}

	void SpawnPowerUp()
	{
		GameObject newPowerUp = ( GameObject )Instantiate( powerUp,
			new Vector3( UnityEngine.Random.Range( col.bounds.min.x, col.bounds.max.x ),
			UnityEngine.Random.Range( col.bounds.min.y, col.bounds.max.y ), 0 ),
			new Quaternion( 0, 0, 0, 0 ) );
		nextPowerUp = UnityEngine.Random.Range( minPowerUpDelay, maxPowerUpDelay );
		lastPowerUp = Time.time;
	}
}

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViscountessGame : MonoBehaviour {
	float timeLastSpawned;
	Collider2D col;	// collider of spawn volume bounding box
	float lastPowerUp;
	float nextPowerUp;

	// variables available to be set in the editor
	public int maxNumberOfEnemies;
	public bool isPaused;	// toggle pausing the game
	public float timeSurvived;
	public float minPowerUpDelay;	// shortest amount of time between powerup spawns
	public float maxPowerUpDelay;	// longest amount of time between powerup spawns
	public int score;
	public int currentNumberOfEnemies;
	public GameObject[] enemy = new GameObject[10];	// array to hold all the possible enemy ships
	public float SpawnRate;
	public GameObject spawnArea;	// game object of spawn bounding box
	public Text scoreText;	// UI for score
	public Text timeText;	// UI for time survived
	public Text finalScoreText;	// game over UI score
	public Text finalTimeText;	// game over UI time survived
	public GameObject pauseScreen;	// UI elements of pause screen
	public GameObject GameOverScreen;	// UI elements of game over screen
	public GameObject gameRunningScreen;	// default game UI
	public GameObject powerUp;	// power up prefab
	public GameObject player;	// player in scene

	// Use this for initialization
	void Start () {
		Init();	// moved initialization of variables to a standalone Init function
	}
	
	// Update is called once per frame
	void Update () {
		if ( !isPaused )	// updates game logic while it is not paused
		{
			timeSurvived += Time.deltaTime;	// update the time from when the game began
			// spawn enemies if there are less than the max and we haven't spawned any recently
			if ( currentNumberOfEnemies < maxNumberOfEnemies && Time.time - timeLastSpawned > 1 / SpawnRate )
			{
				SpawnEnemy();
			}

			// update the UI text elements
			scoreText.text = "Score: " + score;
			timeText.text = "Time: " + timeSurvived.ToString( "F2" );

			// pause the game when ESC is pressed
			if ( Input.GetKey( KeyCode.Escape ) )
			{
				OnPauseGame();
			}
			// spawn powerups based on the last time one was spawned and a random range of seconds has elapsed
			if ( Time.time > lastPowerUp + nextPowerUp )
			{
				SpawnPowerUp();
			}
		}
	}

	// setup variables
	void Init()
	{
		maxNumberOfEnemies = 20;
		currentNumberOfEnemies = 0;
		SpawnRate = 4;	// spawn up to 4 enemies per second
		Time.timeScale = 1;	//sets time to run at normal pace. needed because timescale is 0 when paused
		timeSurvived = 0;
		timeLastSpawned = float.MinValue;	// set to minimum value so enemy spawning can occur as soon as the game starts
		col = spawnArea.GetComponent<Collider2D>();	// gets the bounding box for the enemy spawn points
		pauseScreen.SetActive( false );	// hide the pause screen UI
		GameOverScreen.SetActive( false );	// hide the game over screen UI
		isPaused = false;
		minPowerUpDelay = 3;	// minimum time between powerups
		maxPowerUpDelay = 10;	// max time between powerup spawns
		lastPowerUp = Time.time;	//set powerup spawn time to beginning of game
		nextPowerUp = UnityEngine.Random.Range( minPowerUpDelay, maxPowerUpDelay );	// get random float for seconds to wait until spawning powerup
	}

	void UpdateScore( int points )
	{
		score += points;
	}

	void SpawnEnemy()
	{
		// spawn enemy at a random location within the bounding box. also chooses a random enemy ship from the array
		GameObject newEnemy = (GameObject)Instantiate( enemy[ UnityEngine.Random.Range( 0, 10 ) ],
			new Vector3(UnityEngine.Random.Range( col.bounds.min.x, col.bounds.max.x ),
			UnityEngine.Random.Range( col.bounds.min.y, col.bounds.max.y ), 0),
			new Quaternion( 0, 0, 0, 0 ) );
		currentNumberOfEnemies++;
		timeLastSpawned = Time.time;
	}

	public void GameOver()
	{
		isPaused = true;	// bool that is used to stop UI update
		Time.timeScale = 0;	// freezes time. make sure to unfreeze on initialization
		GameOverScreen.SetActive( true );	// show game over summary screen
		gameRunningScreen.SetActive( false );	// hide the normal UI
		float finalTime = timeSurvived;

		// push values to game over screen text boxes
		finalScoreText.text = "Final Score: " + score;
		finalTimeText.text = "Time Alive: " + finalTime.ToString( "F2" );
	}

	void OnPauseGame()
	{
		Time.timeScale = 0;	// freezes time. make sure to unfreeze
		isPaused = true;
		pauseScreen.SetActive( true ); // show pause menu
	}

	public void OnResumeGameClicked()
	{
		pauseScreen.SetActive( false );	// hide pause menu
		Time.timeScale = 1;	// unfreeze time
		isPaused = false;
	}

	// function for exit game button click on pause menu
	public void OnExitGameClicked()
	{
		Application.Quit();
	}

	// function for returning to main menu after game over
	public void OnMainMenuClicked()
	{
		SceneManager.LoadScene( "Main Menu" );
	}

	void SpawnPowerUp()
	{
		// get random location in enemy spawn bounding box to generate powerup
		GameObject newPowerUp = ( GameObject )Instantiate( powerUp,
			new Vector3( UnityEngine.Random.Range( col.bounds.min.x, col.bounds.max.x ),
			UnityEngine.Random.Range( col.bounds.min.y, col.bounds.max.y ), 0 ),
			new Quaternion( 0, 0, 0, 0 ) );
		nextPowerUp = UnityEngine.Random.Range( minPowerUpDelay, maxPowerUpDelay );	// set a random number of seconds for when next powerup should spawn
		lastPowerUp = Time.time;
	}
}

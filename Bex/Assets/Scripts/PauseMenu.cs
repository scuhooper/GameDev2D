using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public bool bIsPaused;
    public GameObject pauseMenu;
    public GameObject controlsList;
	public GameObject gameOverScreen;
	public Text gameOverText;
	public AudioSource victory;
	public AudioSource defeat;

	bool bGameWon;
	bool bGameOver;
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
        {
			if ( !bIsPaused && !bGameOver )
			{
				bIsPaused = true;
				Time.timeScale = 0;
				pauseMenu.SetActive( true );
			}
        }
	}

	void Init()
	{
		bIsPaused = false;
		bGameWon = false;
		bGameOver = false;
		gameOverScreen.SetActive( false );
		pauseMenu.SetActive( false );
		controlsList.SetActive( false );
		Time.timeScale = 1;
	}

    public void OnResumeButtonClicked()
    {
        bIsPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive( false );
    }

    public void OnControlsButtonClicked()
    {
        if ( pauseMenu.activeInHierarchy )
        {
            pauseMenu.SetActive( false );
            controlsList.SetActive( true );
        }
        else
        {
            pauseMenu.SetActive( true );
            controlsList.SetActive( false );
        }
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene( "MainMenu" );
    }

    public void OnExitGameButtonClicked()
    {
        Application.Quit();
    }

	public void OnPlayAgainButtonClicked()
	{
		SceneManager.LoadScene( "LevelOne" );
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )
		{
			// set game win condition
			Time.timeScale = 0;
			bGameWon = true;
			GameOver();
		}
	}

	public void GameOver()
	{
		bGameOver = true;
		if ( bGameWon )
		{
			gameOverText.text = "Victory!!!";
			victory.Play();
		}
		else
		{
			gameOverText.text = "Game Over";
			defeat.Play();
		}

		gameOverScreen.SetActive( true );
	}
}

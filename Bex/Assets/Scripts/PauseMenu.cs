using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public bool bIsPaused;
    public GameObject pauseMenu;
    public GameObject controlsList;

	// Use this for initialization
	void Start () {
        bIsPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
        {
            bIsPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive( true );
        }
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
}

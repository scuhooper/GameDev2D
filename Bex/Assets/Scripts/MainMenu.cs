using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject controlsMenu;
	public GameObject menuButtons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene( "LevelOne" );
	}

	public void OnControlsButtonClicked()
	{
		if ( menuButtons.activeInHierarchy )
		{
			menuButtons.SetActive( false );
			controlsMenu.SetActive( true );
		}
		else
		{
			menuButtons.SetActive( true );
			controlsMenu.SetActive( false );
		}
	}

	public void OnExitButtonClicked()
	{
		Application.Quit();
	}
}

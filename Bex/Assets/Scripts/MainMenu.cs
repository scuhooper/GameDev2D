using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

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

	}

	public void OnExitButtonClicked()
	{
		Application.Quit();
	}
}

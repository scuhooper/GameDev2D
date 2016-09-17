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

	// button click functions
	public void OnPlayGameClicked()
	{
		SceneManager.LoadScene( "Level1" );
	}

	public void OnExitGameClicked()
	{
		Application.Quit();
	}
}

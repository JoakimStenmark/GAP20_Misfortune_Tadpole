using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	public bool gameIsPaused;

	public GameObject PauseMenuUI;

	private void Start()
	{
		gameIsPaused = false;
		
		ToggleMouseVisibility();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseState();
		}
		
		if (Input.GetKeyDown("r"))
		{
			ReloadScene();
		}
	}

	public void TogglePauseState()
	{
		gameIsPaused = !gameIsPaused;
		if (gameIsPaused)
		{
			PauseMenuUI.SetActive(true);
			Time.timeScale = 0f;
			ToggleMouseVisibility();
		}
		else if (!gameIsPaused)
		{
			PauseMenuUI.SetActive(false);
			Time.timeScale = 1f;
			ToggleMouseVisibility();
		}
	}

	private void ToggleMouseVisibility()
	{
		if (!gameIsPaused)
		{
			Cursor.visible = false;
		}
		else if (gameIsPaused)
		{
			Cursor.visible = true;
		}
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
	}
	
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1f;
	}
}

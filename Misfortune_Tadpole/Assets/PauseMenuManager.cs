using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	private bool gameIsPaused;
	private bool mouseIsVisible;

	public GameObject PauseMenuUI;

	private void Start()
	{
		mouseIsVisible = true;
		gameIsPaused = false;
		
		ToggleMouseVisibility();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseState();
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
		if (mouseIsVisible)
		{
			Cursor.visible = false;
			mouseIsVisible = false;
		}
		else
		{
			Cursor.visible = true;
			mouseIsVisible = true;
		}
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

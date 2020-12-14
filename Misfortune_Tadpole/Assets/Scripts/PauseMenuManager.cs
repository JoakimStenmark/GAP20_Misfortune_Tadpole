﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	public bool gameIsPaused;
	public bool gameIsWon;

	public GameObject PauseMenuUI;
	private AudioSource audioSource;
	public AudioClip confirmSound;
	public AudioClip declineSound;

	public GameObject VictoryMenuUI;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.ignoreListenerPause = true;
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
			AudioListener.pause = true;
		}
		else if (!gameIsPaused)
		{
			PauseMenuUI.SetActive(false);
			Time.timeScale = 1f;
			ToggleMouseVisibility();
			AudioListener.pause = false;
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

	public void SetVictoryState()
    {
		gameIsWon = true;	
		Time.timeScale = 0f;
		ToggleMouseVisibility();
		VictoryMenuUI.SetActive(true);
		AudioListener.pause = true;

	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		AudioListener.pause = false;
		Time.timeScale = 1f;
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		AudioListener.pause = false;
		Time.timeScale = 1f;
	}

	public void LoadNextScene()
	{
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			AudioListener.pause = false;
			Time.timeScale = 1f;
		}
		else
		{
			GoToMainMenu();
		}
	}

	public void PlayConfirmSound()
    {
		audioSource.clip = confirmSound;
		audioSource.Play();
    }

	public void PlayDeclineSound()
	{
		audioSource.clip = declineSound;
		audioSource.Play();
	}
}

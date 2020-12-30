using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	public bool gameIsPaused;
	public bool gameIsWon;

	public GameObject OptionsMenuUI;
	public GameObject PauseMenuUI;
	private AudioSource audioSource;
	public AudioClip confirmSound;
	public AudioClip declineSound;

	private String level = "Level: ";
	private TextMeshProUGUI levelText;
	public GameObject VictoryMenuUI;
	private Transform playerTransform;

	public float levelTimer;

	private bool tutorialScreen;
	private GameObject tutorialImage;

	private StarTracker starTracker;

	private void Start()
	{
		starTracker = GameObject.FindWithTag(nameof(StarTracker)).GetComponent<StarTracker>();
		
		levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
		levelText.text = level + SceneManager.GetActiveScene().buildIndex;
		
		playerTransform = FindObjectOfType<PlayerController>().transform;
		
		audioSource = GetComponent<AudioSource>();
		audioSource.ignoreListenerPause = true;
		
		gameIsPaused = false;
		
		ToggleMouseVisibility();
		
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			tutorialImage = GameObject.Find("Instructions Image");
			tutorialScreen = true;
		}
	}

	private void Update()
	{
		if (tutorialScreen && Input.GetKeyDown(KeyCode.Space))
		{
			tutorialImage.gameObject.SetActive(false);
			tutorialScreen = false;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape) && !gameIsWon)
		{
			TogglePauseState();
		}

		levelTimer += Time.deltaTime;
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
			OptionsMenuUI.SetActive(false);
			Time.timeScale = 1f;
			ToggleMouseVisibility();
			AudioListener.pause = false;
		}
	}

	private void ToggleMouseVisibility()
	{
		if (gameIsPaused || gameIsWon)
		{
			Cursor.visible = true;
		}
		else
		{
			Cursor.visible = false;
		}
	}

	public void SetVictoryState()
	{
		gameIsWon = true;
	    VictoryMenuUI.SetActive(true);
		Time.timeScale = 0f;
		ToggleMouseVisibility();
		AudioListener.pause = true;
		LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].cleared = true;
		starTracker.SaveAmountOfStars();
		LevelTracker.instance.SaveHighscore();
	}

	public void GoToMainMenu()
	{
		LevelTracker.instance.levelLoader.LoadLevel(0);
		AudioListener.pause = false;
		AudioController.instance.StopMusic(true);
		Time.timeScale = 1f;
		Checkpoint.checkpoint = false;
	}

	public void ReloadSceneFromCheckpoint()
	{
        if (LevelTracker.instance != null)
        {
			LevelTracker.instance.levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
			AudioListener.pause = false;
			Time.timeScale = 1f;

        }
        else
        {
			Debug.LogError("LevelTracker is Missing");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
			
		
	}

	public void ReloadSceneFromStart()
	{
		Checkpoint.checkpoint = false;
		ReloadSceneFromCheckpoint();
	}

	public void LoadNextScene()
	{
		Checkpoint.checkpoint = false;
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			LevelTracker.instance.levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
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

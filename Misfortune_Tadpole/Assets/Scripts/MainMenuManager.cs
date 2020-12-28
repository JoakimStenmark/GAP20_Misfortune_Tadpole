using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	public AudioClip confirmSound;
	public AudioClip declineSound;
	private AudioSource audioSource;
	public GameObject[] buttons;
	public bool levelUnlockToggle = true;

	private void Start()
    {
	    audioSource = GetComponent<AudioSource>();
	    buttons[0].GetComponent<Button>().interactable = true;
    }

	private void Update()
	{
		//Unlock all levels
		if (Input.GetKeyDown(KeyCode.P) && levelUnlockToggle)
		{
			foreach (LevelInfo level in LevelTracker.instance.levels)
			{
				level.cleared = true;
			}
		}
	}

	public void LoadAvailableLevelButtons()
	{
		for (int i = 0; i < LevelTracker.instance.levels.Length; i ++)
		{
			if (LevelTracker.instance.levels[i].cleared)
			{
				buttons[i + 1].GetComponent<Button>().interactable = true;
			}
		}
	}

    public void ExitGame()
	{
		Application.Quit();
	}

	public void LoadLevel(int levelNum)
	{
		SceneManager.LoadScene(levelNum);
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

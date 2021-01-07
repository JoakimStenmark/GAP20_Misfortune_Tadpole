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
	public Animator transition;
	public float transitionTime = 1f;

	public GameObject[] stars;
	public Sprite bronze;
	public Sprite silver;
	public Sprite gold;
	public Sprite none;
	

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
		for (int i = 0; i < LevelTracker.instance.levels.Length - 1; i ++)
		{
			if (LevelTracker.instance.levels[i].cleared)
			{
				buttons[i + 1].GetComponent<Button>().interactable = true;
			}
		}
		GetLevelScore();
	}

	public void GetLevelScore()
    {
		for (int i = 0; i < LevelTracker.instance.levels.Length; i++)
		{
            switch (LevelTracker.instance.levels[i].currentMedal)
            {
                case Medal.none:
					stars[i].GetComponent<Image>().sprite = none;
					break;
                case Medal.bronze:
					stars[i].GetComponent<Image>().sprite = bronze;
					break;
                case Medal.silver:
					stars[i].GetComponent<Image>().sprite = silver;
					break;
                case Medal.gold:
					stars[i].GetComponent<Image>().sprite = gold;
					break;
                default:
                    break;
            }
        }
		Debug.Log("setting medals");
	}

	public void LoadLevel(int levelIndex)
	{
		LevelTracker.instance.levelLoader.LoadLevel(levelIndex);
		AudioController.instance.StopMusic(true);
	}

	public void ExitGame()
	{
		Application.Quit();
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
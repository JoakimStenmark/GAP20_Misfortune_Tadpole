using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public AudioClip confirmSound;
	public AudioClip declineSound;
	private AudioSource audioSource;

    private void Start()
    {
		audioSource = GetComponent<AudioSource>();
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

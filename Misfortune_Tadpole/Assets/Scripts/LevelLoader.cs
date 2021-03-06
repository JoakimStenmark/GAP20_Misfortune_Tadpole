﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public Animator transition;

	public float transitionTime = 1f;

	public void LoadLevel(int levelNum)
	{
		StartCoroutine(LoadLevelCoroutine(levelNum));
	}

	IEnumerator LoadLevelCoroutine(int levelIndex)
	{
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(transitionTime); 
		SceneManager.LoadScene(levelIndex);
	}
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelInfo
{
    [SerializeField]
    private int levelId;
    public int LevelId { get => levelId;}
    public float timeHighscore;
    public int goalFlowersFound;
    public bool cleared = false;

    public LevelInfo(int levelNumber)
    {       
        timeHighscore = 0;
        goalFlowersFound = 0;
        levelId = levelNumber;
    }

}

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker instance;

    public LevelInfo[] levels;
    public int currentLevel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        ResetAllLevelInfos();
    }

    void ResetAllLevelInfos()
    {
        levels = new LevelInfo[SceneManager.sceneCountInBuildSettings - 1];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = new LevelInfo(i + 1);
        }
    }
    
    public void SetCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }


}

using System;
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
    public int starsCollected;
    public int goalFlowersFound;
    public bool cleared = false;

    public LevelInfo(int levelNumber)
    {       
        starsCollected = 0;
        goalFlowersFound = 0;
        levelId = levelNumber;
    }

}

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker instance;

    public LevelInfo[] levels;
    public int currentLevel;
    public LevelLoader levelLoader;

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

        levelLoader = GetComponent<LevelLoader>();
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

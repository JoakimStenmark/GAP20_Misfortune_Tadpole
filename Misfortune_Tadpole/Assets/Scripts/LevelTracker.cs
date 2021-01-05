using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Medal
{
    none,
    bronze,
    silver,
    gold,
}
[Serializable]
public class LevelInfo
{
    [SerializeField]
    private int levelId;
    public int LevelId { get => levelId;}
    public int starsCollected;
    public int goalFlowersFound;
    public bool cleared = false;
    public Medal currentMedal;

    public LevelInfo(int levelNumber)
    {       
        starsCollected = 0;
        goalFlowersFound = 0;
        levelId = levelNumber;
        currentMedal = Medal.none;
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

        InitializeLevels();
        
        LoadHighscore();

        levelLoader = GetComponent<LevelLoader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            DebugAllLevelInfo();
        }
    }

    private void DebugAllLevelInfo()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Debug.Log("Level" + (i + 1) + " Stars:" + levels[i].starsCollected + " Cleared: " + (levels[i].cleared ? "true" : "false") + "Medal: " + levels[i].currentMedal);
        }
    }

    void InitializeLevels()
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

    public void SaveHighscore()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        string levelKey = "Level stars";
        levelKey += currentLevel.ToString();

        PlayerPrefs.SetInt(levelKey, levels[currentLevel - 1].starsCollected);

        levelKey = "Level clear";
        levelKey += currentLevel.ToString();

        PlayerPrefs.SetInt(levelKey, levels[currentLevel - 1].cleared ? 1 : 0);

        levelKey = "Level Medal";
        levelKey += currentLevel.ToString();

        PlayerPrefs.SetInt(levelKey, (int)levels[currentLevel - 1].currentMedal);
        Debug.Log("Saving scores");


    }

    public void LoadHighscore()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int level = i + 1;

            string levelKey = "Level stars";
            levelKey += level.ToString();

            levels[i].starsCollected = PlayerPrefs.GetInt(levelKey, 0);

            levelKey = "Level clear";
            levelKey += level.ToString();

            levels[i].cleared = PlayerPrefs.GetInt(levelKey, 0) == 1;

            levelKey = "Level Medal";
            levelKey += level.ToString();

            levels[i].currentMedal = (Medal)PlayerPrefs.GetInt(levelKey, 0);
        }
        Debug.Log("Loding scores");
    }
}
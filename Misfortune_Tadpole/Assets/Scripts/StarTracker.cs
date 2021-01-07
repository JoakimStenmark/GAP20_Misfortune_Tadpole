using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarTracker : MonoBehaviour
{
    public StarPickup[] starPickups;
    public PauseMenuManager pauseMenuManager;
    public StarUITracker starUITracker;
    public int starsTaken = 0;
    private AudioPlayer starSound;
    public Medal AchievedMedal { get => achievedMedal; }
    private Medal achievedMedal;


    void Start()
    {
        pauseMenuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuManager>();
        starUITracker = GameObject.FindGameObjectWithTag("StarUITracker").GetComponent<StarUITracker>();
        starPickups = GetComponentsInChildren<StarPickup>();

        starUITracker.UpdateStarUI(starsTaken, starPickups.Length);

        starSound = GetComponent<AudioPlayer>();
    }

    public void AddStar()
    {
        starsTaken++;
        starUITracker.UpdateStarUI(starsTaken, starPickups.Length);
        starSound.PlayRoundRobinSound();
    }

    public void SaveAmountOfStars()
    {
        if (starsTaken > LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].starsCollected)
        {
            LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].starsCollected = starsTaken;
        }
    }

    public void CalculateMedal()
    {

        if (starPickups.Length > 0)
        {
            float stars = starsTaken;
            float score = stars / starPickups.Length;

            Debug.Log(score);
            if (score >= 1)
            {
                SaveMedal(Medal.gold);
            }
            else if (score > 0.5f)
            {
                SaveMedal(Medal.silver);
            }
            else if (score > 0)
            {
                SaveMedal(Medal.bronze);
            }
            else
            {
                SaveMedal(Medal.none);
            }

        }
        else
        {

            SaveMedal(Medal.none);
        }
    }

    void SaveMedal(Medal medal)
    {
        achievedMedal = medal;
        if ((int)medal > (int)LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].currentMedal)
        {
            LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].currentMedal = medal;
        }

    }
}

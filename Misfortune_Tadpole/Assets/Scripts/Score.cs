using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Score : MonoBehaviour
{
    private StarTracker starTracker;
    public float totalStars;
    public float starsCollected;
    public float score;
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    private Image medalRenderer;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI starCounter;


    void Start()
    {
        Init();
        setScore();
    }

    void Init()
    {
        starTracker = GameObject.FindGameObjectWithTag("StarTracker").GetComponent<StarTracker>();
        starsCollected = starTracker.starsTaken;
        totalStars = starTracker.starPickups.Length;
        medalRenderer = GetComponent<Image>();
        
    }

    void setScore()
    {
        starCounter.text = starsCollected + "/" + totalStars;
        if (totalStars > 0)
        {
            score = (starsCollected / totalStars);

            if (score >= 1)
            {
                medalRenderer.sprite = gold;
                winText.text = "Gold medal!";
            }
            else if (score > 0.5f)
            {
                medalRenderer.sprite = silver;
                winText.text = "Silver medal!";

            }
            else if (score > 0)
            {
                medalRenderer.sprite = bronze;
                winText.text = "Bronze medal!";

            }
            else
            {
                medalRenderer.enabled = false;
                winText.text = "Good job!";

            }

        }
        else
        {
            medalRenderer.enabled = false;
            winText.text = "Good job!";
        }
    }

    void SaveMedal()
    {
        //if (starsTaken > LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].starsCollected)
        //{
        //    LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].starsCollected = starsTaken;
        //}
    }

    void ShakeStar()
    {
        //transform.sc
    }
}

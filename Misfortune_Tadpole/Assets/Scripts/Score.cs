using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
    }

    public void setScore()
    {
        starCounter.text = starsCollected + "/" + totalStars;

        //switch check levelinfo score and set image after that
        switch (LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].currentMedal)
        {
            case Medal.none:
                medalRenderer.enabled = false;
                winText.text = "Good job!";
                break;
            case Medal.bronze:
                medalRenderer.sprite = bronze;
                winText.text = "Bronze medal!";

                break;
            case Medal.silver:
                medalRenderer.sprite = silver;
                winText.text = "Silver medal!";

                break;
            case Medal.gold:
                medalRenderer.sprite = gold;
                winText.text = "Gold medal!";

                break;
            default:
                break;
        }


    }

    void SaveMedal(Medal medal)
    {
        if ((int)medal > (int)LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].currentMedal)
        {
            LevelTracker.instance.levels[SceneManager.GetActiveScene().buildIndex - 1].currentMedal = medal;
        }
    }

    void ShakeStar()
    {
        //transform.sc
    }
}

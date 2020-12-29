using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarUITracker : MonoBehaviour
{
    public TextMeshProUGUI starsText;
    public string amountOfStars = "";
    public StarTracker starTracker;

    void Awake()
    {
        starTracker = GameObject.FindGameObjectWithTag("StarTracker").GetComponent<StarTracker>();
        starsText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateStarUI(int currentStars, int maxStars)
    {
        starsText.text = amountOfStars + currentStars + " / " + maxStars; 
    }
}

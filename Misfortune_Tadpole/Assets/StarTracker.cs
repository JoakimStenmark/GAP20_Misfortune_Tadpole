﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTracker : MonoBehaviour
{
    public StarPickup[] starPickups;
    public PauseMenuManager pauseMenuManager;
    public StarUITracker starUITracker;
    public int starsTaken = 0;

    void Start()
    {
        pauseMenuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuManager>();
        starUITracker = GameObject.FindGameObjectWithTag("StarUITracker").GetComponent<StarUITracker>();
        starPickups = GetComponentsInChildren<StarPickup>();

        starUITracker.UpdateStarUI(starsTaken, starPickups.Length);
    }

    public void AddStar()
    {
        starsTaken++;
        starUITracker.UpdateStarUI(starsTaken, starPickups.Length);
    }

    public void SaveAmountOfStars()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private bool tutorialFreeze;
    private bool shown;
    public GameObject text;
    private PauseMenuManager pauseMenuManager;
    void Start()
    {
        pauseMenuManager = GameObject.Find("UI 1").GetComponent<PauseMenuManager>();
        //text = GameObject.Find("TutorialText");
    }
    
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || TouchInput.instance.TouchBegan()) && tutorialFreeze)
        {
            Time.timeScale = 1f;
            text.gameObject.SetActive(false);
            tutorialFreeze = false;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !shown)
        {
            shown = true;
            Time.timeScale = 0;
            text.gameObject.SetActive(true);
            tutorialFreeze = true;
        }
    }
}

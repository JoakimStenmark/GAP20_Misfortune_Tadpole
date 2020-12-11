using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlower : MonoBehaviour
{
    public PauseMenuManager pauseMenuManager;
    public GameObject victoryScreen;

    void Start()
    {
        
    }

    void Update()
    {
        if (pauseMenuManager == null || victoryScreen == null)
        {
            Debug.LogWarning("GoalFlower cannot call victory since references are Missing");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CallVictory();
        }
    }

    void CallVictory()
    {
        pauseMenuManager.TogglePauseState();
        victoryScreen.SetActive(true);
    }



}

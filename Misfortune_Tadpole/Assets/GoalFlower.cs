using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlower : MonoBehaviour
{
    public PauseMenuManager pauseMenuManager;


    void Start()
    {
        
    }

    void Update()
    {
        if (pauseMenuManager == null)
        {
            Debug.LogWarning("GoalFlower cannot call victory since references are Missing");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("CallVictory", 1f);
        }
    }

    void CallVictory()
    {
        pauseMenuManager.TogglePauseState();
    }



}

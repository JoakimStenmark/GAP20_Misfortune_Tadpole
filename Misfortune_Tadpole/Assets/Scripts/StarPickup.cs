using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
    private PauseMenuManager pauseMenuManager;
    private StarTracker starTracker;


    void Start()
    {
        pauseMenuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuManager>();
        starTracker = GetComponentInParent<StarTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            starTracker.AddStar();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlower : MonoBehaviour
{

    public PauseMenuManager pauseMenuManager;
    private Animator anim;
    private int playerEnterHash = Animator.StringToHash("playerEnter");

    private bool victory;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
        if (collision.gameObject.CompareTag("Player") && !victory)
        {
            victory = true;
            anim.SetTrigger(playerEnterHash);
            Invoke("CallVictory", 1f);
        }
    }

    void CallVictory()
    {
        pauseMenuManager.SetVictoryState();
    }
}

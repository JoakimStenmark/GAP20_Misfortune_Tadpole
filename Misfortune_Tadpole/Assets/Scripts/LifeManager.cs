using UnityEngine;
using UnityEngine.SceneManagement;


public class LifeManager : MonoBehaviour
{
    private PauseMenuManager pauseMenuManager;
    private GameObject[] lives;
    private Animator anim;
    private int loseLife = Animator.StringToHash("loseLife");
    private int gainLife = Animator.StringToHash("gainLife");

    public int startingLives = 3;
    public int currentLives;
    public float deathTimer;

    void Start()
    {
        pauseMenuManager = GetComponentInParent<PauseMenuManager>();
        currentLives = startingLives;

        lives = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++) {
            lives[i] = transform.GetChild(i).gameObject;
        }
    }

    public bool LooseLife()
    {
        bool isLiving = true;
        anim = lives[currentLives - 1].gameObject.GetComponent<Animator>();
        anim.SetTrigger(loseLife);
        currentLives--;

        if (currentLives < 1)
        {
            isLiving = false;
            Invoke(nameof(Death), 3f);           
        }

        return isLiving; 

    }

    public void GainLife()
    {
        if (currentLives < startingLives)
        {
            anim = lives[currentLives].gameObject.GetComponent<Animator>();
            anim.SetTrigger(gainLife);
            currentLives++;            
        }
    }

    public void Death()
    {
            pauseMenuManager.ReloadScene();
    }
}

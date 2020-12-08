using UnityEngine;
using UnityEngine.SceneManagement;


public class LifeManager : MonoBehaviour
{
    
    private GameObject[] lives;
    
    public int startingLives = 3;
    public int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = startingLives;

        lives = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++) {
            lives[i] = transform.GetChild(i).gameObject;
        }
    }

    public void LooseLife()
    {
        lives[currentLives - 1].gameObject.SetActive(false);
        currentLives--;
        
        if (currentLives < 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GainLife()
    {
        lives[currentLives].gameObject.SetActive(true);
        currentLives++;

        if (currentLives >= startingLives)
            currentLives = startingLives;

    }
}

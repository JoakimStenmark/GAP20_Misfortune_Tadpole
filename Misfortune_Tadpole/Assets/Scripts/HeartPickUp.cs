using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    public LifeManager lifeManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            lifeManager.GetComponent<LifeManager>().GainLife();
        }
    }
}

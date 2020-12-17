using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    private bool gainedLife;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !gainedLife)
        {
            gainedLife = true;
            other.gameObject.GetComponent<PlayerController>().ChangeLifeAmount(true);
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ChangeLifeAmount(true);
            gameObject.SetActive(false);
        }
    }
}

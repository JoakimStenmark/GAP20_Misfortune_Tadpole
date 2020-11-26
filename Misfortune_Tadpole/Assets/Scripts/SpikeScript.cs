using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public PlayerController playerController;
    public int spikeDmg = -10;
    public float dmgInterval = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.ChangeWaterAmount(spikeDmg, dmgInterval);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.ChangeWaterAmount(spikeDmg, dmgInterval);
        }
    }
}

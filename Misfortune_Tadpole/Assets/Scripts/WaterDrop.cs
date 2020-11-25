using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public int waterToAdd;

    private void Start()
    {
        waterToAdd = Mathf.Abs(waterToAdd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().ChangeWaterAmount(waterToAdd);
        }
    }

}

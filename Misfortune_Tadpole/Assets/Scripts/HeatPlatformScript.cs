using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPlatformScript : MonoBehaviour
{
	public int heatPlatformDamage;
	public float damageInterval = 0.2f;

	public PlayerController playerController;
	private AudioSource heatSound;
	public AudioSource sizzleSound;

	void Start()
    {
		heatSound = GetComponent<AudioSource>();
		
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerController = collision.gameObject.GetComponent<PlayerController>();
			playerController.ChangeWaterAmount(heatPlatformDamage, damageInterval);
            if (heatSound.isPlaying)
            {
				heatSound.Stop();
            }
			heatSound.Play();
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerController = collision.gameObject.GetComponent<PlayerController>();
			playerController.ChangeWaterAmount(heatPlatformDamage, damageInterval);

            if (!sizzleSound.isPlaying)
            {
				sizzleSound.Play();
            }
			sizzleSound.gameObject.transform.position = collision.transform.position;

		}
		
	}

	private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player"))
		{
			sizzleSound.Stop();
		}
	}
}

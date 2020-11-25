using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPlatformScript : MonoBehaviour
{
	public int heatPlatformDamage;
	public float damageInterval = 0.2f;

	public PlayerController playerController;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerController = collision.gameObject.GetComponent<PlayerController>();
			playerController.ChangeWaterAmount(heatPlatformDamage, damageInterval);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerController = collision.gameObject.GetComponent<PlayerController>();
			playerController.ChangeWaterAmount(heatPlatformDamage, damageInterval);
		}
	}
}

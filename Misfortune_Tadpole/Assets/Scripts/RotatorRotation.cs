using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorRotation : MonoBehaviour
{
    [Tooltip("Negative value rotates clockwise, Positive value rotates counter clockwise.")]
    public float rotationSpeed;
    private AudioSource rotatorSound;

    private void Start()
    {
        rotatorSound = GetComponent<AudioSource>();
        rotatorSound.pitch = Random.Range(0.8f, 1.2f) + (Mathf.Abs(rotationSpeed) / 400);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
}


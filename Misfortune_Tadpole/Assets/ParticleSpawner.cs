using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 spawnPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
        Quaternion spawnRotation = new Quaternion(collision.contacts[0].normal.x, collision.contacts[0].normal.x, 0, 0);
        Instantiate(particlePrefab, spawnPosition, spawnRotation);
    }
}

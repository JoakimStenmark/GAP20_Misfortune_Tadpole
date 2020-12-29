using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public bool active; 
    public GameObject particlePrefab;
    private PlatformEffector2D lastEffector;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
        {
            PlatformEffector2D currentEffector = collision.gameObject.GetComponent<PlatformEffector2D>();

            if (currentEffector != null)
            {
                if (lastEffector != currentEffector)
                {
                    lastEffector = currentEffector;
                }
                else
                {
                    return;
                }
            }

            spawnPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            spawnRotation = new Quaternion(collision.contacts[0].normal.x, collision.contacts[0].normal.x, 0, 0);

            Instantiate(particlePrefab, spawnPosition, spawnRotation);
        }
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (active)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector3 newSpawnPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
                spawnRotation = new Quaternion(collision.contacts[0].normal.x, collision.contacts[0].normal.x, 0, 0);
                if (Vector3.Distance(newSpawnPosition, spawnPosition) > 1.5f)
                {
                    spawnPosition = newSpawnPosition;
                    Instantiate(particlePrefab, spawnPosition, spawnRotation);

                }
            }
        }
    }
}

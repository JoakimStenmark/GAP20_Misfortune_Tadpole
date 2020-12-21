using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public bool active; 
    public GameObject particlePrefab;
    private PlatformEffector2D lastEffector;

    void Start()
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

            Vector3 spawnPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            Quaternion spawnRotation = new Quaternion(collision.contacts[0].normal.x, collision.contacts[0].normal.x, 0, 0);

            Instantiate(particlePrefab, spawnPosition, spawnRotation);
        }
       
    }
}

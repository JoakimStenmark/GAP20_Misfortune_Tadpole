using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem particle;
    public bool destroyWhenFinished;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (!particle.isPlaying && destroyWhenFinished)
        {
            Destroy(gameObject);
        }
    }
}

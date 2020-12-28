using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRoundRobinSound()
    {
        if (audioSource.isPlaying)
        {
            //soundPlayer.Stop();
        }
        audioSource.clip = sounds[Random.Range(0, sounds.Length - 1)];
        audioSource.pitch = Random.Range(0.9f, 1.2f);       
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSound : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip confirmSound;
    public AudioClip declineSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void PlayConfirmSound()
    {
        audioSource.clip = confirmSound;
        audioSource.Play();
    }

    public void PlayDeclineSound()
    {
        audioSource.clip = declineSound;
        audioSource.Play();
    }
}

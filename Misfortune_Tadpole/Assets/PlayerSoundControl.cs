using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundControl : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    [Header("Wind Settings")]
    public AudioLowPassFilter windLowPassFilter;
    public float windVolume;
    public AnimationCurve windVolumeCurve;
    public Rigidbody2D playerRb2d;
    private float previousT;
    private float amount;

    private AudioSource soundPlayer;
    public AudioClip[] jumpSounds;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        amount = ConvertVelocityToAmount();
        SetWindVolume(amount);
        windLowPassFilter.cutoffFrequency = Mathf.Lerp(500f, 10000f, amount);
    }

    float ConvertVelocityToAmount()
    {
        float t = Mathf.Clamp(playerRb2d.velocity.magnitude, 0f, 30f) * 0.033f;        
        float newAmount = windVolumeCurve.Evaluate((t + previousT) * 0.5f);       
        previousT = t;
        return newAmount;
    }

    void SetWindVolume(float volumeAmount)
    {
        windVolume = Mathf.Lerp(-80f, -5f, volumeAmount);
        audioMixer.SetFloat("SpeedVolume", windVolume);
    }

    public void PlayJumpSound()
    {
        if (soundPlayer.isPlaying)
        {
            soundPlayer.Stop();
        }
        soundPlayer.clip = jumpSounds[Random.Range(0, 2)];
        soundPlayer.pitch = Random.Range(0.7f, 1.2f);
        soundPlayer.Play();
    }



}

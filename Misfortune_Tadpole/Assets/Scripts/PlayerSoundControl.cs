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
    [Header("RoundRobin Sounds")]
    public AudioClip[] jumpSounds;
    public AudioClip[] landSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] waterPickupSounds;
    public AudioClip[] stuckSounds;
    public AudioClip gainLife;

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

    private void PlayRoundRobinSound(AudioClip[] sounds)
    {
        if (soundPlayer.isPlaying)
        {
            //soundPlayer.Stop();
        }
        soundPlayer.clip = sounds[Random.Range(0, sounds.Length - 1)];
        soundPlayer.pitch = Random.Range(0.9f, 1.2f);
        soundPlayer.volume = 1f;
        soundPlayer.Play();
    }

    public void PlayJumpSound()
    {
        PlayRoundRobinSound(jumpSounds);
    }

    public void PlayHurtSound()
    {
        PlayRoundRobinSound(hurtSounds);
    }

    public void PlayWaterPickupSound()
    {
        PlayRoundRobinSound(waterPickupSounds);
    }

    public void PlayStuckSound()
    {
        PlayRoundRobinSound(stuckSounds);
    }

    public void PlayLandSound()
    {
        if (soundPlayer.isPlaying)
        {
            //soundPlayer.Stop();
        }
        soundPlayer.clip = landSounds[Random.Range(0, landSounds.Length - 1)];
        soundPlayer.pitch = Random.Range(0.9f, 1.2f);
        soundPlayer.volume = Mathf.Clamp(playerRb2d.velocity.magnitude, 0f, 30f) * 0.033f;
        soundPlayer.Play();
    }

    public void PlayGetLifeSound()
    {
        soundPlayer.clip = gainLife;
        soundPlayer.volume = 0.5f;
        soundPlayer.Play();
    }

    public void MutePlayer()
    {
        //audioMixer.SetFloat("PlayerVolume", -80f);
        soundPlayer.mute = true;
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    AudioLowPassFilter lowPassFilter;
    public float windVolume;
    public AnimationCurve windVolumeCurve;
    public Rigidbody2D playerRb2d;
    private float previousT;
    private float amount;
    void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        
    }

    void Update()
    {
        amount = ConvertVelocityToAmount();
        SetWindVolume(amount);
        lowPassFilter.cutoffFrequency = Mathf.Lerp(500f, 10000f, amount);
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
        windVolume = Mathf.Lerp(-80f, 0f, volumeAmount);
        audioMixer.SetFloat("SpeedVolume", windVolume);
        //audioMixer.SetFloat("SpeedVolume", 0f);
    }



}

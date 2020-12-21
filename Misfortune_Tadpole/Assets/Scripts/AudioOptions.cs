using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundSlider;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Lerp(-80, 0, musicSlider.value * 0.01f);
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSoundVolume()
    {
        float volume = Mathf.Lerp(-80, 0, soundSlider.value * 0.01f);

        audioMixer.SetFloat("SoundVolume", volume);
    }
}

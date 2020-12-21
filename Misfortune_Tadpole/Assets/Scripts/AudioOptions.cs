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

    private float musicVolume;
    private float soundVolume;
    void Start()
    {
        if (AudioController.instance != null)
        {
            musicSlider.value = AudioController.instance.musicVolume;
            soundSlider.value = AudioController.instance.soundVolume;
        }
    }

    void Update()
    {
        
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        AudioController.instance.musicVolume = musicVolume;
        audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, musicVolume * 0.01f));
    }
    public void SetSoundVolume()
    {
        soundVolume = soundSlider.value;
        AudioController.instance.soundVolume = soundVolume;
        audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, soundVolume * 0.01f));
    }
}

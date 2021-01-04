using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioMixer audioMixer;
    
    public float musicVolume = 0f;
    public float soundVolume = 0f;
    
    private AudioSource musicPlayer;
    [HideInInspector] public AudioSource MusicPlayer { get => musicPlayer;}
    
    public float musicFadeTime;
    public AudioClip menuMusic;
    private float audioSourceVolume;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (instance == this)
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                musicVolume = PlayerPrefs.GetFloat("musicVolume");
                soundVolume = PlayerPrefs.GetFloat("soundVolume");
            }

            SetMixerVolume();

            musicPlayer = GetComponent<AudioSource>();
            musicPlayer.ignoreListenerPause = true;
            audioSourceVolume = musicPlayer.volume;

            SceneManager.sceneLoaded += OnSceneLoaded;

            PlayMusicClip(menuMusic);
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            PlayMusicClip(menuMusic);
        }
    }
        
    public void PlayMusicClip(AudioClip music)
    {
        if (musicPlayer.isPlaying)
        {
            musicPlayer.Stop();
        }
        musicPlayer.clip = music;
        musicPlayer.volume = audioSourceVolume;
        musicPlayer.Play();

    }

    public void StopMusic(bool allowFade)
    {
        if (allowFade)
        {
            musicPlayer.DOFade(0f, 1f).OnComplete(Stop);
        }
        else
            Stop();

    }

    private void Stop()
    {
        musicPlayer.Stop();
    }

    public void SetMixerVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume) * 20);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            SaveVolumeSettings();
            DOTween.Kill(musicPlayer);
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


}

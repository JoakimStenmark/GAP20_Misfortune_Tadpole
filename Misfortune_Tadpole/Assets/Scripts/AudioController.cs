using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    public float musicVolume = 0f;
    public float soundVolume = 0f;
    
    private AudioSource musicPlayer;
    [HideInInspector] public AudioSource MusicPlayer { get => musicPlayer;}
    
    public float musicFadeTime;
    public AudioClip menuMusic;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.ignoreListenerPause = true;

        SceneManager.sceneLoaded += OnSceneLoaded;

        PlayMusicClip(menuMusic);
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
        musicPlayer.volume = 1f;
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

    private void OnDisable()
    {
        DOTween.Kill(musicPlayer);
    }


}

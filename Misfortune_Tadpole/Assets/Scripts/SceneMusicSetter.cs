using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicSetter : MonoBehaviour
{

    public AudioClip music;
    void Start()
    {
        if (AudioController.instance != null)
        {

            if (AudioController.instance.MusicPlayer.clip != music)
            {
                AudioController.instance.PlayMusicClip(music);
            }

        }
    }

}

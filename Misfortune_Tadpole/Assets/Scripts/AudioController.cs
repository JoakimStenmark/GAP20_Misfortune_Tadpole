using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public float musicVolume = 0f;
    public float soundVolume = 0f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }


}

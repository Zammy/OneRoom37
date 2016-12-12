using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager Instance;

    public AudioSource TalkSound;

    [SerializeField]
    bool Mute;


    void Awake()
    {
        Instance = this;
    }

    public void PlayTalkSound()
    {
        if (Mute)
        {
            return;
        }
        TalkSound.Play();
    }
}

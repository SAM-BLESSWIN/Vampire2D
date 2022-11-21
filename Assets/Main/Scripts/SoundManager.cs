using System;
using UnityEngine;

public enum SoundTypes
{
    BUTTONCLICK,
    LEVELLOAD,
    LEVELLOCKED,
    PLAYERMOVE,
    PLAYERJUMP,
    LEVELCOMPLETED,
    PLAYERDEATH,
    PLAYERHIT,
    COLLECTABLES,
}

[Serializable]
public class Sounds
{
    public SoundTypes soundType;
    public AudioClip audioClip;
}


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private Sounds[] sounds;

    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(SoundTypes soundType)
    {
        if (sfx.isPlaying)
        {
            sfx.Stop();
        }

        AudioClip clip = GetAudioClip(soundType);
        if(clip != null)
        {
            sfx.PlayOneShot(clip);
        }
    }

    public void PlayContinuous(SoundTypes soundType)
    {
        if (sfx.isPlaying) return;

        AudioClip clip = GetAudioClip(soundType);
        if (clip != null)
        {
            sfx.PlayOneShot(clip);
        }
    }

    public AudioClip GetAudioClip(SoundTypes soundType)
    {
        Sounds sound =  Array.Find(sounds, s => s.soundType == soundType);
        if(sound != null)
        {
            return sound.audioClip;
        }
        return null;
    }
}

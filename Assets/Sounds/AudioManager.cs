using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, menuSounds, matchSounds, speakSounds;
    public AudioSource musicSource, menuSource, matchSource, speakingSource;
   // public AudioClip InitialWhistle;
    //public AudioClip LastWhistle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("No se encuentra sonido");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }

    public void PlayMenuSounds(string name)
    {
        Sound s = Array.Find(menuSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No se encuentra sonido");
        }

        else
        {
            menuSource.clip = s.clip;
            menuSource.PlayOneShot(s.clip);
        }

    }

    public void PlayMatchSounds(string name)
    {
        Sound s = Array.Find(matchSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No se encuentra sonido");
        }

        else
        {
            matchSource.clip = s.clip;
            matchSource.PlayOneShot(s.clip);
        }

    }

    public void PlaySpeakSound(string name)
    {
        Sound s = Array.Find(speakSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No se encuentra sonido");
        }

        else
        {
            speakingSource.clip = s.clip;
            speakingSource.Play();
        }
    }


    [Serializable]
    public class Sound
    {
      public string name;
      public AudioClip clip;

    }

   /* public void InitialWhistle()
    {
      PlayMatchSounds("Whistle01");
    }

    public void playSound(AudioClip clip)
    {
        matchSource.PlayOneShot(clip,0.7f);
    }

    public void playWhistle() { playSound(LastWhistle) ; }*/
}

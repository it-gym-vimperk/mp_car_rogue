using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    AudioSource musicSource;

    float volume;
    int lastClip;

    void Start()
    {
        volume = SavePayerStats.musicVolume;
        musicSource = GetComponent<AudioSource>();

        ChooseNewMusic();
    }


    void Update()
    {
        musicSource.volume = volume;

        if (!musicSource.isPlaying)
        {
            ChooseNewMusic();
        }
    }


   void ChooseNewMusic()
   {
        int randomMusic = Random.Range(0, clips.Length);

        if(lastClip != randomMusic)
        {
            musicSource.clip = clips[randomMusic];
            lastClip = randomMusic;

            musicSource.Play();
        }
        else
        {
            ChooseNewMusic();
        }

   }
}

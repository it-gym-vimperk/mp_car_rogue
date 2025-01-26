using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    AudioSource musicSource;

    public static float volume = 0.5f;
    int lastClip;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        ChooseNewMusic();
    }


    void Update()
    {
        Debug.Log(volume);
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

    public void ChangedMusicVol(Slider musicSlider)
    {
        volume = musicSlider.value;
    }
}

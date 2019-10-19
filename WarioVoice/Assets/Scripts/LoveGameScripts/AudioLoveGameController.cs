using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoveGameController : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip lostSound;
    public AudioClip backgroundSong;
    public AudioClip victorySound;
    public AudioSource womenAudioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void playBackgroundMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = backgroundSong;
        _audioSource.Play();
    }

    public float getVictorySoundTime()
    {
        return victorySound.length;
    }

    public float getLostSoundTime()
    {
        return lostSound.length;
    }

    public void playVictorySound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
        womenAudioSource.clip = victorySound;
        womenAudioSource.Play();
    }

    public void playLostSound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.volume = 1;
        womenAudioSource.clip = lostSound;
        womenAudioSource.Play();
    }

}

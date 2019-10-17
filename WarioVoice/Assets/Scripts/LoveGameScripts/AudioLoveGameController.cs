using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoveGameController : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip lostSound;
    public AudioClip backgroundSong;
    public AudioClip victorySound;

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
        _audioSource.clip = victorySound;
        _audioSource.Play();
    }

    public void playLostSound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.volume = 1;
        _audioSource.clip = lostSound;
        _audioSource.Play();
    }

}

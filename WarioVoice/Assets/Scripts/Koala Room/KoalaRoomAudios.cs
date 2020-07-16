using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KoalaRoomAudios : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxAudios;
    [SerializeField] private AudioClip _rainClip;
    [SerializeField] private AudioClip _eatClip;
    [SerializeField] private AudioClip _playClip;

    public void playRainClip()
    {
        _sfxAudios.clip = _rainClip;
        _sfxAudios.Play();
    }

    public void playEatClip()
    {
        _sfxAudios.clip = _eatClip;
        _sfxAudios.Play();
    }

    public void playPlayClip()
    {
        _sfxAudios.clip = _playClip;
        _sfxAudios.Play();
    }
}

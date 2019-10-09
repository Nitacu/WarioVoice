using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    public void play()
    {
        _audioSource.Play();
    }
}

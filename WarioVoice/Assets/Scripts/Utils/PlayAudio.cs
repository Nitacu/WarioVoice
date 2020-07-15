using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'PlayAudio._audioSource' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioSource _audioSource;
#pragma warning restore CS0649 // El campo 'PlayAudio._audioSource' nunca se asigna y siempre tendrá el valor predeterminado null
    [Header("Si quiere que se de play solo")]
    [SerializeField] private float _time = 0;

    private void Start()
    {
        if (_time>0)
        {
            Invoke("play", _time);
        }
    }

    public void play()
    {
        _audioSource.Play();
    }
}

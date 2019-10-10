using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip _hummingClip;
    [SerializeField] AudioClip _wrongClip;


    private AudioSource _source;
    private AudioClip _currentClip;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();   
    }

    public void playWrongSound()
    {
        _currentClip = _wrongClip;
        playClip();
    }

    public void playHumming()
    {
        _currentClip = _hummingClip;
        playClip();        
    }


    private void playClip()
    {
        _source.Pause();
        _source.clip = _currentClip;
        if (!_source.isPlaying)
        {
            _source.Play();
        }
    }
}


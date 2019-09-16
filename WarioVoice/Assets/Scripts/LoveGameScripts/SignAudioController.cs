using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignAudioController : MonoBehaviour
{
    private bool isPlaying = false;
    private AudioSource _audioSource;
    private WordController _wordController;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _wordController = FindObjectOfType<WordController>();
    }

    private void OnMouseDown()
    {
        playAudio(_wordController.signsInGame[_wordController.currentSign]);
    }

    public void playAudio(Sign _sign)
    {
        if (!isPlaying)
        {
            isPlaying = true;
            _audioSource.clip = _sign.pronunciation;
            _audioSource.Play();
            Invoke("clearBool", _sign.pronunciation.length);
        }
    }

    private void clearBool()
    {
        isPlaying = false;
    }
}

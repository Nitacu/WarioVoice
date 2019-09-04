using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBaloon : MonoBehaviour
{
    private AudioSource _audioSourse;

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        _audioSourse.Play();
    }

    private void OnMouseDown()
    {
        _audioSourse.Play();
    }

}

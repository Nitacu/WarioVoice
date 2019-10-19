using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOrquesta : MonoBehaviour
{
    public AudioSource cameraAudioSource;

    public void endAnimationOrquesta()
    {
        FindObjectOfType<TextScreenControl>().startGame();
        cameraAudioSource.Play();
        gameObject.SetActive(false);
    }
}

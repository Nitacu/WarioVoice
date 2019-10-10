using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerControl
{

    private static AudioMixerControl _instance;

    public bool musicOn = true;
    public bool sfxOn = true;

    public static AudioMixerControl GetInstance()
    {
        if (_instance == null)
        {
            _instance = new AudioMixerControl();
        }

        return _instance;
    }

}

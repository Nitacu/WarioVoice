using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextToSpeech : MonoBehaviour
{

    [SerializeField] private AudioSource _audio;
    [SerializeField] private TMP_Text _inputField;
    [SerializeField] private Button _button;

    public void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        _button.gameObject.SetActive(false);
        StartCoroutine(DownloadAudio());
    }

    IEnumerator DownloadAudio()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q="+_inputField.text+"&tl=En-gb";
        WWW www = new WWW(url);
        yield return www;
        _button.gameObject.SetActive(true);
        _audio.clip = www.GetAudioClip(false,true,AudioType.MPEG);
        _audio.Play();
    }
}

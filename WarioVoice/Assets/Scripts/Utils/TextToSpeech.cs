using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextToSpeech : MonoBehaviour
{

    [SerializeField] private AudioSource _audio;
#pragma warning disable CS0649 // El campo 'TextToSpeech._inputField' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _inputField;
#pragma warning restore CS0649 // El campo 'TextToSpeech._inputField' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TextToSpeech._button' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Button _button;
#pragma warning restore CS0649 // El campo 'TextToSpeech._button' nunca se asigna y siempre tendrá el valor predeterminado null

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
#pragma warning disable CS0618 // 'WWW' está obsoleto: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
#pragma warning disable CS0618 // 'WWW' está obsoleto: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
        WWW www = new WWW(url);
#pragma warning restore CS0618 // 'WWW' está obsoleto: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
#pragma warning restore CS0618 // 'WWW' está obsoleto: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
        yield return www;
        _button.gameObject.SetActive(true);
        _audio.clip = www.GetAudioClip(false,true,AudioType.MPEG);
        _audio.Play();
    }
}

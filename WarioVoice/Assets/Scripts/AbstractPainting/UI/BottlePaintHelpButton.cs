using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottlePaintHelpButton : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private TextMeshProUGUI _text;
    public TextMeshProUGUI Text { get => _text; set => _text = value; }

    private AudioClip _audioClip;
    public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }


#pragma warning disable CS0649 // El campo 'BottlePaintHelpButton._bottleColor' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Image _bottleColor;
#pragma warning restore CS0649 // El campo 'BottlePaintHelpButton._bottleColor' nunca se asigna y siempre tendrá el valor predeterminado null
    public Color BottleColor
    {
        get { return _bottleColor.color; }
        set { _bottleColor.color = value; }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = _audioClip;
            GetComponent<AudioSource>().Play();
        }
    }
}

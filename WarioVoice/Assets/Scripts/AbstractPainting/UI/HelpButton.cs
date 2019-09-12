using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HelpButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _sprite;
    public Image Sprite { get => _sprite; set => _sprite = value; }

    [SerializeField] private TextMeshProUGUI _text;
    public TextMeshProUGUI Text { get => _text; set => _text = value; }

    private AudioClip _audioClip;
    public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }


    private void OnEnable()
    {        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<AudioSource>().clip = _audioClip;
        GetComponent<AudioSource>().Play();

    }
}

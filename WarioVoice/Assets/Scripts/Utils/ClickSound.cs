using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour, IPointerDownHandler
{
#pragma warning disable CS0649 // El campo 'ClickSound._sound' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _sound;
#pragma warning restore CS0649 // El campo 'ClickSound._sound' nunca se asigna y siempre tendrá el valor predeterminado null
    private AudioSource _source;
    private Button  _button;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _source.playOnAwake = false;
        _source.clip = _sound;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_source.isPlaying && _button.IsInteractable())
        {
            _source.Play();
        }

    }
}

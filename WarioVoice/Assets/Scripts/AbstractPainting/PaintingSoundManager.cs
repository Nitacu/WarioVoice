using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSoundManager : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'PaintingSoundManager._hummingClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _hummingClip;
#pragma warning restore CS0649 // El campo 'PaintingSoundManager._hummingClip' nunca se asigna y siempre tendrá el valor predeterminado null
    public AudioClip HummingClip
    {
        get { return _hummingClip; }
    }
#pragma warning disable CS0649 // El campo 'PaintingSoundManager._wrongClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _wrongClip;
#pragma warning restore CS0649 // El campo 'PaintingSoundManager._wrongClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PaintingSoundManager._goodPaint' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _goodPaint;
#pragma warning restore CS0649 // El campo 'PaintingSoundManager._goodPaint' nunca se asigna y siempre tendrá el valor predeterminado null
    public AudioClip GoodPaint
    {
        get { return _goodPaint; }
    }
#pragma warning disable CS0649 // El campo 'PaintingSoundManager._badPaint' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _badPaint;
#pragma warning restore CS0649 // El campo 'PaintingSoundManager._badPaint' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PaintingSoundManager._greatTada' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _greatTada;
#pragma warning restore CS0649 // El campo 'PaintingSoundManager._greatTada' nunca se asigna y siempre tendrá el valor predeterminado null


    private AudioSource _source;
    private AudioClip _currentClip;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();   
    }
    public void playGoodJob(bool success)
    {
        _currentClip = (success) ? _goodPaint : _badPaint;        
        playClip();
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

    public void playGreatTada()
    {
        _currentClip = _greatTada;
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


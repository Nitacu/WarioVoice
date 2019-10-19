using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip _hummingClip;
    public AudioClip HummingClip
    {
        get { return _hummingClip; }
    }
    [SerializeField] AudioClip _wrongClip;
    [SerializeField] AudioClip _goodPaint;
    public AudioClip GoodPaint
    {
        get { return _goodPaint; }
    }
    [SerializeField] AudioClip _badPaint;
    [SerializeField] AudioClip _greatTada;


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


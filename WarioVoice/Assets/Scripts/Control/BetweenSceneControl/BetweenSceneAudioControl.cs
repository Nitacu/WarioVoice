using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenSceneAudioControl : MonoBehaviour
{
    private AudioSource _source;

    [SerializeField] AudioClip _goClip;
    [SerializeField] AudioClip _gameOverClip;

    [SerializeField] float _normalVelocity = 1;
    [SerializeField] float _fasterVelocity = 2;
    private bool _goLaunched = false;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();
        setToFasterVelocity();
        if (GameManager.GetInstance().GameCompleted)
        {
            _source.volume = 0;
        }
    }

    public void playGO()
    {
        if (_goLaunched)
        {
            return;
        }
        
        _source.Pause();
        _source.clip = _goClip;
        if (!_source.isPlaying)
        {
            _source.Play();
        }        
        setToNormalVelocity();
        _goLaunched = true;
    }

    public void setToNormalVelocity()
    {
        GetComponent<AudioSource>().pitch = _normalVelocity;
    }

    public void setToFasterVelocity()
    {
        GetComponent<AudioSource>().pitch = _fasterVelocity;
    }

    public void playGameOver()
    {
        if (_goLaunched)
        {
            return;
        }

        _source.Pause();
        _source.clip = _gameOverClip;
        if (!_source.isPlaying)
        {
            _source.Play();
        }
        setToNormalVelocity();
        _goLaunched = true;
    }
}

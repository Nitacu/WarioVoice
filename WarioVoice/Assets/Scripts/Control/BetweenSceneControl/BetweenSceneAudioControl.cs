using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenSceneAudioControl : MonoBehaviour
{
    private AudioSource _source;

#pragma warning disable CS0649 // El campo 'BetweenSceneAudioControl._goClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _goClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneAudioControl._goClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneAudioControl._gameOverClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _gameOverClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneAudioControl._gameOverClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneAudioControl._greatAudioClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _greatAudioClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneAudioControl._greatAudioClip' nunca se asigna y siempre tendrá el valor predeterminado null

    [SerializeField] float _normalVelocity = 1;
    [SerializeField] float _fasterVelocity = 2;
    private bool _goLaunched = false;

    private void OnEnable()
    {
        Debug.Log("Between SceneAudioControl OnEnable() start");

        _source = GetComponent<AudioSource>();
        setToFasterVelocity();
        if (GameManager.GetInstance().GameCompleted)
        {
            _source.volume = 0;
        }
        Debug.Log("Between SceneAudioControl OnEnable() end");
    }

    public void playGreat()
    {
        if (_goLaunched)
        {
            return;
        }

        _source.Pause();
        _source.clip = _greatAudioClip;
        if (!_source.isPlaying)
        {
            _source.Play();
        }
        setToNormalVelocity();
        _goLaunched = true;
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

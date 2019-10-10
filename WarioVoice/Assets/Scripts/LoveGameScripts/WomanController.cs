using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : MonoBehaviour
{
    private const string IDLEANIMATION = "Idle";
    private const string LOVEANIMATION = "Loved";
    private const string WTFANIMATION = "Wtf";

    public AudioClip awwSFX;
    public AudioClip whatSFX;
    private AudioSource audioSource;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void playIdle()
    {
        _animator.Play(Animator.StringToHash(IDLEANIMATION));
    }

    public void playLoveAnimation()
    {
        _animator.Play(Animator.StringToHash(LOVEANIMATION), -1, 0f);
        
        Invoke("playIdle", GetAnimationClip(LOVEANIMATION).length);
    }

    public void playWTFAnimation(int cont)
    {
        _animator.Play(Animator.StringToHash(WTFANIMATION), -1, 0f);
        
        if (cont != 3)
        {
            Invoke("playIdle", GetAnimationClip(WTFANIMATION).length);
        }
    }

    public void playGoodSoundEffect()
    {
        audioSource.clip = awwSFX;
        audioSource.Play();
    }

    public void playBadSoundEffect()
    {
        audioSource.clip = whatSFX;
        audioSource.Play();
    }
    

    private AnimationClip GetAnimationClip(string name)
    {
        if (!_animator) return null; // no animator

        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null; // no clip by that name
    }
}

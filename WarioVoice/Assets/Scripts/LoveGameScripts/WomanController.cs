using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : MonoBehaviour
{
    private const string IDLEANIMATION = "Idle";
    private const string LOVEANIMATION = "Loved";
    private const string WTFANIMATION = "Wtf";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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

    public void playWTFAnimation()
    {
        _animator.Play(Animator.StringToHash(WTFANIMATION), -1, 0f);
        Invoke("playIdle", GetAnimationClip(WTFANIMATION).length);
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

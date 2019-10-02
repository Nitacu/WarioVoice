using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{

    public List<Transform> positionList = new List<Transform>();
    public List<AnimationClip> clips = new List<AnimationClip>();
    private int random = 0;
    private Animator _animator;
    [HideInInspector]
    public AnimationClip previousClip;
    public AnimationClip TaxiOut;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void playTaxiOut()
    {
        _animator.Play(Animator.StringToHash(TaxiOut.name), -1, 0f);
    }

    public void setPosition()
    {
        random = Random.Range(0, positionList.Count);
        transform.position = positionList[random].position;
    }

    public void playAnimation()
    {
        random = Random.Range(0, clips.Count);
        if(previousClip != clips[random])
        {
            previousClip = clips[random];
            _animator.Play(Animator.StringToHash(clips[random].name), -1, 0f);
        }
        else
        {
            playAnimation();
        }
    }

    public float getCurrentClipTime()
    {
        return previousClip.length;
    }
}

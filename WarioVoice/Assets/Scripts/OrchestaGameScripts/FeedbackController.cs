using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    public AnimationClip questionAnswer;
    public AnimationClip wrongAnswer;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void playQuestion()
    {
        _anim.Play(Animator.StringToHash(questionAnswer.name), -1, 0f);
    }

    public void playWrong()
    {
        _anim.Play(Animator.StringToHash(wrongAnswer.name), -1, 0f);
    }

    public float getWrongLength()
    {
        return wrongAnswer.length;
    }
}

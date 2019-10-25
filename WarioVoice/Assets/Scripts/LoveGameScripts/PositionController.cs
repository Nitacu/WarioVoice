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
    public GameObject trashCan;
    public GameObject cake;
    public GameObject blueHouse;
    public GameObject street;
    public AudioClip carEngine;
    public AudioSource sfxAudioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void playIdleFriend()
    {
        _animator.Play(Animator.StringToHash("IdleFriend"), -1, 0f);
    }

    public void playTaxiOut()
    {
        _animator.Play(Animator.StringToHash(TaxiOut.name), -1, 0f);
       // sfxAudioSource.clip = carEngine;
       // sfxAudioSource.Play();
    }

    public void playTrashCanOut()
    {
        playIdleFriend();
        trashCan.SetActive(true);
    }

    public void playCakeOut()
    {
        playIdleFriend();
        cake.SetActive(true);
    }

    public void playSewerOut()
    {
        playIdleFriend();
        street.SetActive(true);
    }

    public void playHouseOut()
    {
        playIdleFriend();
        blueHouse.SetActive(true);
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
            clipOptions();

            _animator.Play(Animator.StringToHash(clips[random].name), -1, 0f);
        }
        else
        {
            playAnimation();
        }
    }

    private void clipOptions()
    {
        if (clips[random].name == "TrashCan")
        {
            trashCan.SetActive(false);
        }
        if (clips[random].name == "Cake")
        {
            cake.SetActive(false);
        }
        if (clips[random].name == "DoorHouse")
        {
            blueHouse.SetActive(false);
        }
        if (clips[random].name == "Sewer")
        {
            street.SetActive(false);
        }
    }

    public float getCurrentClipTime()
    {
        return previousClip.length;
    }
}

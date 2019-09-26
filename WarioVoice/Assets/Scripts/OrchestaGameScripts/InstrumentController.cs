﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour
{
    
    public Instrument instrumentObject;
    public int numberspawn;
    private SpriteRenderer spriteRenderer;
    private Sprite instrumentQuiet;
    private AudioSource _audio;
    private AudioClip _instrumentSound;
    private AudioClip _instrumentNameSound;
    private AnimationClip clipAnimation;
    private Animator _anim;

    public bool isOn = false;

    public enum ENUMINSTRUMENT
    {
        VIOLIN,
        DRUMS,
        PIANO,
        HARMONICA,
        SAXOPHONE,
        TUBA,
        TRUMPET,
        HARP,
        GUITAR,
        FLUTE,
        MARIMBA,
        MARACAS,
        CELLO,
        CLARINET,
        ACCORDION,
        TRIANGLE,
        TAMBOURINE,
        CYMBALS,
        XYLOPHONE
    }

    [HideInInspector]
    public ENUMINSTRUMENT _instrument;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        instrumentObject = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        //setInstrument(instrumentObject);
        //setMemberPlaying();
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        setInstrument();
        
    }

    public void setInstrument()
    {
        if (instrumentObject != null)
        {
            _instrument = instrumentObject.instrument;
            instrumentQuiet = instrumentObject.sprite;
            _instrumentNameSound = instrumentObject.clipName;
            _instrumentSound = instrumentObject.clipSound;
            clipAnimation = instrumentObject.clipAnimation;
        }
    }

    public void setMemberPlaying()
    {    
        spriteRenderer.sprite = instrumentQuiet;
    }


    public void playSound()
    {
        GetComponent<AudioSource>().clip = _instrumentSound;
        GetComponent<AudioSource>().Play();
    }

    public void playName()
    {
        GetComponent<AudioSource>().clip = _instrumentNameSound;
        GetComponent<AudioSource>().Play();
    }

    public void setQuietInstrument()
    {
        spriteRenderer.sprite = instrumentQuiet;
        isOn = true;
    }

    public void changeInstrument(bool boolean)
    {
        isOn = boolean;
        if (boolean)
        {
            playClip();
        }
    }

    public float getSoundTime()
    {
        return _instrumentSound.length;
    }

    public float getNameTime()
    {
        return _instrumentNameSound.length;
    }

    public void playClip()
    {
        _anim.Play(Animator.StringToHash(clipAnimation.name.ToString()), -1, 0f);
        Invoke("setIdle", _instrumentSound.length);
    }

    private void setIdle()
    {
        _anim.Play(Animator.StringToHash("Idle"),-1,0f);
        GetComponent<MusicParticles>().stopParticles();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }

    }
}

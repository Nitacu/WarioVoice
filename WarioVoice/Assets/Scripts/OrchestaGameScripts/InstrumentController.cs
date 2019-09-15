using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour
{
    [HideInInspector]
    public Instrument instrumentObject;
    public int numberspawn;
    private SpriteRenderer spriteRenderer;
    private Sprite instrumentQuiet;
    private Sprite directorPlaying;
    private Sprite memberPlaying;
    private AudioSource _audio;
    private AudioClip _instrumentSound;
    private AudioClip _instrumentNameSound;

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
        HARP
    }

    [HideInInspector]
    public ENUMINSTRUMENT _instrument;

    // Start is called before the first frame update
    void Start()
    {
        instrumentObject = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        //setInstrument(instrumentObject);
        //setMemberPlaying();
    }

    public void setInstrument(Instrument instrument)
    {
        _instrument = instrument.instrument;
        instrumentQuiet = instrument.sprite;
        directorPlaying = instrument.directorPlaying;
        memberPlaying = instrument.memberPlaying;
        _instrumentNameSound = instrument.clipName;
        _instrumentSound = instrument.clipSound;
    }

    public void setMemberPlaying()
    {
        spriteRenderer.sprite = memberPlaying;
    }

    public void setDirectorPlaying()
    {
        spriteRenderer.sprite = directorPlaying;
    }

    public void playSound()
    {
        _audio.clip = _instrumentSound;
        _audio.Play();
    }

    public void playName()
    {
        _audio.clip = _instrumentNameSound;
        _audio.Play();
    }

    public void setQuietInstrument()
    {
        spriteRenderer.sprite = instrumentQuiet;
        isOn = true;
    }

    public void changeInstrument(bool boolean, Instrument _newInstrument)
    {
        isOn = boolean;
    }

    public float getSoundTime()
    {
        return _instrumentSound.length;
    }

    public float getNameTime()
    {
        return _instrumentNameSound.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1);
        }

    }
}

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

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        instrumentObject = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        //setInstrument(instrumentObject);
        //setMemberPlaying();
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        setInstrument();
    }

    public void setInstrument()
    {
        _instrument = instrumentObject.instrument;
        instrumentQuiet = instrumentObject.sprite;
        directorPlaying = instrumentObject.directorPlaying;
        memberPlaying = instrumentObject.memberPlaying;
        _instrumentNameSound = instrumentObject.clipName;
        _instrumentSound = instrumentObject.clipSound;
    }

    public void setMemberPlaying()
    {
        /*if (memberPlaying == null)
        {
            Debug.Log("No tengo sprite de member playing");
        }
        if (spriteRenderer == null)
        {
            Debug.Log("No tengo sprite renderer wey");
        }*/
      
        spriteRenderer.sprite = memberPlaying;
    }

    public void setDirectorPlaying()
    {
        spriteRenderer.sprite = directorPlaying;
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
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }

    }
}

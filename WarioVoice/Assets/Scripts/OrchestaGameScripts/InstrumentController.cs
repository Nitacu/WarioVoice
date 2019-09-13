using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour
{

    public Instrument instrumentObject;

    private SpriteRenderer spriteRenderer;
    private Sprite instrumentQuiet;
    private Sprite directorPlaying;
    private Sprite memberPlaying;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        setInstrument(instrumentObject);
        setMemberPlaying();
    }

    public void setInstrument(Instrument instrument)
    {
        _instrument = instrument.instrument;
        instrumentQuiet = instrument.sprite;
        directorPlaying = instrument.directorPlaying;
        memberPlaying = instrument.memberPlaying;
    }

    public void setMemberPlaying()
    {
        spriteRenderer.sprite = memberPlaying;
    }

    public void setDirectorPlaying()
    {
        spriteRenderer.sprite = directorPlaying;
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

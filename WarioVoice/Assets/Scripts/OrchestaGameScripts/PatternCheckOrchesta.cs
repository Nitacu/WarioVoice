using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternCheckOrchesta : CommandParser
{
    private PatternController patternControl;
    private InstrumentController.ENUMINSTRUMENT _enumInstrument;
    private bool instrumentWord = false;
    public bool canTalk = true;
    

    // Start is called before the first frame update
    void Start()
    {
        patternControl = GetComponent<PatternController>();
    }

    public override void parseCommand(string command)
    {
        
       getInstrument(command);

    }

    public void getInstrument(string _instrument)
    {
        instrumentWord = false;

        foreach (Transform musicInstrument in patternControl.instrumentsGameObject.transform)
        {
            if (_instrument.Equals(musicInstrument.gameObject.GetComponent<InstrumentController>()._instrument.ToString(), System.StringComparison.OrdinalIgnoreCase))
            {
                instrumentWord = true;
            }
        }

        getEnum(_instrument);


        patternControl.checkInstrument(_enumInstrument, instrumentWord);
    }

    private void getEnum(string _instrument)
    {
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.TUBA.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TUBA;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.HARMONICA.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARMONICA;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.DRUMS.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.DRUMS;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.HARP.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARP;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.PIANO.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.PIANO;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.SAXOPHONE.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.SAXOPHONE;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.TRUMPET.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TRUMPET;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.VIOLIN.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.VIOLIN;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.GUITAR.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.GUITAR;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.FLUTE.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.FLUTE;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.MARIMBA.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.MARIMBA;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.MARACAS.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.MARACAS;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.CELLO.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CELLO;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.CLARINET.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CLARINET;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.ACCORDION.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.ACCORDION;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.TRIANGLE.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TRIANGLE;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.TAMBOURINE.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TAMBOURINE;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.CYMBALS.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CYMBALS;
        }
        if (_instrument.Equals(InstrumentController.ENUMINSTRUMENT.XYLOPHONE.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.XYLOPHONE;
        }


    }
}

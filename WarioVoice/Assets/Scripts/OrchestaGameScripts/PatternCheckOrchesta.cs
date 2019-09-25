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

        foreach (Instrument musicInstrument in patternControl.instrumentsList)
        {
            if (_instrument.Equals(musicInstrument.instrument.ToString(), System.StringComparison.OrdinalIgnoreCase))
            {
                instrumentWord = true;
            }
        }

        getEnum(_instrument);


        patternControl.checkInstrument(_enumInstrument, instrumentWord);
    }

    private void getEnum(string _instrument)
    {
        if (_instrument.Equals("TUBA", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TUBA;
        }
        if (_instrument.Equals("HARMONICA", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARMONICA;
        }
        if (_instrument.Equals("DRUMS", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.DRUMS;
        }
        if (_instrument.Equals("HARP", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARP;
        }
        if (_instrument.Equals("PIANO", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.PIANO;
        }
        if (_instrument.Equals("SAXOPHONE", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.SAXOPHONE;
        }
        if (_instrument.Equals("TRUMPET", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TRUMPET;
        }
        if (_instrument.Equals("VIOLIN", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.VIOLIN;
        }
        if (_instrument.Equals("GUITAR", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.GUITAR;
        }
        if (_instrument.Equals("FLUTE", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.FLUTE;
        }
        if (_instrument.Equals("MARIMBA", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.MARIMBA;
        }
        if (_instrument.Equals("MARACAS", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.MARACAS;
        }
        if (_instrument.Equals("CELLO", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CELLO;
        }
        if (_instrument.Equals("CLARINET", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CLARINET;
        }
        if (_instrument.Equals("ACCORDION", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.ACCORDION;
        }
        if (_instrument.Equals("TRIANGLE", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TRIANGLE;
        }
        if (_instrument.Equals("TAMBOURINE", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.TAMBOURINE;
        }
        if (_instrument.Equals("CYMBALS", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.CYMBALS;
        }
        if (_instrument.Equals("XYLOPHONE", System.StringComparison.OrdinalIgnoreCase))
        {
            _enumInstrument = InstrumentController.ENUMINSTRUMENT.XYLOPHONE;
        }
    }
}

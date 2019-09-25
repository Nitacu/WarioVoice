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


    }
}

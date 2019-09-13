using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCheckOrchesta : CommandParser
{
    private PatternController patternControl;
    private InstrumentController.ENUMINSTRUMENT _enumInstrument;
    private bool instrumentWord = false;

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
            if ( _instrument == musicInstrument.gameObject.GetComponent<InstrumentController>()._instrument.ToString())
            {
                instrumentWord = true;
            }
        }



        switch (_instrument)
        {
            case "TUBA":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.TUBA;
                break;
            case "HARMONICA":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARMONICA;
                break;
            case "DRUMS":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.DRUMS;
                break;
            case "HARP":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.HARP;
                break;
            case "PIANO":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.PIANO;
                break;
            case "SAXOPHONE":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.SAXOPHONE;
                break;
            case "TRUMPET":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.TRUMPET;
                break;
            case "VIOLIN":
                _enumInstrument = InstrumentController.ENUMINSTRUMENT.VIOLIN;
                break;
        }

        patternControl.checkInstrument(_enumInstrument, instrumentWord);
    }
}

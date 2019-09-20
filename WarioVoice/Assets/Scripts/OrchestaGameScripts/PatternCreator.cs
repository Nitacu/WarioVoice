using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCreator : MonoBehaviour
{
    
    private int instrumentNumber;

    Instrument lastInstrument = null;



    public Instrument[] patternCreatorCrystal(int numberOfCrystals, int patternDuration, List<Instrument> instruments)
    {

        Instrument[] pattern = new Instrument[patternDuration];
        for (int i = 0; i < patternDuration; i++)
        {  
            pattern[i] = getRandomInstrument(numberOfCrystals, instruments);
        }

        return pattern;
    }

    private Instrument getRandomInstrument(int numberOfCrystals, List<Instrument> instruments)
    {

        instrumentNumber = Random.Range(0, instruments.Count);

        if(instruments[instrumentNumber] == lastInstrument)
        {
            
            return getRandomInstrument(numberOfCrystals, instruments);

        }
        else {
            lastInstrument = instruments[instrumentNumber];
            return instruments[instrumentNumber];        
        }

        
        
    }

}

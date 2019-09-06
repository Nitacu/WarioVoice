﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronsScript : MonoBehaviour
{
    CrystalController.Colors crystalColor;
    private int crystalNumber;

    

    public CrystalController.Colors[] patternCreator(int numberOfCrystals, int patternDuration, List<CrystalController.Colors> crystals)
    {
        
        CrystalController.Colors[] pattern = new CrystalController.Colors[patternDuration];
        for(int i = 0; i < patternDuration; i++)
        {
            pattern[i] = getRandomColor(numberOfCrystals, crystals);
        }

        return pattern;
    }

    private CrystalController.Colors getRandomColor(int numberOfCrystals, List<CrystalController.Colors> crystals)
    {
        
        crystalNumber = Random.Range(0, crystals.Count);

        return crystals[crystalNumber];
    }

}

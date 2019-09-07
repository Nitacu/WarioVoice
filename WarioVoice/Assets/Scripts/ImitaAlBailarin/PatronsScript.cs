using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronsScript : MonoBehaviour
{
    CrystalController.Colors crystalColor;
    private int crystalNumber;

    Crystal crystalsInGame;

   

    public Crystal[] patternCreatorCrystal(int numberOfCrystals, int patternDuration, List<Crystal> crystals)
    {

        Crystal[] pattern = new Crystal[patternDuration];
        for (int i = 0; i < patternDuration; i++)
        {
            pattern[i] = getRandomCrystal(numberOfCrystals, crystals);
        }

        return pattern;
    }

    private Crystal getRandomCrystal(int numberOfCrystals, List<Crystal> crystals)
    {

        crystalNumber = Random.Range(0, crystals.Count);

        return crystals[crystalNumber];
    }

}

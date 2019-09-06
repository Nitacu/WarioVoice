using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharItem : BuildPairItem
{
    private void Start()
    {
        switch (_type)
        {
            case BuildingVocabulary.PairType.POLICE:
                _recognitionName = BuildingVocabulary.POLICEMAN;
                break;
            case BuildingVocabulary.PairType.STUDENT:
                _recognitionName = BuildingVocabulary.STUDENT;
                break;
            case BuildingVocabulary.PairType.DOCTOR:
                _recognitionName = BuildingVocabulary.DOCTOR;
                break;
            case BuildingVocabulary.PairType.CLOWN:
                _recognitionName = BuildingVocabulary.CLOWN;

                break;
            default:
                break;
        }
    }
}

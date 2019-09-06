using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : BuildPairItem
{
    private void Start()
    {
        switch (_type)
        {
            case BuildingVocabulary.PairType.POLICE:
                _recognitionName = BuildingVocabulary.POLICESTATION;
                break;
            case BuildingVocabulary.PairType.STUDENT:
                _recognitionName = BuildingVocabulary.SCHOOL;
                break;
            case BuildingVocabulary.PairType.DOCTOR:
                _recognitionName = BuildingVocabulary.HOSPITAL;
                break;
            case BuildingVocabulary.PairType.CLOWN:
                _recognitionName = BuildingVocabulary.CIRCUS;
                break;
            default:
                break;
        }
    }
}

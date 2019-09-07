using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : BuildPairItem
{
    private void Start()
    {
        _pairItemType = PairType.BUILD;

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
            case BuildingVocabulary.PairType.FIREMAN:
                _recognitionName = BuildingVocabulary.FIRESTATION;
                break;
            case BuildingVocabulary.PairType.CHEF:
                _recognitionName = BuildingVocabulary.RESTAURANT;

                break;
            case BuildingVocabulary.PairType.PILOT:
                _recognitionName = BuildingVocabulary.AIRPORT;
                break;
            case BuildingVocabulary.PairType.NUN:
                _recognitionName = BuildingVocabulary.CHURCH;
                break;
            case BuildingVocabulary.PairType.SECRETARY:
                _recognitionName = BuildingVocabulary.OFFICE;
                break;
            case BuildingVocabulary.PairType.FOOTBALLER:
                _recognitionName = BuildingVocabulary.STADIUM;
                break;
            case BuildingVocabulary.PairType.ACTRESS:
                _recognitionName = BuildingVocabulary.CINEMA;
                break;
            case BuildingVocabulary.PairType.MUSICIAN:
                _recognitionName = BuildingVocabulary.MUSICSTAGE;

                break;
            default:
                break;
        }

    }


}

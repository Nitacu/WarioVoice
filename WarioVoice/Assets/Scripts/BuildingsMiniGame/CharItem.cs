using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharItem : BuildPairItem
{
    private Transform _buildTargetPosition;
    private Transform _startPosition;
    private bool _moveToTarget;
    private float _timeToGetBuild;

    private float _refVelocityX;
    private float _refVelocityY;

    private void Start()
    {
        _pairItemType = PairType.CHAR;


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
            case BuildingVocabulary.PairType.FIREMAN:
                _recognitionName = BuildingVocabulary.FIREMAN;

                break;
            case BuildingVocabulary.PairType.CHEF:
                _recognitionName = BuildingVocabulary.CHEF;
                break;
            case BuildingVocabulary.PairType.PILOT:
                _recognitionName = BuildingVocabulary.PILOT;
                break;
            case BuildingVocabulary.PairType.NUN:
                _recognitionName = BuildingVocabulary.NUN;
                break;
            case BuildingVocabulary.PairType.SECRETARY:
                _recognitionName = BuildingVocabulary.SECRETARY;
                break;
            case BuildingVocabulary.PairType.FOOTBALLER:
                _recognitionName = BuildingVocabulary.FOOTBALLER;
                break;
            case BuildingVocabulary.PairType.ACTRESS:
                _recognitionName = BuildingVocabulary.ACTRESS;
                break;
            case BuildingVocabulary.PairType.MUSICIAN:
                _recognitionName = BuildingVocabulary.MUSICIAN;

                break;
            default:
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (_moveToTarget && _buildTargetPosition != null)
        {
            Vector3 _smoothPosition = new Vector3(Mathf.SmoothDamp(_startPosition.position.x, _buildTargetPosition.position.x, ref _refVelocityX, _timeToGetBuild)
                , Mathf.SmoothDamp(_startPosition.position.y, _buildTargetPosition.position.y, ref _refVelocityY, _timeToGetBuild)
                , transform.position.z);
            transform.position = _smoothPosition;
        }
    }


    public void moveToBuild(Transform buildTarget, float timeToGetBuild)
    {
        _moveToTarget = true;
        _buildTargetPosition = buildTarget;
        _timeToGetBuild = timeToGetBuild;
        _startPosition = transform;
    }
}

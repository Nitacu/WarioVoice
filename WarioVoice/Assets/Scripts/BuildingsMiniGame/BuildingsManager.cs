using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : CommandParser
{
    #region VOCABULARIOCHARS
    private const string POLICEMAN = "Policeman";
    private const string DOCTOR = "Doctor";
    private const string STUDENT = "Student";
    #endregion

    #region VOCABULARIOBUILDS
    private const string POLICESTATION = "Police station";
    private const string HOSPITAL = "Hospital";
    private const string SCHOOL = "School";
    #endregion

    private GameObject _firstSelection;
    private GameObject _secondSelection;
    private bool _itemSelected;

    public enum PairType
    {
        POLICE, STUDENT, DOCTOR
    }

    private PairType _currentType;

    public override void parseCommand(string command)
    {
        setTypeByCommand(command);
        findItemOnBuilds();
        findItemOnChars();

        if (_firstSelection != null && _secondSelection != null)
        {
            checkPair();
        }

        base.parseCommand(command);
    }

    //simulación sin voz
    public void parseCommandSimulation(string command)
    {
        setTypeByCommand(command);
        findItemOnBuilds();
        findItemOnChars();

        if (_firstSelection != null && _secondSelection != null)
        {
            checkPair();
        }
    }

    private void checkPair()
    {
        if (_firstSelection.GetComponent<BuildItem>().Type == _secondSelection.GetComponent<BuildItem>().Type)
        {
            Debug.Log("Correcto");
        }
        else
        {
            Debug.Log("Incorecto");

        }
    }

    private void setTypeByCommand(string command)
    {
        switch (command)
        {
            case HOSPITAL:
                _currentType = PairType.DOCTOR;
                break;
            case DOCTOR:
                _currentType = PairType.DOCTOR;
                break;
            case POLICEMAN:
                _currentType = PairType.POLICE;
                break;
            case POLICESTATION:
                _currentType = PairType.POLICE;
                break;
            case STUDENT:
                _currentType = PairType.STUDENT;
                break;
            case SCHOOL:
                _currentType = PairType.STUDENT;
                break;

            default:
                break;
        }
    }

    private void setGreyGameObject(GameObject go)
    {
        go.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    private void findItemOnChars()
    {
        CharItem[] _charItem = FindObjectsOfType<CharItem>();

        foreach (var item in _charItem)
        {
            if (item.Type == _currentType)
            {
                if (_itemSelected)
                {
                    if (item.gameObject == _firstSelection)
                    {
                        _secondSelection = item.gameObject;
                        setGreyGameObject(item.gameObject);
                    }
                }
                else
                {
                    _firstSelection = item.gameObject;
                    _itemSelected = true;
                    setGreyGameObject(item.gameObject);

                }
            }
        }
    }

    private void findItemOnBuilds()
    {
        BuildItem[] _buildItem = FindObjectsOfType<BuildItem>();
        foreach (var item in _buildItem)
        {
            if (item.Type == _currentType)
            {
                if (_itemSelected)
                {
                    if (item.gameObject == _firstSelection)
                    {
                        _secondSelection = item.gameObject;
                        setGreyGameObject(item.gameObject);
                    }
                }
                else
                {
                    _firstSelection = item.gameObject;
                    _itemSelected = true;
                    setGreyGameObject(item.gameObject);

                }
            }
        }
    }


}
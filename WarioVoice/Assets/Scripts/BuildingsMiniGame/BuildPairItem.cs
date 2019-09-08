using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPairItem : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    public enum PairType
    {
        BUILD, CHAR
    }

    protected PairType _pairItemType;
    public PairType PairItemType
    {
        get { return _pairItemType;  }
    }

    private bool _pairedUp;
    public bool PairedUp
    {
        get { return _pairedUp; }
        set { _pairedUp = value; }
    }

    protected string _recognitionName;
    public string RecognitionName
    {
        get { return _recognitionName; }
    }

    [SerializeField] protected BuildingVocabulary.PairType _type;
    public BuildingVocabulary.PairType Type
    {
        get { return _type; }
    }
}

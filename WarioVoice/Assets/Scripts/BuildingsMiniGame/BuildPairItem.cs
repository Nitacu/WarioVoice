using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPairItem : MonoBehaviour
{
    [SerializeField] private string _recognitionName;
    public string RecognitionName
    {
        get { return _recognitionName; }
    }

    [SerializeField] private BuildingsManager.PairType _type;
    public BuildingsManager.PairType Type
    {
        get { return _type; }
    }
}

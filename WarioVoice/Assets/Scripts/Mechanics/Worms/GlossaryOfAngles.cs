using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryOfAngles : MonoBehaviour
{
    // Palabras que tiene que decir
    public const string DEGREES = "degrees";
    public const string DEGREE = "degree";
    public const string SYMBOL_GRADES = "°";
    public const string PERCENT = "percent";
    public const string SYMBOL_PERCENT = "%";
    private static GlossaryOfAngles _instance;

    public static GlossaryOfAngles GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GlossaryOfAngles();

        }
        return _instance;
    }

    private GlossaryOfAngles()
    {

    }
}

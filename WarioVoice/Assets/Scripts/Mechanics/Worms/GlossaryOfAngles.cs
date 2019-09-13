using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryOfAngles : MonoBehaviour
{
    // Palabras que tiene que decir
    public const string DEGREES = "degrees"; 
    //angulos 
    public const string TEN = "TEN";
    public const string TWENTY = "TWENTY";
    public const string THIRTY = "THIRTY";
    public const string FORTY = "FORTY";
    public const string FIFTY = "FIFTY";
    public const string SIXTY = "SIXTY";
    public const string SEVENTY = "SEVENTY";
    public const string EIGHTY = "EIGHTY";
    public const string NINETY = "NINETY";
    public const string ONE_HUNDRED = "ONE HUNDRED";
    public const string ONE_HUNDRED_TEN = "ONE HUNDRED TEN";
    public const string ONE_HUNDRED_TWENTY = "ONE HUNDRED TWENTY";
    public const string ONE_HUNDRED_THIRTY = "ONE HUNDRED THIRTY";
    public const string ONE_HUNDRED_FORTY = "ONE HUNDRED FORTY";
    public const string ONE_HUNDRED_FIFTY = "ONE HUNDRED FIFTY";
    public const string ONE_HUNDRED_SIXTY = "ONE HUNDRED SIXTY";
    public const string ONE_HUNDRED_SEVENTY = "ONE HUNDRED SEVENTY";
    public const string ONE_HUNDRED_EIGHTY = "ONE HUNDRED EIGHTY";


    public enum angles
    {
        RIGHT,
        ACUTEM,
        OBTUSE,
        PLAIN,
        TEN,
        TWENTY,
        THIRTY,
        FORTY,
        FIFTY,
        SIXTY,
        SEVENTY,
        EIGHTY,
        NINETY,
        ONE_HUNDRED,
        ONE_HUNDRED_TEN,
        ONE_HUNDRED_TWENTY,
        ONE_HUNDRED_THIRTY,
        ONE_HUNDRED_FORTY,
        ONE_HUNDRED_FIFTY,
        ONE_HUNDRED_SIXTY,
        ONE_HUNDRED_SEVENTY,
        ONE_HUNDRED_EIGHTY
    }


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

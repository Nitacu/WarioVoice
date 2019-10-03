using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary 
{
    // Palabras que tiene que decir
    public const string BLACK_PEPPER = "BLACK_PEPPER";
    public const string LEMON = "LEMON";
    public const string GLASSES = "GLASSES";
    public const string ONION = "ONION";
    public const string HIGH_HEEL = "HIGH_HEEL";
    public const string PERFUME = "PERFUME";
    public const string CANDY = "CANDY";
    public const string UMBRELLA = "UMBRELLA";
    public const string SPATULA = "SPATULA";
    public const string NEWSPAPER = "NEWSPAPER";
    public const string COIN = "COIN";
    public const string CUPCAKE = "CUPCAKE";

    public enum attack
    {
        BLACK_PEPPER,
        LEMON,
        GLASSES,
        ONION,
        HIGH_HEEL,
        PERFUME,
        CANDY,
        UMBRELLA,
        NEWSPAPER,
        SPATULA,
        COIN,
        CUPCAKE
    }


    private static AttackGlossary _instance;

    public static AttackGlossary GetInstance()
    {
        if (_instance == null)
        {
            _instance = new AttackGlossary();

        }
        return _instance;
    }

    private AttackGlossary()
    {

    }
}

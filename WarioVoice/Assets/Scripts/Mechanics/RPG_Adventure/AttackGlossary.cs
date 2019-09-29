using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary 
{
    // Palabras que tiene que decir
    public const string PEPPER = "PEPPER";
    public const string LEMON = "LEMON";
    public const string PAPER_PLANE = "PAPER_ PLANE";
    public const string ONION = "ONION";
    public const string CHLORINE = "CHLORINE";
    public const string PERFUME = "PERFUME";
    public const string BUBBLE_GUM = "BUBBLE_GUM";
    public const string SWORD = "SWORD";

    public enum attack
    {
        PEPPER,
        LEMON,
        PAPER_PLANE,
        ONION,
        CHORINE,
        PERFUME,
        BUBLE_BUM,
            SWORD
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

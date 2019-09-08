using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary 
{
    // Palabras que tiene que decir
    public const string THROW = "THROW";
    public const string DOWNGRADE = "DOWNGRADE";
    public const string LAUGH = "LAUGH";
    public const string SHOOT = "SHOOT";
    public const string BURN = "BURN";
    public const string ACCUSE = "ACCUSE";
    public const string EAT = "EAT";
    public const string GIVE_AWAY = "GIVE_AWAY";
    public const string LOOK = "LOOK";
    public const string SHOUT = "SHOUT";
    public const string BLOW = "BLOW";
    public const string TELL = "TELL";
    public const string FLY = "FLY";
    public const string SCRATCH = "SCRATCH";
    public const string PASTE = "PASTE";
    public const string HIT = "HIT";

    public enum attack
    {
        THROW,
        DOWNGRADE,
        LAUGH,
        SHOOT,
        BURN,
        ACCUSE,
        EAT,
        GIVE_AWAY,
        LOOK,
        SHOUT,
        BLOW,
        TELL,
        FLY,
        SCRATCH,
        PASTE,
        HIT
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

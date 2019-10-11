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

    public const string BATTERY = "BATTERY";
    public const string DRILL = "DRILL";
    public const string EXTINGUISHER = "EXTINGUISHER";
    public const string FAN = "FAN";
    public const string SAND = "SAND";
    public const string SPONGE = "SPONGE";
    public const string VACUUM_CLEANER = "VACUUM_CLEANER";
    public const string BREAD = "BREAD";
    public const string CHOCOLATE = "HOT_CHOCOLATE";
    public const string STRAWBERRY = "STRAWBERRY";
    public const string PICKAXE = "PICKAXE";

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
        CUPCAKE,
        BATTERY,
        DRILL,
        EXTINGUISHER,
        FAN,
        SAND,
        SPONGE,
        VACUUM_CLEANER,
        CHOCOLATE,
        STRAWBERRY,
        PICKAXE
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

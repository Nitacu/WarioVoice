using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary 
{
    // Palabras que tiene que decir
    public const string BLACK_PEPPER = "black pepper";
    public const string LEMON = "lemon";
    public const string GLASSES = "glasses";
    public const string ONION = "onion";
    public const string HIGH_HEEL = "high heel";
    public const string PERFUME = "perfume";
    public const string CANDY = "candy";
    public const string UMBRELLA = "umbrella";
    public const string SPATULA = "spatula";
    public const string NEWSPAPER = "newspaper";
    public const string COIN = "coin";
    public const string CUPCAKE = "cupcake";

    public const string BATTERY = "battery";
    public const string DRILL = "drill";
    public const string EXTINGUISHER = "extinguisher";
    public const string FAN = "fan";
    public const string SAND = "sand";
    public const string SPONGE = "sponge";
    public const string VACUUM_CLEANER = "vacuum cleaner";
    public const string BREAD = "bread";
    public const string CHOCOLATE = "hot chocolate";
    public const string STRAWBERRY = "strawberry";
    public const string PICKAXE = "pickaxe";

    public const string ANTENNA = "antenna";
    public const string BAT = "bat";
    public const string BOUQUET = "bouquet";
    public const string HONEY = "honey";
    public const string KITE = "kite";
    public const string LEAVES_BLOWER = "leaves blower";
    public const string RACKET = "racket";


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
        PICKAXE,
        ANTENNA,
        BAT,
        BOUQUET,
        HONEY,
        KITE,
        LEAVES_BLOWER,
        RACKET
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

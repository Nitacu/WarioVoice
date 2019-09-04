using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrimoire
{
    private static PlayerGrimoire _instance;

    //Lista de Bools para saber que hechizos ya puede usar
    public  bool _showMeMore = true;
    public  bool _biggerIsBetter = true;
    public  bool _smallIscute = true;
    public  bool _gravityBreak = true;
    public  bool _igniteSpark = true;

    //lista de bools para saber que comandos del gato se pueden usar
    public  bool _wall = true;
    public  bool _jump = true;
    public  bool _run = true;
    public  bool _stayPut = true;
    public bool _walkForward = true;
    public bool _walkBackwards = true;
    public bool _grabObj = true;

    //encantamietnos
    public const string SHOW_ME_MORE = "SHOW_ME_MORE";
    public const string BIGGER_IS_BETTER = "BIGGER_IS_BETTER";
    public const string SMALLER_IS_CUTE = "SMALLER_IS_CUTER";
    public const string GRAVITY_BREAKS = "GRAVITY_BREAKS";
    public const string IGNITE_SPARK = "IGNITE_SPARK";

    //comandos
    public const string WALK = "WALK";
    public const string JUMP = "JUMP";
    public const string RUN = "RUN";
    public const string STAY_PUT = "STAY_PUT";
    public const string WALK_FORWARD = "WALK_FORWARD";
    public const string WALK_BACKWARDS = "WALK_BACKWARDS";
    public const string GRAB = "GRAB";

    public enum enchantment
    {
        SHOW_ME_MORE,
        BIGGER_IS_BETTER,
        SMALLER_IS_CUTE,
        GRAVITY_BREAKS,
        IGNITE_SPARK
    }

    public enum commands
    {
        WALK,
        JUMP,
        RUN,
        STAY_PUT,
        WALK_FORWARD,
        WALK_BACKWARDS,
        GRAB
    }


    public static PlayerGrimoire GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerGrimoire();

        }
        return _instance;
    }

    private PlayerGrimoire()
    {

    }
}

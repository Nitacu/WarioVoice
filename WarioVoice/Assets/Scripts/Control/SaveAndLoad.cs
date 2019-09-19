using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad
{
    private const string BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE = "BOSSDIFFICULTYCHECKPOINT";

    public static int loadBossDifficulty()
    {


        if (!PlayerPrefs.HasKey(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE))
        {
            PlayerPrefs.SetInt(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE, 1);
        }

        return PlayerPrefs.GetInt(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE);        
    }

    public static void saveBossDifficulty(int _bossDifficulty)
    {
        PlayerPrefs.SetInt(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE, _bossDifficulty);
    }

   

}

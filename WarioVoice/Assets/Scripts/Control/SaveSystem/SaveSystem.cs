using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    public const string BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE = "BOSSDIFFICULTYCHECKPOINT";
    public const string PLAYERDATA_PLAYERPREFCODE = "PLAYERDATA";


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

    public static bool getSlot(int slotCode)
    {
        string playerData = PLAYERDATA_PLAYERPREFCODE + slotCode.ToString();

        if (PlayerPrefs.HasKey(playerData))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

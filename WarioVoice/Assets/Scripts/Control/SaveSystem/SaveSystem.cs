using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    public const string BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE = "BOSSDIFFICULTYCHECKPOINT";
    public const string PLAYERDATA_PLAYERPREFCODE = "PLAYERDATA";


    public static int loadCurrentBossDifficulty()
    {
        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        if (_currentPlayerInformation == null)
        {
            return 0;
        }

        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        if (PlayerPrefs.HasKey(key))
        {
            return _currentPlayerInformation.bossesDefeated + 1;
        }
        else
        {
            return 0;
        }

        /*
        if (!PlayerPrefs.HasKey(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE))
        {
            PlayerPrefs.SetInt(BOSSDIFFICULTYCHECKPOINT_PLAYERPREFCODE, 1);
        }*/

        return _currentPlayerInformation.bossesDefeated + 1;
    }

    public static void saveCurrentBossDifficulty(int _bossDifficulty)
    {
        //bossDificulty siempre es +1 de los bosses derrotados

        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        _currentPlayerInformation.bossesDefeated = _bossDifficulty - 1;

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);
    }

    public static void increasePlayedTime()
    {
        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        int _newPlayedTime = (int)Time.timeSinceLevelLoad;

        _currentPlayerInformation.playedTime += _newPlayedTime;

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);

    }


    public static void increaseMicrophonePressedTime(bool success)
    {
        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        _currentPlayerInformation.microphonePressedTimes += 1;

        if (success)
        {
            _currentPlayerInformation.microphonePressedTimesSuccesses += 1;

        }

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);
    }

}

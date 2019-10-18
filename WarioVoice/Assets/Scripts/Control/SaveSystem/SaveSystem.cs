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
        if (GameManager.GetInstance().CurrentPlayerInformation == null)
        {
            return;
        }

        //bossDificulty siempre es +1 de los bosses derrotados

        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        _currentPlayerInformation.bossesDefeated = _bossDifficulty - 1;

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);
    }

    public static void increasePlayedTime()
    {

        if (GameManager.GetInstance().CurrentPlayerInformation == null)
        {
            return;
        }

        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        int _newPlayedTime = (int)Time.timeSinceLevelLoad;

        _currentPlayerInformation.playedTime += _newPlayedTime;

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);

    }


    public static void increaseMicrophonePressedTime(bool success)
    {
        if (GameManager.GetInstance().CurrentPlayerInformation == null )
        {
            return;
        }

        Debug.Log("Increase accuracy: " + success);
        
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

    public static void increaseMicrophonePressedTime(bool success, string pronouncedWord, ChangeScene.EspikinglishMinigames miniGame)
    {
        if (GameManager.GetInstance().CurrentPlayerInformation == null)
        {
            return;
        }

        Debug.Log("Increase accuracy: " + success);

        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;

        switch (miniGame)
        {
            case ChangeScene.EspikinglishMinigames.PAINTING:
                if (!_currentPlayerInformation._pronouncedWordsPaint.Contains(pronouncedWord))
                {
                    _currentPlayerInformation._pronouncedWordsPaint.Add(pronouncedWord);
                }
                break;
            case ChangeScene.EspikinglishMinigames.ORCHESTA:
                if (!_currentPlayerInformation._pronouncedWordsOrchesta.Contains(pronouncedWord))
                {
                    _currentPlayerInformation._pronouncedWordsOrchesta.Add(pronouncedWord);
                }                
                break;
            case ChangeScene.EspikinglishMinigames.LOVE_SCENE:
                if (!_currentPlayerInformation._pronouncedWordsLove.Contains(pronouncedWord))
                {
                    _currentPlayerInformation._pronouncedWordsLove.Add(pronouncedWord);
                }
                break;
            case ChangeScene.EspikinglishMinigames.WORMS:
                if (!_currentPlayerInformation._pronouncedWordsWorms.Contains(pronouncedWord))
                {
                    _currentPlayerInformation._pronouncedWordsWorms.Add(pronouncedWord);
                }                
                break;
            case ChangeScene.EspikinglishMinigames.RPG:
                if (!_currentPlayerInformation._pronouncedWordsBoss.Contains(pronouncedWord))
                {
                    _currentPlayerInformation._pronouncedWordsBoss.Add(pronouncedWord);
                }                
                break;            
        }

        _currentPlayerInformation.microphonePressedTimes += 1;    
        
        if (success)
        {
            _currentPlayerInformation.microphonePressedTimesSuccesses += 1;
        }

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);
    }

    public static PlayerInformation getPlayerInstace()
    {
        return GameManager.GetInstance().CurrentPlayerInformation;
    }

    public static void increasePlayedAMiniGame(ChangeScene.EspikinglishMinigames miniGame, bool success)
    {
        if (GameManager.GetInstance().CurrentPlayerInformation == null)
        {
            return;
        }

        PlayerInformation _currentPlayerInformation = GameManager.GetInstance().CurrentPlayerInformation;
      
        switch (miniGame)
        {
            case ChangeScene.EspikinglishMinigames.PAINTING:
                _currentPlayerInformation.timesPlayedModernPaints += 1;
                if (!success) _currentPlayerInformation.timesLossedModernPaints += 1;
                break;
            case ChangeScene.EspikinglishMinigames.ORCHESTA:
                _currentPlayerInformation.timesPlayedOrchesta += 1;
                if (!success) _currentPlayerInformation.timesLossedOrchesta += 1;
                break;
            case ChangeScene.EspikinglishMinigames.LOVE_SCENE:
                _currentPlayerInformation.timesPlayedLoveGame += 1;
                if (!success) _currentPlayerInformation.timesLossedLoveGame += 1;
                break;
            case ChangeScene.EspikinglishMinigames.WORMS:
                _currentPlayerInformation.timesPlayedWorms += 1;
                if (!success) _currentPlayerInformation.timesLossedWorms += 1;
                break;
            case ChangeScene.EspikinglishMinigames.RPG:
                _currentPlayerInformation.timesPlayeRPG += 1;
                if (!success) _currentPlayerInformation.timesLossedRPG += 1;
                break;
        }

        string json = JsonUtility.ToJson(_currentPlayerInformation);
        string key = PLAYERDATA_PLAYERPREFCODE + _currentPlayerInformation.slotNumber.ToString();

        PlayerPrefs.SetString(key, json);
    }

}

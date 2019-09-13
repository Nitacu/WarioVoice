using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;

    private int currentGameDifficulty = 1;

    public int getGameDifficulty()
    {
        return currentGameDifficulty;
    }

    public void setGameDifficulty(int gameDifficulty)
    {
        currentGameDifficulty = gameDifficulty;
    }

    public void increaseDifficulty()
    {
        currentGameDifficulty++;
        if (currentGameDifficulty > 10)
        {
            currentGameDifficulty = 1;
        }
    }

    public static GameManager GetInstance()
    {

        if (_instance == null)
        {
            _instance = new GameManager();

        }


        return _instance;
    }

    private GameManager()
    {

    }

}

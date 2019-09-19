using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    #region LevelProgression 
    private const int _bossLevelMultiple = 2;
    private List<ChangeScene.EspikinglishMinigames> _currentLevelRoundOrder = new List<ChangeScene.EspikinglishMinigames>();
    private List<ChangeScene.EspikinglishMinigames> _uncompletedLevels = new List<ChangeScene.EspikinglishMinigames>();
    private int _currentLevelIndexProgression = 0;
    public void increaseProgression()
    {
        _currentLevelIndexProgression++;
    }
    #endregion

    #region GameDifficulty
    private int currentGameDifficulty = 1;

    public int getGameDifficulty()
    {
        return _instance.currentGameDifficulty;
    }

    public void setGameDifficulty(int gameDifficulty)
    {
        _instance.currentGameDifficulty = gameDifficulty;
    }

    public void increaseDifficulty()
    {
        _instance.currentGameDifficulty++;
        if (_instance.currentGameDifficulty > 10)
        {
            _instance.currentGameDifficulty = 1;
        }
    }
    #endregion

    private int _lives;

    private static GameManager _instance;

    public static GameManager GetInstance()
    {

        if (_instance == null)
        {
            _instance = new GameManager();

            //_instance.setLevelRound();

            _instance._lives = 4;
            //_instance._currentBossDifficulty = 1;//o según el progreso que lleve
        }

        return _instance;
    }

    #region FUNCIONESLANZARNIVEL OLD
    /*
    public void setLevelRound()
    {

        _currentLevelRoundOrder.Clear();
        _uncompletedLevels.Clear();

        List<ChangeScene.EspikinglishMinigames> _posibleLevels = new List<ChangeScene.EspikinglishMinigames>();
        _posibleLevels.Clear();

        foreach (ChangeScene.EspikinglishMinigames item in System.Enum.GetValues(typeof(ChangeScene.EspikinglishMinigames)))
        {
            _posibleLevels.Add(item);
        }

       
        _instance._currentLevelRoundOrder = sortLevels(_posibleLevels);

    }

    private List<ChangeScene.EspikinglishMinigames> sortLevels(List<ChangeScene.EspikinglishMinigames> minigamesList)
    {

        List<ChangeScene.EspikinglishMinigames> _sortedList = new List<ChangeScene.EspikinglishMinigames>();
        _sortedList.Clear();


        int numberOfMinigames = minigamesList.Count;

        for (int i = 0; i < numberOfMinigames; i++)
        {
            int randomNumber = Random.Range(0, minigamesList.Count);
            //Debug.Log("Iteración" +i +". Nivel Añadido" + _posibleLevels[randomNumber]);
            _sortedList.Add(minigamesList[randomNumber]);
            minigamesList.RemoveAt(randomNumber);
        }

        return _sortedList;
    }

    public void startGame()
    {
        _instance._currentLevelIndexProgression = 0;        
        Debug.Log("Start Game. Progression" + _instance._currentLevelIndexProgression + ", Difficulty: " + _instance.currentGameDifficulty);
        Debug.Log("Niveles por ejecutar: " + _instance._currentLevelRoundOrder.Count);
        ChangeScene.ChangeSceneProgression(_instance._currentLevelRoundOrder[_instance._currentLevelIndexProgression]);

    }

    public void launchMinigame(bool increaseProgression)
    {


        if (!increaseProgression)
        {
            _instance._uncompletedLevels.Add(_instance._currentLevelRoundOrder[_currentLevelIndexProgression]);
        }

        _instance.increaseProgression();
        
        if (_instance._currentLevelIndexProgression >= _instance._currentLevelRoundOrder.Count)//si ya terminó todos los niveles
        {

            if (_instance._uncompletedLevels.Count > 0)  //si hubo niveles que no completó
            {
                _instance._uncompletedLevels = sortLevels(_instance._uncompletedLevels);
                _instance._currentLevelRoundOrder.Clear();

                foreach (var item in _instance._uncompletedLevels)
                {
                    _currentLevelRoundOrder.Add(item);
                }

                //_instance._currentLevelRoundOrder = _instance._uncompletedLevels;
                _instance._uncompletedLevels.Clear();
                _instance._currentLevelIndexProgression = 0;
                startGame();
                return;
            }
            else
            {

                if (_instance.currentGameDifficulty % _bossLevelMultiple == 0)
                {

                    UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.RPG);
                }
                else
                {
                    _instance._currentLevelIndexProgression = 0;
                    _instance.increaseDifficulty();
                    _instance.setLevelRound();
                    startGame();
                }
            }

           
            
        }
        else
        {
            ChangeScene.ChangeSceneProgression(_instance._currentLevelRoundOrder[_instance._currentLevelIndexProgression]);
        }

    }

    public void finishBossBattle(bool defeated)
    {
        if (defeated)
        {
            _instance.increaseDifficulty();
            _instance.setLevelRound();
        }
    }
    */
    #endregion


    #region FUNCIONESLANZARNIVEL NEW

    private List<MiniGameLevel> _miniGamesRound = new List<MiniGameLevel>();
    private int _currentBossDifficulty = 1;
    private MiniGameLevel _currentMinigame;

    private void setMiniGamesRound()
    {
        //AÑADIR NIVELES 1 - SEGUN EL BOSS LEVEL => (BOSSLEVES * 2) -1

        foreach (ChangeScene.EspikinglishMinigames item in System.Enum.GetValues(typeof(ChangeScene.EspikinglishMinigames)))
        {
            MiniGameLevel _newminiGame = new MiniGameLevel(item, (_instance._currentBossDifficulty * 2) - 1);
            _instance._miniGamesRound.Add(_newminiGame);
        }

        /*
        //AÑADIR NIVELES 2 - SEGUN EL BOSS LEVEL => (BOSSLEVES * 2)
        foreach (ChangeScene.EspikinglishMinigames item in System.Enum.GetValues(typeof(ChangeScene.EspikinglishMinigames)))
        {
            MiniGameLevel _newminiGame = new MiniGameLevel(item, (_instance._currentBossDifficulty * 2));
            _instance._miniGamesRound.Add(_newminiGame);
        }*/
    }

    private MiniGameLevel returnRandomMiniGame()
    {
        MiniGameLevel _randomMiniGame;

        if (_instance._miniGamesRound.Count == 1)
        {
            _randomMiniGame = _instance._miniGamesRound[0];
            return _randomMiniGame;

        }

        if (_instance._currentMinigame == null)
        {
            int _indexRandom = Random.Range(0, _instance._miniGamesRound.Count);
            _randomMiniGame = _instance._miniGamesRound[_indexRandom];
        }
        else
        {
            do
            {
                int _indexRandom = Random.Range(0, _instance._miniGamesRound.Count);
                _randomMiniGame = _instance._miniGamesRound[_indexRandom];

            } while (_randomMiniGame._miniGame == _instance._currentMinigame._miniGame); //PARA QUE NO TIRE EL MISMO MINIJUEGO
        }

        _instance._currentMinigame = _randomMiniGame;

        return _randomMiniGame;
    }

    public void StartGame()
    {
        _instance._currentBossDifficulty = SaveAndLoad.loadBossDifficulty();

        setMiniGamesRound();

        MiniGameLevel miniGameToLaunch = returnRandomMiniGame();

        _instance.currentGameDifficulty = miniGameToLaunch._difficulty;
        _instance._currentMinigame = miniGameToLaunch;

        ChangeScene.ChangeSceneProgression(miniGameToLaunch._miniGame);
    }

    public void launchNextMinigame(bool minigamePassed)
    {
        Debug.Log("Current minigame: " + _instance._currentMinigame._miniGame);

        if (minigamePassed)
        {
            if (_instance._currentMinigame._difficulty == ((_instance._currentBossDifficulty * 2) - 1))//saber si era nivel uno 
            {
                Debug.Log("MiniGamePassed, removing from list. Lenght: "  + _instance._miniGamesRound.Count);
                _instance._miniGamesRound.Remove(_instance._currentMinigame);
                Debug.Log("Minigame removed from list. Lenght: " + _instance._miniGamesRound.Count);
                MiniGameLevel miniGameLevelUp = new MiniGameLevel(_instance._currentMinigame._miniGame, _instance._currentMinigame._difficulty + 1);
                _instance._miniGamesRound.Add(miniGameLevelUp);
            }
            else
            {
                if (_instance._currentMinigame._difficulty == (_instance._currentBossDifficulty * 2))
                {
                    _instance._miniGamesRound.Remove(_instance._currentMinigame);
                }
            }
        }
        else
        {
            _instance._lives--;

            if (_instance._lives <= 0)
            {
                //PERDER
                Debug.Log("Todas las vidas perdidas");
                UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                return;
            }
        }

        Debug.Log("Current MiniGames in queue: " + _instance._miniGamesRound.Count);

        if (_instance._miniGamesRound.Count > 0)
        {
            MiniGameLevel miniGameToLaunch = returnRandomMiniGame();

            _instance.currentGameDifficulty = miniGameToLaunch._difficulty;
            _instance._currentMinigame = miniGameToLaunch;

            ChangeScene.ChangeSceneProgression(miniGameToLaunch._miniGame);
        }
        else
        {
            //lanzar boss
            _instance.currentGameDifficulty = _instance._currentBossDifficulty;
            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.RPG);
        }
    }

    public void finisBossBattle(bool bossDefeated)
    {
        if (bossDefeated)
        {
            _instance._currentBossDifficulty++;
            SaveAndLoad.saveBossDifficulty(_instance._currentBossDifficulty);

            if (_instance._currentBossDifficulty > 5)
            {
                Debug.Log("JUEGO COMPLETADO WIII!!!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                return;
            }

            StartGame();
        }
        else
        {
            _instance._lives--;

            if (_instance._lives <= 0)
            {
                //PERDER
                Debug.Log("Todas las vidas perdidas");
                UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                return;

            }

            _instance.currentGameDifficulty = _instance._currentBossDifficulty;
            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.RPG);
        }
    }

    #endregion
}

public class MiniGameLevel
{
    public ChangeScene.EspikinglishMinigames _miniGame;
    public int _difficulty;

    public MiniGameLevel(ChangeScene.EspikinglishMinigames minigame, int difficulty)
    {
        _miniGame = minigame;
        _difficulty = difficulty;
    }
}

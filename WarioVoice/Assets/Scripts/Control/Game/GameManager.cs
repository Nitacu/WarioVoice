using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Leaves Control
    public int leavesInInventory = 3;
    public void consumeLeaf()
    {
        leavesInInventory -= 1;
    }
    public void addLeaf()
    {
        leavesInInventory += 1;
    }
    #endregion

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

    #region Persistent information
    private int _money;

    public int Money { get => _money; set => _money = value; }
    #endregion

    private int _lives;
    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public const int maxBosses = 3;
    public int maxNumberOfLives = 5;

    private PlayerInformation _currentPlayerInformation;
    public PlayerInformation CurrentPlayerInformation
    {
        get { return _currentPlayerInformation; }
        set { _currentPlayerInformation = value; }
    }

    private bool _developerMode;
    public bool DeveloperMode
    {
        get { return _developerMode; }
        set { _developerMode = value; }
    }

    private static GameManager _instance;

    public static GameManager GetInstance()
    {

        if (_instance == null)
        {
            Debug.Log("GameManager Instanciated");
            _instance = new GameManager();

            //_instance.setLevelRound();

            _instance._lives = 4;
            _instance.DeveloperMode = false;
            _instance._currentBossDifficulty = 3;//o según el progreso que lleve
            _instance.Money = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_MONEY);
        }

        return _instance;
    }

    public static void ResetInstance()
    {
        if (_instance != null)
        {
            _instance = null;
        }
    }

    #region FUNCIONESLANZARNIVEL NEW

    private List<MiniGameLevel> _miniGamesRound = new List<MiniGameLevel>();
    private int _currentBossDifficulty = 1;
    private MiniGameLevel _currentMinigame;
    public MiniGameLevel CurrentMiniGame
    {
        get { return _currentMinigame; }
    }
    private bool _liveLossed;
    public bool LiveLossed
    {
        get { return _liveLossed; }
    }
    private bool _gameLossed;
    public bool GameLossed
    {
        get { return _gameLossed; }
    }
    private bool _gameCompleted;
    public bool GameCompleted
    {
        get { return _gameCompleted; }
    }

    private void setMiniGamesRound()
    {
        Debug.Log("SetMiniGamesRound() Start");

        _instance._miniGamesRound.Clear();

        //AÑADIR NIVELES 1 - SEGUN EL BOSS LEVEL => (BOSSLEVES * 2) -1

        if (_instance._currentBossDifficulty == 1)
        {
            int difficulty = (_instance._currentBossDifficulty * 2) - 1;

            _instance._miniGamesRound.Add(new MiniGameLevel(ChangeScene.EspikinglishMinigames.LOVE_SCENE, difficulty, 1));
            _instance._miniGamesRound.Add(new MiniGameLevel(ChangeScene.EspikinglishMinigames.PAINTING, difficulty, 1));
            _instance._miniGamesRound.Add(new MiniGameLevel(ChangeScene.EspikinglishMinigames.ORCHESTA, difficulty, 1));
            _instance._miniGamesRound.Add(new MiniGameLevel(ChangeScene.EspikinglishMinigames.WORMS, difficulty, 1));

            Debug.Log("SetMiniGamesRound() return if _currentBossDifficulty == 1");
            return;
        }

        foreach (ChangeScene.EspikinglishMinigames item in System.Enum.GetValues(typeof(ChangeScene.EspikinglishMinigames)))
        {
            if (!(item == ChangeScene.EspikinglishMinigames.RPG))
            {
                int difficulty = (_instance._currentBossDifficulty * 2) - 1;
                MiniGameLevel _newminiGame = new MiniGameLevel(item, difficulty, 1);
                _instance._miniGamesRound.Add(_newminiGame);
            }
        }

        Debug.Log("SetMiniGamesRound() End");

    }

    private MiniGameLevel returnRandomMiniGame()
    {
        Debug.Log("returnRandomMiniGame() Start");


        MiniGameLevel _randomMiniGame;


        if (_instance._miniGamesRound.Count == 1)//CUANDO SOLO QUEDA UN MINIJUEGO EN COLA
        {
            _randomMiniGame = _instance._miniGamesRound[0];
            Debug.Log("returnRandomMiniGame() return _miniGamesRound.Count == 1 CUANDO SOLO QUEDA UN MINIJUEGO EN COLA");

            return _randomMiniGame;
        }

        if (_instance._currentMinigame == null)//SI ES EL PRIMERMINIJUEGO DEVOLVER UNO CUALQUIERA
        {
            int _indexRandom = Random.Range(0, _instance._miniGamesRound.Count);
            _randomMiniGame = _instance._miniGamesRound[_indexRandom];
        }
        else//SINO ELEGIR UNO SEGUN LA PRIORIDAD
        {
            int priorityParamater = 1;
            List<MiniGameLevel> _minigamesWithPriority = new List<MiniGameLevel>();
            _minigamesWithPriority.Clear();

            do
            {
                foreach (var item in _instance._miniGamesRound)
                {
                    if (item._priority == priorityParamater)
                    {
                        _minigamesWithPriority.Add(item);
                    }
                }

                priorityParamater++;

            } while (_minigamesWithPriority.Count <= 0);


            do
            {
                int _indexRandom = Random.Range(0, _minigamesWithPriority.Count);
                _randomMiniGame = _minigamesWithPriority[_indexRandom];

            } while (_randomMiniGame._miniGame == _instance._currentMinigame._miniGame); //PARA QUE NO TIRE EL MISMO MINIJUEGO          
        }

        //ORDEN PRIMERO
        if (_instance._currentBossDifficulty == 1)
        {
            bool haveLevelOne = false;

            foreach (var item in _instance._miniGamesRound)
            {
                if (item._priority == 1)
                {
                    haveLevelOne = true;
                }
            }

            if (haveLevelOne)
            {
                _randomMiniGame = _instance._miniGamesRound[0];
            }
        }

        _instance._currentMinigame = _randomMiniGame;

        Debug.Log("returnRandomMiniGame() end return random miniGame");

        return _randomMiniGame;
    }

    public void StartGame()
    {
        Debug.Log("StartGame() Start");

        _instance._liveLossed = false;
        _instance._gameLossed = false;
        _instance._gameCompleted = false;

        _instance._currentBossDifficulty = SaveSystem.loadCurrentBossDifficulty();

        setMiniGamesRound();

        MiniGameLevel miniGameToLaunch = returnRandomMiniGame();

        _instance.currentGameDifficulty = miniGameToLaunch._difficulty;
        _instance._currentMinigame = miniGameToLaunch;

        UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.BETWEENMINIGAMES);

        Debug.Log("StartGame() End");

    }

    public void launchNextMinigame(bool minigamePassed)
    {
        Debug.Log("launchNextMinigame() Start");

        SaveSystem.increasePlayedTime();
        SaveSystem.increasePlayedAMiniGame(_currentMinigame._miniGame, minigamePassed);

        _instance._liveLossed = false;
        _instance._gameLossed = false;

        if (minigamePassed)
        {
            if (_instance._currentMinigame._difficulty == ((_instance._currentBossDifficulty * 2) - 1))//saber si era nivel uno 
            {
                _instance._miniGamesRound.Remove(_instance._currentMinigame);
                int difficulty = _instance._currentMinigame._difficulty + 1;
                MiniGameLevel miniGameLevelUp = new MiniGameLevel(_instance._currentMinigame._miniGame, difficulty, _instance._currentMinigame._priority + 1);
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
            _instance._liveLossed = true;
            /*
            //SUBIR PRIORIDAD
            foreach (var item in _instance._miniGamesRound)//SUBIR PRIORIDAD
            {
                if (item == _instance._currentMinigame)
                {
                    item._priority += 1;
                    Debug.Log("Nueva prioridad a: " + item._miniGame + " establecida en: " + item._priority);
                }
            }
            */
            MiniGameLevel newpriorityMiniGame = _instance._currentMinigame;
            newpriorityMiniGame._priority += 1;
            _instance._miniGamesRound.Remove(_instance._currentMinigame);
            _instance._miniGamesRound.Add(newpriorityMiniGame);



            if (_instance._lives <= 0)
            {
                //PERDER
                Debug.Log("Todas las vidas perdidas");
                _instance._gameLossed = true;
            }
        }

        if (_instance._miniGamesRound.Count > 0)
        {
            MiniGameLevel miniGameToLaunch = returnRandomMiniGame();

            _instance.currentGameDifficulty = miniGameToLaunch._difficulty;
            _instance._currentMinigame = miniGameToLaunch;
        }
        else
        {
            //lanzar boss
            _instance.currentGameDifficulty = _instance._currentBossDifficulty;

            MiniGameLevel miniGameToLaunch = new MiniGameLevel(ChangeScene.EspikinglishMinigames.RPG, _instance._currentBossDifficulty, 1);
            _instance._currentMinigame = miniGameToLaunch;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.BETWEENMINIGAMES);
        Debug.Log("launchNextMinigame() End");
    }

    public void LoadMinigame()
    {
        Debug.Log("LoadMinigame() Start");

        //_instance.currentGameDifficulty = _instance._currentMinigame._difficulty;
        ChangeScene.ChangeSceneProgression(_instance._currentMinigame._miniGame);

        Debug.Log("LoadMinigame() End");
    }

    public void finisBossBattle(bool bossDefeated)
    {
        Debug.Log("finisBossBattle() Start");

        SaveSystem.increasePlayedAMiniGame(_currentMinigame._miniGame, bossDefeated);
        SaveSystem.increasePlayedTime();

        _instance._liveLossed = false;
        //_instance._gameCompleted = false;

        if (bossDefeated)
        {
            _instance._currentBossDifficulty++;
            SaveSystem.saveCurrentBossDifficulty(_instance._currentBossDifficulty);

            if (_instance._currentBossDifficulty > maxBosses)
            {
                Debug.Log("JUEGO COMPLETADO WIII!!!");
                _instance._gameCompleted = true;
                _instance._currentMinigame = null;
                UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.BETWEENMINIGAMES);
                Debug.Log("finisBossBattle() return _currentBossDifficulty > maxBosses");
                return;
            }

            //StartGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.BOSSDEFEATED);
            Debug.Log("finisBossBattle() return bossDefeated");
            return;
        }
        else
        {
            _instance._lives--;
            _instance._liveLossed = true;

            if (_instance._lives <= 0)
            {
                //PERDER
                Debug.Log("Todas las vidas perdidas");
                _instance._gameLossed = true;

            }

            _instance.currentGameDifficulty = _instance._currentBossDifficulty;
            MiniGameLevel miniGameToLaunch = new MiniGameLevel(ChangeScene.EspikinglishMinigames.RPG, _instance._currentBossDifficulty, 1);
            _instance._currentMinigame = miniGameToLaunch;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.BETWEENMINIGAMES);
        Debug.Log("finisBossBattle() End");
    }
    #endregion

}

public class MiniGameLevel
{
    public ChangeScene.EspikinglishMinigames _miniGame;
    public int _difficulty;
    public int _priority;

    public MiniGameLevel(ChangeScene.EspikinglishMinigames minigame, int difficulty, int priority)
    {
        _miniGame = minigame;
        _difficulty = difficulty;
        _priority = priority;
    }
}

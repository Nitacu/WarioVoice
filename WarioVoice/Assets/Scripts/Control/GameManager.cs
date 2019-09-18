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

    private static GameManager _instance;

    public static GameManager GetInstance()
    {

        if (_instance == null)
        {
            _instance = new GameManager();
            _instance.setLevelRound();

        }

        return _instance;
    }



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

        /*


        List<ChangeScene.EspikinglishMinigames> _levelOrder = new List<ChangeScene.EspikinglishMinigames>();
        _levelOrder.Clear();

        foreach (ChangeScene.EspikinglishMinigames item in System.Enum.GetValues(typeof(ChangeScene.EspikinglishMinigames)))
        {
            _posibleLevels.Add(item);
        }

        int numberOfMinigames = _posibleLevels.Count;

        for (int i = 0; i < numberOfMinigames; i++)
        {
            int randomNumber = Random.Range(0, _posibleLevels.Count);
            //Debug.Log("Iteración" +i +". Nivel Añadido" + _posibleLevels[randomNumber]);
            _levelOrder.Add(_posibleLevels[randomNumber]);
            _posibleLevels.RemoveAt(randomNumber);
        }


    */
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
        Debug.Log("progression increased to: " + _instance._currentLevelIndexProgression);
        Debug.Log("All levels: " + _instance._currentLevelRoundOrder.Count);

        if (_instance._currentLevelIndexProgression >= _instance._currentLevelRoundOrder.Count)//si ya terminó todos los niveles
        {

            Debug.Log("niveles pasados, revisar niveles incompletos");

            if (_instance._uncompletedLevels.Count > 0)  //si hubo niveles que no completó
            {
                Debug.Log("Si hay niveles incompletos, reemplazar. Niveles incompletos: " + _instance._uncompletedLevels.Count);
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
                Debug.Log("No hay niveles incompletos");

                if (_instance.currentGameDifficulty % _bossLevelMultiple == 0)
                {
                    Debug.Log("Lanzar Boss");

                    UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.RPG);
                }
                else
                {
                    _instance._currentLevelIndexProgression = 0;
                    Debug.Log("Incrementar nivel");
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

}

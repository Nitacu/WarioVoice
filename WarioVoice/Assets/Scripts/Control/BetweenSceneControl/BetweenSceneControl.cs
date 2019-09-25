using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetweenSceneControl : MonoBehaviour
{
    private const string NEXTMINIGAMEIN_ENG = "Next game";
    private const string NEXTMINIGAMEIN_ESP = "Siguiente juego";
    private const string BOSSBATTLE_ENG = "Boss Battle";
    private const string BOSSBATLE_ESP = "Jefe";
    private const string LOSE_ENG = "You lose";
    private const string LOSE_ESP = "Perdiste";
    private const string COMPLETED = "You complete the Game!";

    private const string CLIPFLAGDOWNNAME = "FlagDown";



    [SerializeField] private List<GameObject> _liveFlags = new List<GameObject>();
    [SerializeField] private Sprite _flagDown;

    [SerializeField] private float _timeToLaunchNextMinigame;
    private float _timeTracking;

    [SerializeField] private TextMeshProUGUI _timeTextENG;
    [SerializeField] private TextMeshProUGUI _timeTextESP;
    [SerializeField] private TextMeshProUGUI _timerNormalText;

    [Header("Game Lossed Vars")]
    [SerializeField] private GameObject _continuePanel;
    [SerializeField] private int _timeToLaunchToMainMenu;
    [SerializeField] private TextMeshProUGUI _timerDownText;

    private bool _gameCompleted;
    private bool _gameLossed;

    private void Update()
    {
        if (_timeTracking > 0)
        {
            _timeTracking -= Time.deltaTime;
        }
        else
        {
            if (!GameManager.GetInstance().GameLossed && !GameManager.GetInstance().GameCompleted)
            {
                GameManager.GetInstance().LoadMinigame();
            }
            else
            {
                if (GameManager.GetInstance().GameCompleted)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                }
                else if (GameManager.GetInstance().GameLossed)
                {
                    //volver al menu
                    //UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                    continueGame(false);

                    //empezar desde el checkpoint
                    //GameManager.GetInstance().Lives = 4;
                    //GameManager.GetInstance().StartGame();
                }
            }
        }

        int timeToshow = Mathf.FloorToInt(_timeTracking);

        if (!GameManager.GetInstance().GameLossed && !GameManager.GetInstance().GameCompleted)
        {
            _timeTextENG.text = (GameManager.GetInstance().CurrentMiniGame._miniGame == ChangeScene.EspikinglishMinigames.RPG) ? BOSSBATTLE_ENG + timeToshow.ToString() : NEXTMINIGAMEIN_ENG;
            _timeTextESP.text = (GameManager.GetInstance().CurrentMiniGame._miniGame == ChangeScene.EspikinglishMinigames.RPG) ? BOSSBATLE_ESP + timeToshow.ToString() : NEXTMINIGAMEIN_ESP;


            if (timeToshow < 1)
            {
                _timerNormalText.text = "GO!";
                FindObjectOfType<BetweenSceneAudioControl>().playGO();
            }
            else
            {
                _timerNormalText.text = timeToshow.ToString();
            }

        }
        else if (GameManager.GetInstance().GameLossed)
        {
            _timerDownText.text = timeToshow.ToString();
            FindObjectOfType<BetweenSceneAudioControl>().playGameOver();
        }
    }

    private void Start()
    {
        _timeTracking = _timeToLaunchNextMinigame;
        disableFlags();

        _gameCompleted = GameManager.GetInstance().GameCompleted;
        _gameLossed = GameManager.GetInstance().GameLossed;

        if (_gameLossed)
        {
            _timeTextENG.text = LOSE_ENG;
            _timeTextESP.text = LOSE_ESP;

            foreach (var item in _liveFlags)
            {
                item.SetActive(false);
            }

            _continuePanel.SetActive(true);
            _timeTracking = _timeToLaunchToMainMenu;
        }
        else if (_gameCompleted)
        {
            _timeTextENG.text = COMPLETED;
            _timeTextESP.text = COMPLETED;
        }

        if (GameManager.GetInstance().LiveLossed)
        {
            int _currentLives = GameManager.GetInstance().Lives + 1;

            for (int i = 0; i < _currentLives; i++)
            {
                _liveFlags[i].GetComponent<Animator>().enabled = true;
            }

            _liveFlags[_currentLives - 1].GetComponent<Animator>().Play(Animator.StringToHash(CLIPFLAGDOWNNAME));
        }
        else
        {
            int _currentLives = GameManager.GetInstance().Lives;

            for (int i = 0; i < _currentLives; i++)
            {
                _liveFlags[i].GetComponent<Animator>().enabled = true;
            }
        }
    }

    private void disableFlags()
    {
        foreach (var item in _liveFlags)
        {
            item.GetComponent<Animator>().enabled = false;
            item.GetComponent<SpriteRenderer>().sprite = _flagDown;
        }
    }

    public void continueGame(bool wantContinue)
    {
        if (wantContinue)
        {
            GameManager.GetInstance().Lives = 4;
            GameManager.GetInstance().StartGame();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
        }
    }
}

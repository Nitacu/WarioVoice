using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetweenSceneControl : MonoBehaviour
{
    private const string NEXTMINIGAMEIN = "Next in ";
    private const string CLIPFLAGDOWNNAME = "FlagDown";
    private const string LOSE = "You lose";
    private const string COMPLETED = "You complete the Game!";


    [SerializeField] private List<GameObject> _liveFlags = new List<GameObject>();
    [SerializeField] private Sprite _flagDown;

    [SerializeField] private float _timeToLaunchNextMinigame;
    private float _timeTracking;

    [SerializeField] private TextMeshProUGUI _timeText;


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
                    UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.WARIOVOICEMENU);
                    
                    //empezar desde el checkpoint
                    //GameManager.GetInstance().Lives = 4;
                    //GameManager.GetInstance().StartGame();
                }
            }
        }

        if (!GameManager.GetInstance().GameLossed && !GameManager.GetInstance().GameCompleted)
        {
            int timeToshow = Mathf.FloorToInt(_timeTracking) + 1;
            _timeText.text = NEXTMINIGAMEIN + timeToshow.ToString();
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
            _timeText.text = LOSE;
        }
        else if (_gameCompleted)
        {
            _timeText.text = COMPLETED;
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
}

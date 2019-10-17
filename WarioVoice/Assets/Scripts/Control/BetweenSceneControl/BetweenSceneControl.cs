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
    private const string LOSE_ENG = "Game over";
    private const string LOSE_ESP = "Juego terminado";
    private const string COMPLETED_ESP = "Thanks for playing Spikinglish Demo!";
    private const string COMPLETED_ENG = "Gracias por jugar el demo de Spikinglish!";

    private const string CLIP_lIVE_IDLE = "IdleLive";
    private const string CLIP_lIVE_FALLING = "LiveFalling";
    private const string CLIP_LIVE_DOWN = "LiveDown";



    [SerializeField] private List<GameObject> _lives = new List<GameObject>();
    [SerializeField] private Sprite liveDOWN;
    [SerializeField] private AnimationClip fallingClip;
    [SerializeField] private GameObject _confetti;

    [SerializeField] private float _timeToLaunchNextMinigame;
    private float _timeTracking;

    [SerializeField] private TextMeshProUGUI _timeTextENG;
    [SerializeField] private TextMeshProUGUI _timeTextESP;
    [SerializeField] private TextMeshProUGUI _timerNormalText;

    [Header("Game Lossed Vars")]
    [SerializeField] private GameObject _continuePanel;
    [SerializeField] private int _timeToLaunchToMainMenu;
    [SerializeField] private TextMeshProUGUI _timerDownText;

    [Header("AudioClips")]
    [SerializeField] private AudioSource _sourceVoice;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _thanksForPlayingClip;

    [Header("AudioClipsEffects")]
    [SerializeField] private AudioClip _gameOverEffect;
    [SerializeField] private AudioClip _gameCompleteEffect;


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
                    UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.SPIKINGLISHMENU);
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
        timeToshow = Mathf.Clamp(timeToshow, 0, timeToshow);

        if (!GameManager.GetInstance().GameLossed && !GameManager.GetInstance().GameCompleted)
        {
            _timeTextENG.text = (GameManager.GetInstance().CurrentMiniGame._miniGame == ChangeScene.EspikinglishMinigames.RPG) ? BOSSBATTLE_ENG : NEXTMINIGAMEIN_ENG;
            _timeTextESP.text = (GameManager.GetInstance().CurrentMiniGame._miniGame == ChangeScene.EspikinglishMinigames.RPG) ? BOSSBATLE_ESP : NEXTMINIGAMEIN_ESP;


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

        }
    }

    private void Start()
    {
        _timerNormalText.text = "";

        _timeTracking = _timeToLaunchNextMinigame;

        //disableFlags();

        _gameCompleted = GameManager.GetInstance().GameCompleted;
        _gameLossed = GameManager.GetInstance().GameLossed;

        if (_gameLossed)
        {
            _timeTextENG.text = LOSE_ENG;
            _timeTextESP.text = LOSE_ESP;

            FindObjectOfType<BetweenSceneAudioControl>().playGameOver();
            StartCoroutine(playClip(_sourceVoice, _gameOverClip, _gameOverEffect.length));

            foreach (var item in _lives)
            {
                item.SetActive(false);
            }

            _continuePanel.SetActive(true);
            _timeTracking = _timeToLaunchToMainMenu;
        }
        else if (_gameCompleted)
        {
            _timeTextENG.text = COMPLETED_ESP;
            _timeTextESP.text = COMPLETED_ENG;
            _timeTracking = _timeToLaunchToMainMenu;
            GameObject confetti = Instantiate(_confetti);
            confetti.transform.position = Vector3.zero;
            FindObjectOfType<BetweenSceneAudioControl>().playGreat();
            StartCoroutine(playClip(_sourceVoice, _thanksForPlayingClip, _gameCompleteEffect.length));

        }

        int _currentLives = GameManager.GetInstance().Lives;

        for (int i = 0; i < _lives.Count; i++)
        {
            if (i >= _currentLives)
            {

                if (i == _currentLives)
                {
                    if (GameManager.GetInstance().LiveLossed)
                    {
                        _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_FALLING));
                    }
                    else
                    {
                        _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_LIVE_DOWN));
                    }
                }
                else
                {
                    _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_LIVE_DOWN));
                }


            }
            else
            {
                _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_IDLE));

            }
        }
        /*
        if (GameManager.GetInstance().LiveLossed)
        {
            int _currentLives = GameManager.GetInstance().Lives + 1;

            for (int i = 0; i < _currentLives; i++)
            {
                _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_IDLE));
                _lives[i].GetComponent<Animator>().enabled = true;
            }

            _lives[_currentLives - 1].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_FALLING));

            StartCoroutine(setLiveDown(fallingClip.averageDuration, _lives[_currentLives - 1]));
            

            for (int i = _lives.Count - 1; i > _currentLives - 1; i--)
            {
                _lives[i].GetComponent<Animator>().enabled = true;
                _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_LIVE_DOWN));
            }
        }
        else
        {
            int _currentLives = GameManager.GetInstance().Lives;

            for (int i = 0; i < _currentLives; i++)
            {
                _lives[i].GetComponent<Animator>().enabled = true;
                _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_IDLE));
            }

            for (int i = _lives.Count -1 ; i > _currentLives -1; i--)
            {
                _lives[i].GetComponent<Animator>().enabled = true;
                _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_LIVE_DOWN));
            }
        }*/
    }
    IEnumerator setLiveDown(float timeToSet, GameObject live)
    {
        yield return new WaitForSeconds(timeToSet);

        live.GetComponent<Animator>().Play(Animator.StringToHash(CLIP_LIVE_DOWN));

    }

    private void disableFlags()
    {
        foreach (var item in _lives)
        {
            item.GetComponent<Animator>().enabled = false;
            //item.GetComponent<SpriteRenderer>().sprite = liveDOWN;
            StartCoroutine(setLiveDown(0, item));
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.SPIKINGLISHMENU);
        }
    }

    IEnumerator playClip(AudioSource source, AudioClip clip, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        source.Pause();
        source.clip = clip;
        source.Play();
    }
}

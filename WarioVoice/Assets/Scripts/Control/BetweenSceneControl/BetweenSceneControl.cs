using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class BetweenSceneControl : MonoBehaviour
{
    private const string NEXTMINIGAMEIN_ENG = "Next game";
    private const string NEXTMINIGAMEIN_ESP = "Siguiente juego";
    private const string BOSSBATTLE_ENG = "Boss Battle";
    private const string BOSSBATLE_ESP = "Jefe";
    private const string LOSE_ENG = "Game over";
    private const string LOSE_ESP = "";
    private const string COMPLETED_ESP = "Thanks for playing Espikinglish Demo!";
    private const string COMPLETED_ENG = "Gracias por jugar el demo de Espikinglish!";

    private const string CLIP_lIVE_IDLE = "IdleLive";
    private const string CLIP_lIVE_FALLING = "LiveFalling";
    private const string CLIP_LIVE_DOWN = "LiveDown";



    [SerializeField] private List<GameObject> _lives = new List<GameObject>();
    [SerializeField] private Sprite liveDOWN;
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._fallingClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AnimationClip _fallingClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._fallingClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._confetti' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _confetti;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._confetti' nunca se asigna y siempre tendrá el valor predeterminado null

#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timeToLaunchNextMinigame' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _timeToLaunchNextMinigame;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timeToLaunchNextMinigame' nunca se asigna y siempre tendrá el valor predeterminado 0
    private float _timeTracking;

#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timeTextENG' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _timeTextENG;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timeTextENG' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timeTextESP' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _timeTextESP;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timeTextESP' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timerNormalText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _timerNormalText;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timerNormalText' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("Game Lossed Vars")]
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._gameCompletedPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _gameCompletedPanel;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._gameCompletedPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._sourceMusic' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioSource _sourceMusic;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._sourceMusic' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("Game Lossed Vars")]
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._continuePanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _continuePanel;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._continuePanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timeToLaunchToMainMenu' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private int _timeToLaunchToMainMenu;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timeToLaunchToMainMenu' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._timerDownText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _timerDownText;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._timerDownText' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("AudioClips")]
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._sourceVoice' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioSource _sourceVoice;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._sourceVoice' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._gameOverClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _gameOverClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._gameOverClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._thanksForPlayingClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _thanksForPlayingClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._thanksForPlayingClip' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("AudioClipsEffects")]
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._gameOverEffect' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _gameOverEffect;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._gameOverEffect' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._gameCompleteEffect' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _gameCompleteEffect;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._gameCompleteEffect' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("Publicity")]
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._videoPublicity' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _videoPublicity;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._videoPublicity' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._videoClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private VideoClip _videoClip;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._videoClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'BetweenSceneControl._titles' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _titles;
#pragma warning restore CS0649 // El campo 'BetweenSceneControl._titles' nunca se asigna y siempre tendrá el valor predeterminado null

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
            Debug.Log("Time tracking start)");

            if (!GameManager.GetInstance().GameLossed && !GameManager.GetInstance().GameCompleted)
            {
                Debug.Log("Time tracking start !gamelossed and !completed)");

                GameManager.GetInstance().LoadMinigame();

                Debug.Log("Time tracking end gamelossed and completed)");

            }
            else
            {
                Debug.Log("Time tracking start gamelossed and completed)");            

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
        Debug.Log("Start() start " + _videoClip.length);

        _timerNormalText.text = "";

        _timeTracking = _timeToLaunchNextMinigame;

        //disableFlags();

       
        _gameCompleted = GameManager.GetInstance().GameCompleted;
        _gameLossed = GameManager.GetInstance().GameLossed;        


        if (_gameLossed)
        {
            Debug.Log("game manager game lossed - if start");

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

            Debug.Log("game manager game lossed - if start");
        }
        else if (_gameCompleted)
        {
            Debug.Log("game manager game completed - if start");

            _timeTextENG.text = COMPLETED_ESP;
            _timeTextESP.text = COMPLETED_ENG;

            _timeTracking = _timeToLaunchToMainMenu;

            GameObject confetti = Instantiate(_confetti);
            confetti.transform.position = Vector3.zero;

            FindObjectOfType<BetweenSceneAudioControl>().playGreat();
            StartCoroutine(playClip(_sourceVoice, _thanksForPlayingClip, _gameCompleteEffect.length));

            foreach (var item in _lives)
            {
                item.SetActive(false);
            }

            _gameCompletedPanel.SetActive(true);
            _sourceMusic.Play();

            Debug.Log("game manager game completed - if end");
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
                        Debug.Log("live Lossed live falling");

                        _lives[i].GetComponent<Animator>().Play(Animator.StringToHash(CLIP_lIVE_FALLING));
                        StartCoroutine(setLiveDown(_fallingClip.length, _lives[i]));
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

        Debug.Log("Start() End");
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
            Debug.Log("Quiere continuar juego start");

            StartCoroutine(playPublicity());
            
            Debug.Log("Quiere continuar juego end");

        }
        else
        {
            Debug.Log("No quiere continuar juego start");

            UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeScene.SPIKINGLISHMENU);

            Debug.Log("No quiere continuar juego end");

        }
    }

    IEnumerator playPublicity()
    {
        _titles.SetActive(false);
        _continuePanel.SetActive(false);
        _videoPublicity.SetActive(true);
        yield return new WaitForSeconds((float)_videoClip.frameCount+1f);
        _titles.SetActive(true);
        _videoPublicity.SetActive(false);
        GameManager.GetInstance().Lives = 1;
        GameManager.GetInstance().StartGame();
    }

    IEnumerator playClip(AudioSource source, AudioClip clip, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        source.Pause();
        source.clip = clip;
        source.Play();
    }
}

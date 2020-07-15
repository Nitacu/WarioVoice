using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoodActionsController : MonoBehaviour
{
    [Header("Cooldown Times")]
    [SerializeField] private float _playSecondsToWait = 28800; //8 horas
    [SerializeField] private float _eatSecondsToWait = 28800;
    [SerializeField] private float _showerSecondsToWait = 28800;
    [SerializeField] private float _moodUpdateSecondsToWait = 43200; // 12 horas
    [Header("Mood Points")]
    [SerializeField] private float _playPoints = 20;
    [SerializeField] private float _foodPoints = 50;
    [SerializeField] private float _showerPoints = 20;
    [SerializeField] private float _moodPoints = -20;
    [SerializeField] private TitoMoodController _titoMood;
    [Header("UI Stuff")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _eatButton;
    [SerializeField] private Button _showerButton;
    [SerializeField] private TextMeshProUGUI _playText;
    [SerializeField] private TextMeshProUGUI _eatText;
    [SerializeField] private TextMeshProUGUI _showerText;
    [SerializeField] private List<GameObject> _titoLifes;
    [Header("UI Clocks")]
    [SerializeField] private Image _playClock;
    [SerializeField] private Image _eatClock;
    [SerializeField] private Image _showerClock;
    [Header("RAIN PREFAB")]
    [SerializeField] private GameObject _rainPrefab;

    private ulong _lastPlayAction;
    private ulong _lastShowerAction;
    private ulong _lastEatAction;
    private ulong _lastMoodUpdateAction;
    private bool _isMoodUpdated = false;

    private const string LAST_EAT_KEY = "LastEat";
    private const string LAST_PLAY_KEY = "LastPlay";
    private const string LAST_SHOWER_KEY = "LastShower";
    private const string LAST_MOOD_UPDATE_KEY = "MoodUpdate";


    public enum ENUM_Actions
    {
        PLAY,
        SHOWER,
        EAT,
        MOOD_UPDATE
    }

    private void Awake()
    {
        if (PlayerPrefs.GetString(LAST_EAT_KEY) != null)
        {
            ulong.TryParse(PlayerPrefs.GetString(LAST_EAT_KEY), out _lastEatAction);
        }
        if (PlayerPrefs.GetString(LAST_PLAY_KEY) != null)
        {
            ulong.TryParse(PlayerPrefs.GetString(LAST_PLAY_KEY), out _lastPlayAction);
        }
        if (PlayerPrefs.GetString(LAST_SHOWER_KEY) != null)
        {
            ulong.TryParse(PlayerPrefs.GetString(LAST_SHOWER_KEY), out _lastShowerAction);
        }
        if (PlayerPrefs.GetString(LAST_MOOD_UPDATE_KEY) != null)
        {
            ulong.TryParse(PlayerPrefs.GetString(LAST_MOOD_UPDATE_KEY), out _lastMoodUpdateAction);
            Debug.Log(_lastMoodUpdateAction);
        }
    }

    private void Start()
    {
        checkButtonsOnStart();
    }

    private void Update()
    {
        checkButtonsOnUpdate();
    }

    public void updateUILifes()
    {
        int numberOfLives = GameManager.GetInstance().maxNumberOfLives;
        
        for(int i = 0; i< _titoLifes.Count; i++)
        {
            if (i < numberOfLives)
            {
                _titoLifes[i].SetActive(true);
            }
            else
            {
                _titoLifes[i].SetActive(false);
            }
        }
    }

    public void restartPrefabs()
    {
        PlayerPrefs.SetString(TutorialKoalaRoomControl.TUTORIAL_COMPLETED_KEY, "false");
        PlayerPrefs.SetString(LAST_PLAY_KEY, "0");
        PlayerPrefs.SetString(LAST_EAT_KEY, "0");
        PlayerPrefs.SetString(LAST_MOOD_UPDATE_KEY, "0");
        PlayerPrefs.SetString(LAST_SHOWER_KEY, "0");

        _lastEatAction = 0;
        _lastPlayAction = 0;
        _lastShowerAction = 0;
        _lastMoodUpdateAction = 0;
        _isMoodUpdated = false;
    }


    private void checkButtonsOnUpdate()
    {
        if (!_isMoodUpdated)
        {
            if (isMoodUpdateActionReady())
            {
                _isMoodUpdated = true;
                updateMoodBar();
            }
        }

        if (!_playButton.IsInteractable())
        {
            _playText.text = getTimeLeft(ENUM_Actions.PLAY);           
            _playClock.fillAmount = (getSecondsLeft(ENUM_Actions.PLAY) / _playSecondsToWait);
            if (_playClock.fillAmount < 0)
            {
                _playClock.fillAmount = 0;
            }
            if (_playClock.fillAmount > 1)
            {
                _playClock.fillAmount = 1;
            }

            if (isPlayActionReady())
            {
                _playButton.interactable = true;
                FindObjectOfType<BallDragNDrop>().CanBallBeDragged = true;
                _playText.text = "";
            }
        }

        if (!_eatButton.IsInteractable())
        {
            _eatText.text = getTimeLeft(ENUM_Actions.EAT);
            _eatClock.fillAmount = getSecondsLeft(ENUM_Actions.EAT) / _eatSecondsToWait;

            if (_eatClock.fillAmount < 0)
            {
                _eatClock.fillAmount = 0;
            }
            if (_eatClock.fillAmount > 1)
            {
                _eatClock.fillAmount = 1;
            }

            if (isEatActionReady())
            {
                _eatButton.interactable = true;
                _eatText.text = "";
            }
        }

        if (!_showerButton.IsInteractable())
        {
            _showerText.text = getTimeLeft(ENUM_Actions.SHOWER);
            _showerClock.fillAmount = getSecondsLeft(ENUM_Actions.SHOWER) / _showerSecondsToWait;

            if (_showerClock.fillAmount < 0)
            {
                _showerClock.fillAmount = 0;
            }
            if (_showerClock.fillAmount > 1)
            {
                _showerClock.fillAmount = 1;
            }

            if (isShowerActionReady())
            {
                _showerButton.interactable = true;
                _showerText.text = "";
            }
        }
    }
    private void checkButtonsOnStart()
    {
        if (!isPlayActionReady())
        {
            _playButton.interactable = false;
        }

        if (!isEatActionReady())
        {
            _eatButton.interactable = false;
        }

        if (!isShowerActionReady())
        {
            _showerButton.interactable = false;
        }

        if (!isMoodUpdateActionReady())
        {
            Debug.Log("Im true");
            _isMoodUpdated = true;
        }
    }
    private string getTimeLeft(ENUM_Actions action)
    {
        ulong lastAction = 0;
        float secondsToWait = 0;

        switch (action)
        {
            case ENUM_Actions.PLAY:
                lastAction = _lastPlayAction;
                secondsToWait = _playSecondsToWait;
                break;
            case ENUM_Actions.SHOWER:
                lastAction = _lastShowerAction;
                secondsToWait = _showerSecondsToWait;
                break;
            case ENUM_Actions.EAT:
                lastAction = _lastEatAction;
                secondsToWait = _eatSecondsToWait;
                break;
        }

        //timer
        ulong diff = ((ulong)System.DateTime.Now.Ticks - lastAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((secondsToWait * 1000) - miliseconds) / 1000;

        string timer = "";

        bool isHours = false;
        //Hours
        timer += ((int)secondsLeft / 3600).ToString() + "H ";
        if (((int)secondsLeft / 3600) > 0)
        {
            isHours = true;
            timer = ((int)secondsLeft / 3600).ToString() + "H";
        }
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        //minutes
        //timer += ((int)secondsLeft / 60).ToString("00") + "M ";
        //seconds
        //timer += (secondsLeft % 60).ToString("00") + "S";

        if (!isHours)
        {
            timer = (secondsLeft % 60).ToString("00") + "S";

            if (((int)secondsLeft / 60) > 0)
            {
                timer = ((int)secondsLeft / 60).ToString("00") + "M";
            }
        }
        

        return timer;
    }

    private float getSecondsLeft(ENUM_Actions action)
    {
        ulong lastAction = 0;
        float secondsToWait = 0;

        switch (action)
        {
            case ENUM_Actions.PLAY:
                lastAction = _lastPlayAction;
                secondsToWait = _playSecondsToWait;
                break;
            case ENUM_Actions.SHOWER:
                lastAction = _lastShowerAction;
                secondsToWait = _showerSecondsToWait;
                break;
            case ENUM_Actions.EAT:
                lastAction = _lastEatAction;
                secondsToWait = _eatSecondsToWait;
                break;
        }

        //timer
        ulong diff = ((ulong)System.DateTime.Now.Ticks - lastAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((secondsToWait * 1000) - miliseconds) / 1000;

        float extraSeconds = 0;
        //Hours check
        if (((int)secondsLeft / 3600) > 0)
        {
            extraSeconds = 3600 * ((int)secondsLeft / 3600);
        }
        secondsLeft -= (((int)secondsLeft / 3600) * 3600);
        secondsLeft += extraSeconds;
        return secondsLeft;
    }

    #region button click actions
    public void playWithTito()
    {
        FindObjectOfType<BallDragNDrop>().CanBallBeDragged = false;
        _lastPlayAction = (ulong)System.DateTime.Now.Ticks;
        PlayerPrefs.SetString(LAST_PLAY_KEY, _lastPlayAction.ToString());
        _titoMood.addMoodPoints(_playPoints);
        _playButton.interactable = false;
    }
    public void eatWithTito()
    {
        _lastEatAction = (ulong)System.DateTime.Now.Ticks;
        PlayerPrefs.SetString(LAST_EAT_KEY, _lastEatAction.ToString());
        _titoMood.addMoodPoints(_foodPoints);
        _eatButton.interactable = false;
    }
    public void showerWithTito()
    {
        Camera cam = Camera.main;
        Vector3 position = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        GameObject rain = Instantiate(_rainPrefab);
        rain.transform.position = position;
        rain.transform.position = new Vector3(0, rain.transform.position.y, rain.transform.position.z);
        _lastShowerAction = (ulong)System.DateTime.Now.Ticks;
        PlayerPrefs.SetString(LAST_SHOWER_KEY, _lastShowerAction.ToString());
        _titoMood.addMoodPoints(_showerPoints);
        _showerButton.interactable = false;
    }
    public void updateMoodBar()
    {
        _lastMoodUpdateAction = (ulong)System.DateTime.Now.Ticks;
        Debug.Log("LMUA: " + _lastMoodUpdateAction);
        PlayerPrefs.SetString(LAST_MOOD_UPDATE_KEY, _lastMoodUpdateAction.ToString());
        if (isPlayActionReady())
        {
            _titoMood.addMoodPoints(_moodPoints);
        }
        if (isShowerActionReady())
        {
            _titoMood.addMoodPoints(_moodPoints);
        }
        _isMoodUpdated = true;
    }
    #endregion

    #region timer check methods
    private bool isPlayActionReady()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - _lastPlayAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((_playSecondsToWait * 1000) - miliseconds) / 1000;

        if (secondsLeft < 0)
        {
            _playText.text = "";
            _playClock.gameObject.SetActive(false);
            return true;
        }
        else
        {
            _playClock.gameObject.SetActive(true);
            return false;
        }
    }
    private bool isEatActionReady()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - _lastEatAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((_eatSecondsToWait * 1000) - miliseconds) / 1000;

        if (secondsLeft < 0)
        {
            _eatText.text = "";
            _eatClock.gameObject.SetActive(false);
            return true;
        }
        else
        {
            _eatClock.gameObject.SetActive(true);
            return false;
        }
    }
    private bool isShowerActionReady()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - _lastShowerAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((_showerSecondsToWait * 1000) - miliseconds) / 1000;

        if (secondsLeft < 0)
        {
            _showerText.text = "";
            _showerClock.gameObject.SetActive(false);
            return true;
        }
        else
        {
            _showerClock.gameObject.SetActive(true);
            return false;
        }
    }
    private bool isMoodUpdateActionReady()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - _lastMoodUpdateAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((_moodUpdateSecondsToWait * 1000) - miliseconds) / 1000;

        if (secondsLeft < 0)
        {           
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    IEnumerator reactiveMoodUpdateBool()
    {
        yield return new WaitForSeconds(3f);
        _isMoodUpdated = false;
    }
}

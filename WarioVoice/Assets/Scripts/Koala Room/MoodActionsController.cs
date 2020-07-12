using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text _playText;
    [SerializeField] private Text _eatText;
    [SerializeField] private Text _showerText;

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


    public void restartPrefabs()
    {
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

            if (isPlayActionReady())
            {
                _playButton.interactable = true;
                _playText.text = "";
            }
        }

        if (!_eatButton.IsInteractable())
        {
            _eatText.text = getTimeLeft(ENUM_Actions.EAT);

            if (isEatActionReady())
            {
                _eatButton.interactable = true;
                _eatText.text = "";
            }
        }

        if (!_showerButton.IsInteractable())
        {
            _showerText.text = getTimeLeft(ENUM_Actions.SHOWER);

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
        //Hours
        timer += ((int)secondsLeft / 3600).ToString() + "H ";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        //minutes
        timer += ((int)secondsLeft / 60).ToString("00") + "M ";
        //seconds
        timer += (secondsLeft % 60).ToString("00") + "S";
        return timer;
    }

    #region button click actions
    public void playWithTito()
    {
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
            return true;
        }
        else
        {
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
            return true;
        }
        else
        {
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
            return true;
        }
        else
        {
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

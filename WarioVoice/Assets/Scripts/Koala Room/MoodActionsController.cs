using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodActionsController : MonoBehaviour
{
    [Header("Cooldown Times")]
    [SerializeField] private float _playSecondsToWait = 30;
    [SerializeField] private float _eatSecondsToWait = 30;
    [SerializeField] private float _showerSecondsToWait = 30;
    [Header("Mood Points")]
    [SerializeField] private float _playPoints = 15;
    [SerializeField] private float _foodPoints = 50;
    [SerializeField] private float _showerPoints = 5;
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

    private const string LAST_EAT_KEY = "LastEat";
    private const string LAST_PLAY_KEY = "LastPlay";
    private const string LAST_SHOWER_KEY = "LastShower";

    public enum ENUM_Actions
    {
        PLAY,
        SHOWER,
        EAT
    }

    private void Start()
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

        checkButtonsOnStart();
    }

    private void Update()
    {
        checkButtonsOnUpdate();
    }

    private void checkButtonsOnUpdate()
    {
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
        PlayerPrefs.SetString(LAST_PLAY_KEY,_lastPlayAction.ToString());
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
    #endregion

    #region timer check methods
    private bool isPlayActionReady()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - _lastPlayAction);
        ulong miliseconds = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)((_playSecondsToWait*1000) - miliseconds) / 1000;

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
    #endregion
}

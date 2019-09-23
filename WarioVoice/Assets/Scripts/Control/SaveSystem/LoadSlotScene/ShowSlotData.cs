using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShowSlotData : MonoBehaviour
{
    private const string PLAYEDTIME = "Played time: ";

    private const string STARTAGAME = "Start a game to record your input Accuracy.";

    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _playedTime;
    [SerializeField] private TextMeshProUGUI _inputAccuracy;
    [SerializeField] private List<Image> _imageBosses = new List<Image>();


    private int _defeatedBosses;
    private string _name;

    private void OnEnable()
    {
        _name = FindObjectOfType<FileManager>().PlayerInfSelected.playerName;
        _defeatedBosses = FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated;

        _nameTextUI.text = _name;

        foreach (var boss in _imageBosses)
        {
            boss.color = Color.black;
        }

        for (int i = 0; i < _defeatedBosses; i++)
        {
            _imageBosses[i].color = Color.white;
        }

        if (_playedTime != null)
        {
            setPlayedTime();
        }
        if (_inputAccuracy != null)
        {
            setAccuracy();
        }

    }

    private void setAccuracy()
    {
        float attemps = FindObjectOfType<FileManager>().PlayerInfSelected.microphonePressedTimes;
        float success = FindObjectOfType<FileManager>().PlayerInfSelected.microphonePressedTimesSuccesses;

        if (attemps > 0)
        {

            float accuracy = Mathf.RoundToInt((success / attemps) * 100);         

            _inputAccuracy.text = "Input Accuracy: " + accuracy + "%";
        }
        else
        {
            _inputAccuracy.text = STARTAGAME;

        }

    }

    private void setPlayedTime()
    {
        int playedTime = FindObjectOfType<FileManager>().PlayerInfSelected.playedTime;

        int seconds = (playedTime % 60);
        int minutes = (playedTime / 60) % 60;
        int hours = (playedTime / 3600) % 24;
        int days = (playedTime / 86400) % 365;

        if (days != 0)
        {
            _playedTime.text = PLAYEDTIME + days.ToString() + " Days, " + hours.ToString() + " Hours, " + minutes.ToString() + " Minutes, " + seconds.ToString() + " Seconds.";
        }
        else if (hours != 0)
        {
            _playedTime.text = PLAYEDTIME + hours.ToString() + " Hours, " + minutes.ToString() + " Minutes, " + seconds.ToString() + " Seconds.";

        }
        else if (minutes != 0)
        {
            _playedTime.text = PLAYEDTIME + minutes.ToString() + " Minutes, " + seconds.ToString() + " Seconds.";

        }
        else
        {
            _playedTime.text = PLAYEDTIME + seconds.ToString() + " Seconds.";

        }


    }

    public void DeleteConfirmation(bool confirmation)
    {
        if (confirmation)
        {
            string key = SaveSystem.PLAYERDATA_PLAYERPREFCODE + FindObjectOfType<FileManager>().PlayerInfSelected.slotNumber.ToString();
            PlayerPrefs.DeleteKey(key);
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.SLOTS));
        }
        else
        {
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.SHOWSLOT));
        }
    }

    public void ResetData()
    {
        //string key = SaveSystem.PLAYERDATA_PLAYERPREFCODE + FindObjectOfType<FileManager>().PlayerInfSelected.slotNumber.ToString();
        //PlayerPrefs.DeleteKey(key);
        StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.RESET));
        //FindObjectOfType<FileManager>().backToSlots();
    }

    public void continueGamePlay()
    {
        GameManager.GetInstance().CurrentPlayerInformation = FindObjectOfType<FileManager>().PlayerInfSelected;
        GameManager.GetInstance().Lives = 4;
        GameManager.GetInstance().StartGame();
    }
}

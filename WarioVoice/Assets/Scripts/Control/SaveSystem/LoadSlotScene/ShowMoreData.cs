﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowMoreData : MonoBehaviour
{
    private const string PLAYEDTIME = "";
    private const string STARTAGAME = "Start a game to record your Accuracy.";
    private const string PLAYED = "You have lost ";
    private const string TIMES1 = " time of ";
    private const string TIMES2 = " times played.";
    private const string NOPLAYED = "You have never played this game.";

#pragma warning disable CS0649 // El campo 'ShowMoreData._playedTime' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _playedTime;
#pragma warning restore CS0649 // El campo 'ShowMoreData._playedTime' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ShowMoreData._inputAccuracy' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _inputAccuracy;
#pragma warning restore CS0649 // El campo 'ShowMoreData._inputAccuracy' nunca se asigna y siempre tendrá el valor predeterminado null

#pragma warning disable CS0649 // El campo 'ShowMoreData.timesPlayedModernPaints' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesPlayedModernPaints;
#pragma warning restore CS0649 // El campo 'ShowMoreData.timesPlayedModernPaints' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesLossedModernPaints;

#pragma warning disable CS0649 // El campo 'ShowMoreData.timesPlayedOrchesta' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesPlayedOrchesta;
#pragma warning restore CS0649 // El campo 'ShowMoreData.timesPlayedOrchesta' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesLossedOrchesta;

#pragma warning disable CS0649 // El campo 'ShowMoreData.timesPlayedLoveGame' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesPlayedLoveGame;
#pragma warning restore CS0649 // El campo 'ShowMoreData.timesPlayedLoveGame' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesLossedLoveGame;

#pragma warning disable CS0649 // El campo 'ShowMoreData.timesPlayedWorms' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesPlayedWorms;
#pragma warning restore CS0649 // El campo 'ShowMoreData.timesPlayedWorms' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesLossedWorms;

#pragma warning disable CS0649 // El campo 'ShowMoreData.timesPlayeRPG' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesPlayeRPG;
#pragma warning restore CS0649 // El campo 'ShowMoreData.timesPlayeRPG' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI timesLossedRPG;

    private void OnEnable()
    {
        PlayerInformation playerInf = FindObjectOfType<FileManager>().PlayerInfSelected;

        timesPlayedModernPaints.text = (playerInf.timesPlayedModernPaints - playerInf.timesLossedModernPaints).ToString() + "/" + playerInf.timesPlayedModernPaints;
        timesPlayedOrchesta.text = (playerInf.timesPlayedOrchesta - playerInf.timesLossedOrchesta).ToString() + "/" + playerInf.timesPlayedOrchesta;
        timesPlayedLoveGame.text = (playerInf.timesPlayedLoveGame - playerInf.timesLossedLoveGame).ToString() + "/" + playerInf.timesPlayedLoveGame;
        timesPlayedWorms.text = (playerInf.timesPlayedWorms - playerInf.timesLossedWorms).ToString() + "/" + playerInf.timesPlayedWorms;
        timesPlayeRPG.text = (playerInf.timesPlayeRPG - playerInf.timesLossedRPG).ToString() + "/" + playerInf.timesPlayeRPG;

        /*
        timesPlayedModernPaints.text = (playerInf.timesPlayedModernPaints > 0) ? PLAYED + playerInf.timesLossedModernPaints.ToString() +
              TIMES1 + playerInf.timesPlayedModernPaints.ToString() + TIMES2 : NOPLAYED;

        timesPlayedOrchesta.text = (playerInf.timesPlayedOrchesta > 0) ? PLAYED + playerInf.timesLossedOrchesta.ToString() +
              TIMES1 + playerInf.timesPlayedOrchesta.ToString() + TIMES2 : NOPLAYED;

        timesPlayedLoveGame.text = (playerInf.timesPlayedLoveGame > 0) ? PLAYED + playerInf.timesLossedLoveGame.ToString() +
              TIMES1 + playerInf.timesPlayedLoveGame.ToString() + TIMES2 : NOPLAYED;

        timesPlayedWorms.text = (playerInf.timesPlayedWorms > 0) ? PLAYED + playerInf.timesLossedWorms.ToString() +
              TIMES1 + playerInf.timesPlayedWorms.ToString() + TIMES2 : NOPLAYED;

        timesPlayeRPG.text = (playerInf.timesPlayeRPG > 0) ? PLAYED + playerInf.timesLossedRPG.ToString() +
              TIMES1 + playerInf.timesPlayeRPG.ToString() + TIMES2 : NOPLAYED;
        */


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

            _inputAccuracy.text = accuracy + "%";
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

        string secondsString = (seconds >= 10) ? seconds.ToString() : "0" + seconds.ToString();
        string minutesString = (minutes >= 10) ? minutes.ToString() : "0" + minutes.ToString();
        string hoursString = (hours >= 10) ? hours.ToString() : "0" + hours.ToString();

        _playedTime.text =hoursString + ":" + minutesString + ":" + secondsString;

        /*

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

        }*/
    }

    public void showVocabulary()
    {
        StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.VOCABULARY));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowMoreData : MonoBehaviour
{
    private const string PLAYEDTIME = "";
    private const string STARTAGAME = "Start a game to record your input Accuracy.";
    private const string PLAYED = "You have lost ";
    private const string TIMES1 = " time of ";
    private const string TIMES2 = " times played.";
    private const string NOPLAYED = "You have never played this game.";

    [SerializeField] private TextMeshProUGUI _playedTime;
    [SerializeField] private TextMeshProUGUI _inputAccuracy;

    [SerializeField] private TextMeshProUGUI timesPlayedModernPaints;
    [SerializeField] private TextMeshProUGUI timesLossedModernPaints;

    [SerializeField] private TextMeshProUGUI timesPlayedOrchesta;
    [SerializeField] private TextMeshProUGUI timesLossedOrchesta;

    [SerializeField] private TextMeshProUGUI timesPlayedLoveGame;
    [SerializeField] private TextMeshProUGUI timesLossedLoveGame;

    [SerializeField] private TextMeshProUGUI timesPlayedWorms;
    [SerializeField] private TextMeshProUGUI timesLossedWorms;

    [SerializeField] private TextMeshProUGUI timesPlayeRPG;
    [SerializeField] private TextMeshProUGUI timesLossedRPG;

    private void OnEnable()
    {
        Debug.Log("Pinturas Veces jugadas: " + FindObjectOfType<FileManager>().PlayerInfSelected.timesPlayedModernPaints + " , Veces perdidas: " + FindObjectOfType<FileManager>().PlayerInfSelected.timesLossedModernPaints);

        PlayerInformation playerInf = FindObjectOfType<FileManager>().PlayerInfSelected;



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
}

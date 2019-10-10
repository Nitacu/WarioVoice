using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShowSlotData : MonoBehaviour
{
    public const string CONTINUE = "Continue";
    public const string STARTGAME = "Start Game";
    public const string DEFEATED_BOSSES = "Defetated bosses | Jefes derrotados";


    //Total played time: 

    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _buttonTextContinue;
    [SerializeField] private TextMeshProUGUI _defeatedBossesText;


    [SerializeField] private List<Image> _imageBosses = new List<Image>();

    [Header("Boss Icons")]
    [SerializeField] private Sprite _defeatedIcon;
    [SerializeField] private Sprite _undefetaedIcon;


    private int _defeatedBosses;
    private string _name;

    private void OnEnable()
    {
        if (_buttonTextContinue != null)
        {
            _buttonTextContinue.text = (FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated > 0) ? CONTINUE : STARTGAME;

        }

        _name = FindObjectOfType<FileManager>().PlayerInfSelected.playerName;
        _defeatedBosses = FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated;

        _nameTextUI.text = _name;

        foreach (var boss in _imageBosses)
        {
            //boss.color = Color.black;
            boss.GetComponent<Image>().sprite = _undefetaedIcon;
        }

        for (int i = 0; i < _defeatedBosses; i++)
        {
            //_imageBosses[i].color = Color.white;
            _imageBosses[i].GetComponent<Image>().sprite = _defeatedIcon;

        }

        _defeatedBossesText.text = DEFEATED_BOSSES + "\n" + _defeatedBosses.ToString() + "/" + GameManager.maxBosses.ToString();
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

        if (FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated > 0)
        {          
            GameManager.GetInstance().StartGame();
        }
        else
        {
            GetComponent<ChangeScene>().chanceScene();
        }        
    }

    public void showMoreData()
    {
        StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.MOREDATA));
    }
}

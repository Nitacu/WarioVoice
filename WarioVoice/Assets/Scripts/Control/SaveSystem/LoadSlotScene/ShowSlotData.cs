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
    public const string GAMECOMPLETED = "Game completed";
    public const string DEFEATED_BOSSES = "Defetated bosses | Jefes derrotados";


    //Total played time: 

#pragma warning disable CS0649 // El campo 'ShowSlotData._nameTextUI' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _nameTextUI;
#pragma warning restore CS0649 // El campo 'ShowSlotData._nameTextUI' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ShowSlotData._buttonTextContinue' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _buttonTextContinue;
#pragma warning restore CS0649 // El campo 'ShowSlotData._buttonTextContinue' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ShowSlotData._defeatedBossesText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _defeatedBossesText;
#pragma warning restore CS0649 // El campo 'ShowSlotData._defeatedBossesText' nunca se asigna y siempre tendrá el valor predeterminado null


    [SerializeField] private List<Image> _imageBosses = new List<Image>();

    [Header("Boss Icons")]
#pragma warning disable CS0649 // El campo 'ShowSlotData._defeatedIcon' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _defeatedIcon;
#pragma warning restore CS0649 // El campo 'ShowSlotData._defeatedIcon' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ShowSlotData._undefetaedIcon' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _undefetaedIcon;
#pragma warning restore CS0649 // El campo 'ShowSlotData._undefetaedIcon' nunca se asigna y siempre tendrá el valor predeterminado null


    private int _defeatedBosses;
    private string _name;

    private void OnEnable()
    {
        if (_buttonTextContinue != null)
        {
            _buttonTextContinue.text = (FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated > 0) ? CONTINUE : STARTGAME;

            if (FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated >= GameManager.maxBosses)
            {
                _buttonTextContinue.text = GAMECOMPLETED;
            }
        }

        _name = FindObjectOfType<FileManager>().PlayerInfSelected.playerName;
        _defeatedBosses = FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated;

        _nameTextUI.text = _name;

        if (_imageBosses.Count >0)
        {
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
        }      

        if (_defeatedBossesText != null)
        {
            _defeatedBossesText.text = DEFEATED_BOSSES + "\n" + _defeatedBosses.ToString() + "/" + GameManager.maxBosses.ToString();
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
        ControlMoney._earnedMoney = false;

        if (FindObjectOfType<FileManager>().PlayerInfSelected.bossesDefeated >= GameManager.maxBosses)
        {
            return;
        }

        GameManager.GetInstance().CurrentPlayerInformation = FindObjectOfType<FileManager>().PlayerInfSelected;
        GameManager.GetInstance().Lives = GameManager.GetInstance().maxNumberOfLives;

        Debug.Log("numero de vidas " + GameManager.GetInstance().maxNumberOfLives);
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

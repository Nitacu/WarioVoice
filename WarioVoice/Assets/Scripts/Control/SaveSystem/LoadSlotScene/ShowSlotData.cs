using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowSlotData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTextUI;
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

    }

    public void ResetData()
    {
        string key = SaveSystem.PLAYERDATA_PLAYERPREFCODE + FindObjectOfType<FileManager>().PlayerInfSelected.slotNumber.ToString();
        PlayerPrefs.DeleteKey(key);
        FindObjectOfType<FileManager>().backToSlots();
    }

    public void continueGamePlay()
    {
        GameManager.GetInstance().CurrentPlayerInformation = FindObjectOfType<FileManager>().PlayerInfSelected;
        GameManager.GetInstance().StartGame();
    }
}

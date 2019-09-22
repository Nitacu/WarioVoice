using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class FileSlot : MonoBehaviour, IPointerDownHandler
{

    private bool hasData;

    [SerializeField] private int _codeFile;

    [SerializeField] private TextMeshProUGUI _name;

    private PlayerInformation _currentplayerInformation;

    private void OnEnable()
    {


        string key = SaveSystem.PLAYERDATA_PLAYERPREFCODE + _codeFile.ToString();

        if (PlayerPrefs.HasKey(key))
        {

            string data = PlayerPrefs.GetString(key);
            _currentplayerInformation = JsonUtility.FromJson<PlayerInformation>(data);

            _name.text = _currentplayerInformation.playerName;
            hasData = true;

        }
        else
        {
            hasData = false;
            _name.text = "Empty";
        }
    }

    private void Start()
    {
        


        /*PlayerData playerData = new PlayerData();
        playerData.slotID = _codeFile;
        playerData.name = "nombre";
        playerData.bossesDefeated = 0;

        string json = JsonUtility.ToJson(playerData);

        string fileName = "/PlayerDataFile" + playerData.slotID.ToString() + ".json";
        string path = Application.dataPath;

        System.IO.File.WriteAllText(path + fileName , json);*/

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasData)
        {
            FindObjectOfType<FileManager>().CurrentSlotSelected = _codeFile;
            FindObjectOfType<FileManager>().PlayerInfSelected = _currentplayerInformation;
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.SHOWSLOT));
            //FindObjectOfType<FileManager>().showMore(_currentplayerInformation, _currentplayerInformation.slotNumber);
        }
        else
        {
            FindObjectOfType<FileManager>().CurrentSlotSelected = _codeFile;
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.CREATESLOT));
            //FindObjectOfType<FileManager>().showMore(null, _codeFile);
        }

    }


}

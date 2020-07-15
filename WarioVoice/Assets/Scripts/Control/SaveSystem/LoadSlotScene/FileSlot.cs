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

#pragma warning disable CS0649 // El campo 'FileSlot._codeFile' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private int _codeFile;
#pragma warning restore CS0649 // El campo 'FileSlot._codeFile' nunca se asigna y siempre tendrá el valor predeterminado 0

#pragma warning disable CS0649 // El campo 'FileSlot._name' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _name;
#pragma warning restore CS0649 // El campo 'FileSlot._name' nunca se asigna y siempre tendrá el valor predeterminado null

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

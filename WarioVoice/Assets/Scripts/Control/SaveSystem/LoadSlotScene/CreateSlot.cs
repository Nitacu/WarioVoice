using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateSlot : MonoBehaviour
{
    private const string FILE = "Slot ";
    private const string PLACEHOLDER = "Enter name...";


    [SerializeField] TextMeshProUGUI _fileNumber;
    [SerializeField] TMP_InputField _inputfield;

    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _namePlaceHolder;

    [SerializeField] GameObject _warningIcon;


    private void OnEnable()
    {
        _namePlaceHolder.text = PLACEHOLDER;      
        _inputfield.Select();
        _inputfield.text = "";
        _fileNumber.text = FILE + FindObjectOfType<FileManager>().CurrentSlotSelected.ToString();
    }

    private void Update()
    {
        if (_inputfield.text.Length != 0)
        {
            _warningIcon.SetActive(false);
        }
    }

    public void createSlot()
    {
       
        if (_inputfield.text.Length != 0)
        {
            FindObjectOfType<FileManager>().createNewSlot(_inputfield.text);
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.SHOWSLOT));
        }
        else
        {
            _warningIcon.SetActive(true);
        }

       

    }
}

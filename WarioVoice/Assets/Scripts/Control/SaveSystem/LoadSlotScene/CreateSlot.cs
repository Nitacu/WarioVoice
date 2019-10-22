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
    [SerializeField] GameObject _warningText;


    private void OnEnable()
    {
        _warningText.SetActive(false);
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

        if (!(_inputfield.text.Length < 10))
        {
            _warningText.SetActive(true);
        }
        else
        {
            _warningText.SetActive(false);
        }
    }

    public void createSlot()
    {
       
        if (_inputfield.text.Length > 0 && _inputfield.text.Length < 10)
        {
            FindObjectOfType<FileManager>().createNewSlot(_inputfield.text);
            StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.SHOWSLOT));
        }
        else
        {
            if (!(_inputfield.text.Length > 0))
            {
                _warningIcon.SetActive(true);
            }

            if (!(_inputfield.text.Length < 10))
            {
                _warningText.SetActive(true);
            }   
        }

       

    }
}

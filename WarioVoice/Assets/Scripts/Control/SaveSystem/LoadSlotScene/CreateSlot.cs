using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateSlot : MonoBehaviour
{
    private const string FILE = "File ";
    private const string PLACEHOLDER = "Enter name...";


    [SerializeField] TextMeshProUGUI _fileNumber;
    [SerializeField] TMP_InputField _inputfield;

    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _namePlaceHolder;


    private void OnEnable()
    {
        _namePlaceHolder.text = PLACEHOLDER;      
        _inputfield.Select();
        _name.text = "";
        _fileNumber.text = FILE + FindObjectOfType<FileManager>().CurrentSlotSelected.ToString();
    }

    public void createSlot()
    {
        FindObjectOfType<FileManager>().createNewSlot(_name.text);
    }
}

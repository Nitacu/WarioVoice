using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateSlot : MonoBehaviour
{
    private const string FILE = "Slot ";
    private const string PLACEHOLDER = "Enter name...";


#pragma warning disable CS0649 // El campo 'CreateSlot._fileNumber' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] TextMeshProUGUI _fileNumber;
#pragma warning restore CS0649 // El campo 'CreateSlot._fileNumber' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'CreateSlot._inputfield' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] TMP_InputField _inputfield;
#pragma warning restore CS0649 // El campo 'CreateSlot._inputfield' nunca se asigna y siempre tendrá el valor predeterminado null

    [SerializeField] TextMeshProUGUI _name;
#pragma warning disable CS0649 // El campo 'CreateSlot._namePlaceHolder' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] TextMeshProUGUI _namePlaceHolder;
#pragma warning restore CS0649 // El campo 'CreateSlot._namePlaceHolder' nunca se asigna y siempre tendrá el valor predeterminado null

#pragma warning disable CS0649 // El campo 'CreateSlot._warningIcon' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] GameObject _warningIcon;
#pragma warning restore CS0649 // El campo 'CreateSlot._warningIcon' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'CreateSlot._warningText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] GameObject _warningText;
#pragma warning restore CS0649 // El campo 'CreateSlot._warningText' nunca se asigna y siempre tendrá el valor predeterminado null


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

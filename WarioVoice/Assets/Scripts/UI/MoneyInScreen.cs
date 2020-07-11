using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _text.text = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_MONEY).ToString();    
    }
}

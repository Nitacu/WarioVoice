using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInScreen : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'MoneyInScreen._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // El campo 'MoneyInScreen._text' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'MoneyInScreen._leafs' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _leafs;
#pragma warning restore CS0649 // El campo 'MoneyInScreen._leafs' nunca se asigna y siempre tendrá el valor predeterminado null

    private void OnEnable()
    {
        if (ControlMoney._earnedMoney)
        {
            _text.text = (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_MONEY) - ControlMoney._lastMoney).ToString();
            StartCoroutine(feeckBackEarnsLeafs());
        }
        else
            _text.text = (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_MONEY)).ToString();
    }

    IEnumerator feeckBackEarnsLeafs()
    {
        Instantiate(_leafs, Camera.main.ScreenToWorldPoint(_text.transform.position), Quaternion.identity);
        yield return new WaitForSeconds(1);
        _text.text = (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_MONEY)).ToString();
    }

}

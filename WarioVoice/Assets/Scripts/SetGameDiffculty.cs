using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetGameDiffculty : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'SetGameDiffculty._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _text;
#pragma warning restore CS0649 // El campo 'SetGameDiffculty._text' nunca se asigna y siempre tendrá el valor predeterminado null

    public void setGameDifficulty()
    {
        GameManager.GetInstance().setGameDifficulty(System.Int32.Parse(_text.text));
    }
}

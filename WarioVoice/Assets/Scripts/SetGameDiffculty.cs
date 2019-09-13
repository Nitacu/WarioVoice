using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetGameDiffculty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void setGameDifficulty()
    {
        GameManager.GetInstance().setGameDifficulty(System.Int32.Parse(_text.text));
    }
}

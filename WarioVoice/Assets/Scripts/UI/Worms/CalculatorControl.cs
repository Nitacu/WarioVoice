using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CalculatorControl : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'CalculatorControl._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // El campo 'CalculatorControl._text' nunca se asigna y siempre tendrá el valor predeterminado null
    
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void appearCalculator()
    {
        _anim.Play(Animator.StringToHash("Appear"));
    }

    public void desappearCalculator()
    {
        _anim.Play(Animator.StringToHash("Desappear"));
    }

    public void addNumber(int number)
    {
        if (_text.text.Length < 3)
            _text.text += number.ToString();
    }

    public void addSymbol(string symbol)
    {
        if (_text.text.Length < 4)
            _text.text += symbol;
    }

    public void clearLastLetter()
    {
        if (_text.text.Length > 0)
            _text.text = _text.text.Remove(_text.text.Length - 1);
    }

    public void clearText()
    {
        _text.text = "";
    }
}

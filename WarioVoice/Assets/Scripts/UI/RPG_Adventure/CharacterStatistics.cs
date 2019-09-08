using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField] private TMP_Text _life;
    [SerializeField] private Image _icon;

    public Image Icon { get => _icon; set => _icon = value; }

    public void reloadStatistics(float life)
    {
        _life.text = life.ToString();

    }

    public void changeColor(Color color)
    {
        _life.color = color;

        Invoke("restColor",3);
    }

    public void restColor()
    {
        changeColor(Color.white);
    }

}

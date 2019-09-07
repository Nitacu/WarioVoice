using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField] private TMP_Text _life;
    [SerializeField] private TMP_Text _mana;


    public void reloadStatistics(float life, float mana)
    {
        _life.text = life.ToString();
        _mana.text = mana.ToString();
    }

    public void changeColor(Color color)
    {
        _life.color = color;
        _mana.color = color;
        Invoke("restColor",3);
    }

    public void restColor()
    {
        changeColor(Color.white);
    }

}

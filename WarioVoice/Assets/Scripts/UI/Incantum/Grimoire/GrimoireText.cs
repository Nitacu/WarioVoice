using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrimoireText : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMP_Text;


    public void showMessange(CommandParser.enchantmentResponse enchantmentResponse)
    {
        Debug.Log(enchantmentResponse.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualDamage : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'VisualDamage._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // El campo 'VisualDamage._text' nunca se asigna y siempre tendrá el valor predeterminado null


    public void desactiveSelf()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("desactiveSelf", 1);
    }

    private void OnDisable()
    {
        _text.text = "";
    }
}

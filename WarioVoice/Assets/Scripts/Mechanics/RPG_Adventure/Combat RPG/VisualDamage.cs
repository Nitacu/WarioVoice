using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void showDamage(float damage)
    {
        _text.text = damage.ToString();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToDeveloperMode : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'ChangeToDeveloperMode._toggle' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Toggle _toggle;
#pragma warning restore CS0649 // El campo 'ChangeToDeveloperMode._toggle' nunca se asigna y siempre tendrá el valor predeterminado null

    private void Start()
    {
        GameManager.ResetInstance();
        _toggle.isOn = GameManager.GetInstance().DeveloperMode;
        GameManager.GetInstance().DeveloperMode = _toggle.isOn;
    }

    public void changeDeveloperMode(bool isOn)
    {
        
        GameManager.GetInstance().DeveloperMode = isOn;

    }
}

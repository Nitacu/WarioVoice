using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToDeveloperMode : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.isOn = GameManager.GetInstance().DeveloperMode;
        //GameManager.GetInstance().DeveloperMode = _toggle.isOn;
    }

    public void changeDeveloperMode(bool isOn)
    {
        GameManager.GetInstance().DeveloperMode = isOn;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToDeveloperMode : MonoBehaviour
{
    public void changeDeveloperMode(bool isOn)
    {
        GameManager.GetInstance().DeveloperMode = isOn;

    }
}

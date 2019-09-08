using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveMeterController : MonoBehaviour
{
    private Image loveBar;
    public float numberOfSigns = 0;
    public float contSigns = 0;

    private void Start()
    {
        loveBar = GetComponent<Image>();
        loveBar.fillAmount = 0;
    }

    public void updateLoveBar()
    {
        contSigns++;
        if (contSigns < numberOfSigns)
        {
            loveBar.fillAmount = contSigns / numberOfSigns;
            
        }
        else
        {
            loveBar.fillAmount = 1;
            Debug.Log("Entro al que no es");
        }
    }
}

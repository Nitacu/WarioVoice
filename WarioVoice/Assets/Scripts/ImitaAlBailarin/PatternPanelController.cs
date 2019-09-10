using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPanelController : MonoBehaviour
{
    public GameObject crystalPanel;

    public void patternCreator(Crystal[] crystals)
    {
        int cont = 0;

        foreach (Crystal crystal in crystals)
        {
            Instantiate(crystalPanel, gameObject.transform);
        }

        foreach (Transform crystal in gameObject.transform)
        { 
            crystal.gameObject.GetComponent<CrystalController>().changeCrystalColor(crystals[cont]);
            cont++;
        }


    }
}

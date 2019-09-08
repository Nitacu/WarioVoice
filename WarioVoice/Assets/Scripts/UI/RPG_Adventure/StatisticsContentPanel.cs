using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsContentPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listCharacterStadistics;

    public GameObject activePanel()
    {
        foreach (GameObject gm in _listCharacterStadistics)
        {
            if (!gm.active)
            {
                gm.SetActive(true);
                return gm;
            }
        }

        return null;
    }

}

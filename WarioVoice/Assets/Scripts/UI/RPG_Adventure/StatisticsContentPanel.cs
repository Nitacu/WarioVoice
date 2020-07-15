using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsContentPanel : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'StatisticsContentPanel._listCharacterStadistics' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private List<GameObject> _listCharacterStadistics;
#pragma warning restore CS0649 // El campo 'StatisticsContentPanel._listCharacterStadistics' nunca se asigna y siempre tendrá el valor predeterminado null

    public GameObject activePanel()
    {
        foreach (GameObject gm in _listCharacterStadistics)
        {
#pragma warning disable CS0618 // 'GameObject.active' está obsoleto: 'GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.'
            if (!gm.active)
#pragma warning restore CS0618 // 'GameObject.active' está obsoleto: 'GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.'
            {
                gm.SetActive(true);
                return gm;
            }
        }

        return null;
    }

}

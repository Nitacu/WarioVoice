using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApperarGrimoire : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'ApperarGrimoire._grimoire' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _grimoire;
#pragma warning restore CS0649 // El campo 'ApperarGrimoire._grimoire' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ApperarGrimoire._openBook' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _openBook;
#pragma warning restore CS0649 // El campo 'ApperarGrimoire._openBook' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'ApperarGrimoire._closeBook' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _closeBook;
#pragma warning restore CS0649 // El campo 'ApperarGrimoire._closeBook' nunca se asigna y siempre tendrá el valor predeterminado null


    public void appearGrimoire()
    {
#pragma warning disable CS0618 // 'GameObject.active' está obsoleto: 'GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.'
        if (_grimoire.active)
#pragma warning restore CS0618 // 'GameObject.active' está obsoleto: 'GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.'
        {
            _grimoire.SetActive(false);
            GetComponent<Image>().sprite = _closeBook;
        }
        else
        {
            _grimoire.SetActive(true);
            GetComponent<Image>().sprite = _openBook;
        }
    }

}

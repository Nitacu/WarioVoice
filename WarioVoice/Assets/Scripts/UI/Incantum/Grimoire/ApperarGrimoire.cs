using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApperarGrimoire : MonoBehaviour
{
    [SerializeField] private GameObject _grimoire;
    [SerializeField] private Sprite _openBook;
    [SerializeField] private Sprite _closeBook;


    public void appearGrimoire()
    {
        if (_grimoire.active)
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

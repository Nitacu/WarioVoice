using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VocabularyData : MonoBehaviour
{


    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI _loveWords;
    [SerializeField] private TextMeshProUGUI _paintWords;
    [SerializeField] private TextMeshProUGUI _orquestsWords;
    [SerializeField] private TextMeshProUGUI _wormsWords;
    [SerializeField] private TextMeshProUGUI _bossWords;


    private void OnEnable()
    {
        resetWords();

        PlayerInformation playerInf = FindObjectOfType<FileManager>().PlayerInfSelected;

        //LOVE WORDS
        for (int i = 0; i < playerInf._pronouncedWordsLove.Count; i++)
        {
            if (i!=0)
            {
                _loveWords.text += (", " + playerInf._pronouncedWordsLove[i]);
            }
            else
            {
                _loveWords.text += playerInf._pronouncedWordsLove[i];
            }
        }

        //PAINT WORDS
        for (int i = 0; i < playerInf._pronouncedWordsPaint.Count; i++)
        {
            if (i != 0)
            {
                _paintWords.text += (", " + playerInf._pronouncedWordsPaint[i]);
            }
            else
            {
                _paintWords.text += playerInf._pronouncedWordsPaint[i];
            }
        }

        //ORQUESTA WORDS
        for (int i = 0; i < playerInf._pronouncedWordsOrchesta.Count; i++)
        {
            if (i != 0)
            {
                _orquestsWords.text += (", " + playerInf._pronouncedWordsOrchesta[i]);
            }
            else
            {
                _orquestsWords.text += playerInf._pronouncedWordsOrchesta[i];
            }
        }

        //WORMS WORDS
        for (int i = 0; i < playerInf._pronouncedWordsWorms.Count; i++)
        {
            if (i != 0)
            {
                _wormsWords.text += (", " + playerInf._pronouncedWordsWorms[i]);
            }
            else
            {
                _wormsWords.text += playerInf._pronouncedWordsWorms[i];
            }
        }

        //BOSS WORDS
        for (int i = 0; i < playerInf._pronouncedWordsBoss.Count; i++)
        {
            if (i != 0)
            {
                _bossWords.text += (", " + playerInf._pronouncedWordsBoss[i]);
            }
            else
            {
                _bossWords.text += playerInf._pronouncedWordsBoss[i];
            }
        }
    }

    public void resetWords()
    {
        _loveWords.text = "";
        _paintWords.text = "";
        _orquestsWords.text = "";    
        _wormsWords.text = "";
        _bossWords.text = "";
    }

    public void backToShowMore()
    {
        StartCoroutine(FindObjectOfType<FileManager>().showMenu(FileManager.Menus.MOREDATA));
    }
}

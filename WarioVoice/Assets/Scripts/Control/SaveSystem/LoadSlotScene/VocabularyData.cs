using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class VocabularyData : MonoBehaviour
{
    private const string NOWORDS = "NO WORDS";

    [Header("UI Texts")]
#pragma warning disable CS0649 // El campo 'VocabularyData._loveWords' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _loveWords;
#pragma warning restore CS0649 // El campo 'VocabularyData._loveWords' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._questionmarkLove' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _questionmarkLove;
#pragma warning restore CS0649 // El campo 'VocabularyData._questionmarkLove' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._paintWords' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _paintWords;
#pragma warning restore CS0649 // El campo 'VocabularyData._paintWords' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._questionmarkpaint' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _questionmarkpaint;
#pragma warning restore CS0649 // El campo 'VocabularyData._questionmarkpaint' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._orquestsWords' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _orquestsWords;
#pragma warning restore CS0649 // El campo 'VocabularyData._orquestsWords' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._questionmarkOrquesta' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _questionmarkOrquesta;
#pragma warning restore CS0649 // El campo 'VocabularyData._questionmarkOrquesta' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._wormsWords' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _wormsWords;
#pragma warning restore CS0649 // El campo 'VocabularyData._wormsWords' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._questionmarkWorms' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _questionmarkWorms;
#pragma warning restore CS0649 // El campo 'VocabularyData._questionmarkWorms' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._bossWords' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _bossWords;
#pragma warning restore CS0649 // El campo 'VocabularyData._bossWords' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'VocabularyData._questionmarkBoss' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _questionmarkBoss;
#pragma warning restore CS0649 // El campo 'VocabularyData._questionmarkBoss' nunca se asigna y siempre tendrá el valor predeterminado null

    private List<string> sortListAlphabetic(List<string> _list)
    {
        _list = _list.OrderBy(o => o).ToList();
        return _list;
    }

    private void OnEnable()
    {
        resetWords();

        PlayerInformation playerInf = FindObjectOfType<FileManager>().PlayerInfSelected;

        //LOVE WORDS
        setText(playerInf._pronouncedWordsLove, _loveWords, _questionmarkLove);

        //PAINT WORDS
        setText(playerInf._pronouncedWordsPaint, _paintWords, _questionmarkpaint);

        //ORQUESTA WORDS
        setText(playerInf._pronouncedWordsOrchesta, _orquestsWords, _questionmarkOrquesta);

        //WORMS WORDS
        setText(playerInf._pronouncedWordsWorms, _wormsWords, _questionmarkWorms);

        //BOSS WORDS
        setText(playerInf._pronouncedWordsBoss, _bossWords, _questionmarkBoss);

    }

    public void setText(List<string> wordsList, TextMeshProUGUI textUI, GameObject questionMark)
    {
        if (wordsList.Count > 0)
        {
            questionMark.SetActive(false);
            List<string> _wordsAux = new List<string>();
            _wordsAux = sortListAlphabetic(wordsList);

            for (int i = 0; i < _wordsAux.Count; i++)
            {
                textUI.text += _wordsAux[i] + "\n";
                /*
                if (i != 0)
                {
                    textUI.text += ("\n" + _wordsAux[i]);
                }
                else
                {
                    textUI.text += _wordsAux[i];
                }*/
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

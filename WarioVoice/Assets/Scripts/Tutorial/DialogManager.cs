using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public Queue<string> sentences = new Queue<string>();
#pragma warning disable CS0649 // El campo 'DialogManager._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // El campo 'DialogManager._text' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'DialogManager._dialogue' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Dialogue _dialogue;
#pragma warning restore CS0649 // El campo 'DialogManager._dialogue' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'DialogManager._buttonNextDialogue' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _buttonNextDialogue;
#pragma warning restore CS0649 // El campo 'DialogManager._buttonNextDialogue' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'DialogManager._buttonSound' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _buttonSound;
#pragma warning restore CS0649 // El campo 'DialogManager._buttonSound' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private List<bool> _showButtonNextDialogue = new List<bool>();
    [SerializeField] private List<AudioClip> _sentenses = new List<AudioClip>();
    [SerializeField] private List<bool> _showButtonSentenses= new List<bool>();
#pragma warning disable CS0649 // El campo 'DialogManager._destroyInstruccion' nunca se asigna y siempre tendrá el valor predeterminado false
    [SerializeField] private bool _destroyInstruccion;
#pragma warning restore CS0649 // El campo 'DialogManager._destroyInstruccion' nunca se asigna y siempre tendrá el valor predeterminado false
#pragma warning disable CS0649 // El campo 'DialogManager._instruccion' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _instruccion;
#pragma warning restore CS0649 // El campo 'DialogManager._instruccion' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'DialogManager._enemy' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _enemy;
#pragma warning restore CS0649 // El campo 'DialogManager._enemy' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'DialogManager._faceObj' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _faceObj;
#pragma warning restore CS0649 // El campo 'DialogManager._faceObj' nunca se asigna y siempre tendrá el valor predeterminado null

    private int _cont = -1;
    private int _numerdialogue = 0;

    private void Start()
    {
        _numerdialogue = _dialogue.sentences.Length;
        StartDialogue();
    }

    public void StartDialogue()
    {
        sentences.Clear();

        foreach (string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_destroyInstruccion && _cont == _numerdialogue-1)
        {
            Destroy(_instruccion);
            return;
        }
        if (_enemy != null)
        {
            if (_cont == _numerdialogue - 2)
            {
                _faceObj.GetComponent<Image>().sprite = _enemy;
            }
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        _cont++;
        showButtons();
        StartCoroutine(TypeSentence(sentence));

    }

    private void showButtons()
    {
        if (_showButtonNextDialogue.Count > 0)
        {
            if (_showButtonNextDialogue[_cont])
            {
                _buttonNextDialogue.SetActive(true);
            }
            else
            {
                _buttonNextDialogue.SetActive(false);
            }
        }

        if (_showButtonSentenses.Count > 0)
        {
            if (_showButtonSentenses[_cont])
            {
                _buttonSound.SetActive(true);
                _buttonSound.GetComponent<AudioSource>().clip = _sentenses[0];
                _sentenses.RemoveAt(0);
            }
            else
            {
                _buttonSound.SetActive(false);
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _text.text += letter;
            yield return null;
        }
    }

}

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
}

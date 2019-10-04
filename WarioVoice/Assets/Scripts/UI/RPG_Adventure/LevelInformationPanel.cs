using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class LevelInformationPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _attacks;
    [SerializeField] private List<GameObject> _images;
    [SerializeField] private GameObject _panelAttacks;
    [SerializeField] private GameObject _fatherText;
    [SerializeField] private TMP_Text _newText;
    [SerializeField] private List<TMP_Text> _listTexts = new List<TMP_Text>();
    private int _index = 0;
    private string _currentDialogue;
    private ControlShifts _controlShifts;

    private void Start()
    {
        ControlShifts = FindObjectOfType<ControlShifts>();
    }

    // true cuando el plaeyr tenga que hablar false para cuando este pasando cosas dentro del juego
    public void showDialogs(string puzzle , bool state = false)
    {
        FindObjectOfType<SetActiveSpeechButton>().setButton(state);
        _panelAttacks.SetActive(state);

        if (_listTexts.Count <= 3)
        {
            _listTexts.Add(Instantiate(_newText, _fatherText.transform));
            _listTexts[_listTexts.Count - 1].text = puzzle;
        }

        if (_listTexts.Count > 1)
        {
            _listTexts[_listTexts.Count - 1].gameObject.SetActive(false);
            _index = 1;
            Invoke("showDialog", 0.5f);
        }
        else
        {
            _listTexts[0].gameObject.SetActive(true);
        }
    }

    public void showDialog()
    {
        if (_index == 0)
        {
            _listTexts[1].gameObject.SetActive(true);
        }
        else
        {
            if (_listTexts.Count == 2)
            {
                _listTexts[0].GetComponent<Animator>().Play(Animator.StringToHash("Old_text"));
                _index = 0;
                Invoke("showDialog", 0.5f);
            }

            if (_listTexts.Count >= 3)
            {
                _listTexts[0].GetComponent<Animator>().Play(Animator.StringToHash("Delete_text"));
                _listTexts[1].GetComponent<Animator>().Play(Animator.StringToHash("Old_text"));
                _listTexts.RemoveAt(0);
                _index = 0;
                Invoke("showDialog", 0.5f);
            }
        }
    }

    public void pronunciationAttack(GameObject gameObject)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public List<GameObject> Attacks { get => _attacks; set => _attacks = value; }
    public ControlShifts ControlShifts { get => _controlShifts; set => _controlShifts = value; }
    public List<GameObject> Images { get => _images; set => _images = value; }
}

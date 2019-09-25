using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInformationPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _attacks;
    [SerializeField] private GameObject _panelAttacks;
    [SerializeField] private TMP_Text _puzzle;
    private string _riddle;
    private string _currentDialogue;
    private ControlShifts _controlShifts;
    private float _time = 0;
    private bool _allowReloadRiddle = false;

    private void Start()
    {
        ControlShifts = FindObjectOfType<ControlShifts>();
    }

    public void activeDialogue(string text)
    {
        _allowReloadRiddle = false;
        FindObjectOfType<SetActiveSpeechButton>().setButton(false);
        _panelAttacks.SetActive(false);
        _puzzle.text = text;
    }

    public void reloadTime()
    {
        _time = 0;
    }

    private void Update()
    {
        if (_allowReloadRiddle)
        {
            _time += Time.deltaTime;

            if (_time >= 3)
            {
                reloadRiddle();
            }
        }

    }

    public void activePanelAttacks(string puzzle)
    {
        _allowReloadRiddle = true;
        _riddle = puzzle;
        FindObjectOfType<SetActiveSpeechButton>().setButton(true);
        _panelAttacks.SetActive(true);
        _puzzle.text = puzzle;
    }

    public void reloadRiddle()
    {
        if (!string.Equals(_puzzle.text, _riddle))
        {
            _puzzle.text = _riddle;   
        }
        _time = 0;
    }

    public void pronunciationAttack(GameObject gameObject)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public List<GameObject> Attacks { get => _attacks; set => _attacks = value; }
    public ControlShifts ControlShifts { get => _controlShifts; set => _controlShifts = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInformationPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _attacks;
    [SerializeField] private List<GameObject> _images;
    [SerializeField] private GameObject _panelAttacks;
    [SerializeField] private TMP_Text _puzzle;
    private string _riddle;
    private string _currentDialogue;
    private ControlShifts _controlShifts;

    private void Start()
    {
        ControlShifts = FindObjectOfType<ControlShifts>();
    }

    public void activeDialogue(string text)
    {
        FindObjectOfType<SetActiveSpeechButton>().setButton(false);
        _panelAttacks.SetActive(false);
        _puzzle.text = text;
    }


    public void activePanelAttacks(string puzzle)
    {
        _riddle = puzzle;
        FindObjectOfType<SetActiveSpeechButton>().setButton(true);
        _panelAttacks.SetActive(true);
        _puzzle.text = puzzle;
    }

    public void pronunciationAttack(GameObject gameObject)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public List<GameObject> Attacks { get => _attacks; set => _attacks = value; }
    public ControlShifts ControlShifts { get => _controlShifts; set => _controlShifts = value; }
    public List<GameObject> Images { get => _images; set => _images = value; }
}

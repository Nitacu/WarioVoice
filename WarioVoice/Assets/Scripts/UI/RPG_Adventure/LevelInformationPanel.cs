using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInformationPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _attacks;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _panelAttacks;
    private ControlShifts _controlShifts;

    private void Start()
    {
        ControlShifts = FindObjectOfType<ControlShifts>();
    }

    public void activeDialogue(string text)
    {
        if (ControlShifts.TurnEnemy)
        {
            _panelAttacks.SetActive(false);
            _text.SetActive(true);
            _text.GetComponent<TMP_Text>().text = text;
        }

    }

    public void activePanelAttacks()
    {
        if (ControlShifts.TurnPlayer)
        {
            _panelAttacks.SetActive(true);
            _text.SetActive(false);
        }

    }

    public void pronunciationAttack(GameObject gameObject)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public List<GameObject> Attacks { get => _attacks; set => _attacks = value; }
    public ControlShifts ControlShifts { get => _controlShifts; set => _controlShifts = value; }
}

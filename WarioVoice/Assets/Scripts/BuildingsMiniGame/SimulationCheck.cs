﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SimulationCheck : MonoBehaviour, IPointerDownHandler
{
    public enum SimulationAction
    {
        RESET, CHECK, RESETALL, LEVELUP
    }

    public SimulationAction _action;

#pragma warning disable CS0649 // El campo 'SimulationCheck._buildManager' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private BuildingsManager _buildManager;
#pragma warning restore CS0649 // El campo 'SimulationCheck._buildManager' nunca se asigna y siempre tendrá el valor predeterminado null
    //[SerializeField] private Text _text;
#pragma warning disable CS0649 // El campo 'SimulationCheck._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _text;
#pragma warning restore CS0649 // El campo 'SimulationCheck._text' nunca se asigna y siempre tendrá el valor predeterminado null

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_action)
        {
            case SimulationAction.RESET:
                _buildManager.resetLevel(_buildManager.CurrentLevel);
                break;
            case SimulationAction.CHECK:
                string _name = _text.text.Remove(_text.text.Length - 1);
                _buildManager.parseCommandSimulation(_name);
                break;
            case SimulationAction.RESETALL:
                _buildManager.resetLevel(0);
                break;
            case SimulationAction.LEVELUP:
                _buildManager.resetLevel(_buildManager.CurrentLevel+1);
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

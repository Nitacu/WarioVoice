﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConfigureRPG : MonoBehaviour
{
    private int _difficultyLevel;
    private CharacterBuilder _characterBuilder;
    private LamiaController _lamiaController;
    private ControlShifts _controlShifts;
    [SerializeField] private List<GameObject> _enemys = new List<GameObject>();

    private void Start()
    {
        _controlShifts = FindObjectOfType<ControlShifts>();
        createdEnemy();
        _characterBuilder = FindObjectOfType<CharacterBuilder>();
        _lamiaController = FindObjectOfType<LamiaController>();
        configurateLevel();

        //carga los ataque que van a tener los heroes
        // se tiene que hacer de esta forma para que no copie la referencia sino las cosas del interior
        foreach (VoiceAttacks voice in _lamiaController.ListAttacksDefinitive)
        {
            _characterBuilder.ListAttacksDefinitive.Add(voice);
        }


        foreach (VoiceAttacks voice in _lamiaController.ListAttacksUseful)
        {
            _characterBuilder.ListAttacksUseful.Add(voice);
        }


        foreach (VoiceAttacks voice in _lamiaController.ListAttacksUseless)
        {
            _characterBuilder.ListAttacksUseless.Add(voice);
        }

        foreach (VoiceAttacks voice in _lamiaController.ListHealingObjects)
        {
            _characterBuilder.ListHealingObjects.Add(voice);
        }

        // crea los players
        _characterBuilder.createdCharacters();
    }

    public void createdEnemy()
    {
        GameObject enemy;
        enemy = _enemys[GameManager.GetInstance().getGameDifficulty() - 1];
        enemy.SetActive(true);
        FindObjectOfType<ExeAttack>().Lamia = enemy.GetComponent<LamiaController>();
    }

    // la vida de cada personaje
    public float configurationCharacters()
    {
        _difficultyLevel = GameManager.GetInstance().getGameDifficulty();
        switch (_difficultyLevel)
        {
            case 1:
                return 2;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 2:
                return 3;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 3:
                return 3;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 4:
                return 3;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 5:
                return 3;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 6:
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 7:
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 8:
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 9:
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible

            case 10:
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                return 1;
#pragma warning disable CS0162 // Se detectó código inaccesible
                break;
#pragma warning restore CS0162 // Se detectó código inaccesible
        }
    }

    public void configurateLevel()
    {
        _difficultyLevel = GameManager.GetInstance().getGameDifficulty();

        switch (_difficultyLevel)
        {
            case 1:
                
                _controlShifts.Invoke("playerEnemy", 5);
                _characterBuilder.NumberCharacters = 2;
                _characterBuilder.NUMBER_ATTACKS_DEFINITIVE1 = 0;
                _characterBuilder.NUMBER_ATTACKS_USEFUL1 = 4;
                _characterBuilder.NUMBER_ATTACKS_USELESS1 = 1;
                _characterBuilder.NUMBER_HEALING_OBJECTS1 = 1;
                _characterBuilder.SPLIT_ATTACKS1 = 2;
                _lamiaController.Life = 4;
                break;

            case 2:
                _controlShifts.Invoke("playerTurn", 5);
                _characterBuilder.NumberCharacters = 2;
                _characterBuilder.NUMBER_ATTACKS_DEFINITIVE1 = 1;
                _characterBuilder.NUMBER_ATTACKS_USEFUL1 = 2;
                _characterBuilder.NUMBER_ATTACKS_USELESS1 = 2;
                _characterBuilder.NUMBER_HEALING_OBJECTS1 = 1;
                _characterBuilder.SPLIT_ATTACKS1 = 0;
                _lamiaController.Life = 5;
                break;

            case 3:
                _controlShifts.Invoke("playerEnemy", 5);
                _characterBuilder.NumberCharacters = 2;
                _characterBuilder.NUMBER_ATTACKS_DEFINITIVE1 = 0;
                _characterBuilder.NUMBER_ATTACKS_USEFUL1 = 4;
                _characterBuilder.NUMBER_ATTACKS_USELESS1 = 1;
                _characterBuilder.NUMBER_HEALING_OBJECTS1 = 1;
                _characterBuilder.SPLIT_ATTACKS1 = 2;
                _lamiaController.Life = 4;
                break;

            case 4:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                break;
        }

    }
}

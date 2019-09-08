﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBuilder : MonoBehaviour
{
    [Header("Todos los ataques del juego")]
    [SerializeField] private List<VoiceAttacks> _listAttacks;
    [Header("Todos los cuerpos de personajes")]
    [SerializeField] private List<TypeHeroeRPG> _listHeroes;
    private const int NUMBER_ATTACKS = 4;
    private int _numberCharacters;
    [Header("Objeto que contendra todos los personajes")]
    [SerializeField] private List<GameObject> _contentCharacters;
    [Header("prefab de personaje")]
    [SerializeField] private GameObject _character;

    private ConfigureRPG _configureRPG;
    public int NumberCharacters { get => _numberCharacters; set => _numberCharacters = value; }


    public void createdCharacters()
    {
        _configureRPG = FindObjectOfType<ConfigureRPG>();
        GameObject aux;
        int numberRandom;

        //crea cada tarjeta de persona
        for (int i = 0; i < NumberCharacters; i++)
        {
            aux = Instantiate(_character, _contentCharacters[NumberCharacters - 2].GetComponentsInChildren<Transform>()[i+1].position,Quaternion.identity);
           
            // se le agrega un icono
            numberRandom = Random.Range(0, _listHeroes.Count - 1);
            aux.GetComponent<HeroProperties>().getIdentity(_listHeroes[numberRandom]._standing, _listHeroes[numberRandom]._icon);
            aux.GetComponent<HeroProperties>().getCharacterStastic(_configureRPG.configurationCharacters());
            _listHeroes.RemoveAt(numberRandom);

            //le agrega los ataques aca personajes y saca estos ataques de la lista
            for (int e = 0; e < NUMBER_ATTACKS; e++)
            {
                numberRandom = Random.Range(0, _listAttacks.Count - 1);
                aux.GetComponent<HeroProperties>().getAttack(_listAttacks[numberRandom]);
                _listAttacks.RemoveAt(numberRandom);
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureRPG : MonoBehaviour
{
    private int _difficultyLevel;
    private CharacterBuilder _characterBuilder;
    private LamiaController _lamiaController;

    private void Start()
    {
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

    // la vida de cada personaje
    public float configurationCharacters()
    {
        _difficultyLevel = GameManager.GetInstance().getGameDifficulty();
        switch (_difficultyLevel)
        {
            case 1:
                return 3;
                break;

            case 2:
                return 3;
                break;

            case 3:
                return 3;
                break;

            case 4:
                return 3;
                break;

            case 5:
                return 3;
                break;

            case 6:
                return 1;
                break;

            case 7:
                return 1;
                break;

            case 8:
                return 1;
                break;

            case 9:
                return 1;
                break;

            case 10:
                return 1;
                break;


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                return 1;
                break;
        }
    }

    public void configurateLevel()
    {
        _difficultyLevel = GameManager.GetInstance().getGameDifficulty();

        switch (_difficultyLevel)
        {
            case 1:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;

            case 2:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;

            case 3:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;

            case 4:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;

            case 5:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = 5;
                break;

            case 6:
                _characterBuilder.NumberCharacters = 3;
                _lamiaController.Life = 1;
                break;

            case 7:
                _characterBuilder.NumberCharacters = 3;
                _lamiaController.Life = 1;
                break;

            case 8:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = 1;
                break;

            case 9:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = 1;
                break;

            case 10:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = 1;
                break;


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                break;
        }

    }
}

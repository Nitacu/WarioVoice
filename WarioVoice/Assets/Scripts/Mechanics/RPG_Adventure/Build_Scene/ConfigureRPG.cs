using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureRPG : MonoBehaviour
{
    [SerializeField]private int _difficultyLevel;
    private CharacterBuilder _characterBuilder;
    private LamiaController _lamiaController;

    private void Start()
    {
        _characterBuilder = FindObjectOfType<CharacterBuilder>();
        _lamiaController = FindObjectOfType<LamiaController>();
        configurateLevel();
        _characterBuilder.createdCharacters();
    }

    public float configurationCharacters()
    {
        _difficultyLevel = AttackGlossary.GetInstance()._difficultyLevel;
        switch (_difficultyLevel)
        {
            case 1:
                return Random.Range(510,560);
                break;

            case 2:
                return Random.Range(490, 560);
                break;

            case 3:
                return Random.Range(470, 560);
                break;

            case 4:
                return Random.Range(450, 560);
                break;

            case 5:
                return Random.Range(430, 560);
                break;

            case 6:
                return Random.Range(410, 560);
                break;

            case 7:
                return Random.Range(390, 560);
                break;

            case 8:
                return Random.Range(370, 560);
                break;

            case 9:
                return Random.Range(350, 560);
                break;

            case 10:
                return Random.Range(330, 560);
                break;


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                return 300;
                break;
        }
    }

    public void configurateLevel()
    {

        switch (_difficultyLevel)
        {
            case 1:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = Random.Range(30,40);
                break;

            case 2:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = Random.Range(40, 53);
                break;

            case 3:
                _characterBuilder.NumberCharacters = 4;
                _lamiaController.Life = Random.Range(50, 66);
                break;

            case 4:
                _characterBuilder.NumberCharacters = 3;
                _lamiaController.Life = Random.Range(60, 78);
                break;

            case 5:
                _characterBuilder.NumberCharacters = 3;
                _lamiaController.Life = Random.Range(70, 92);
                break;

            case 6:
                _characterBuilder.NumberCharacters = 3;
                _lamiaController.Life = Random.Range(80, 105);
                break;

            case 7:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = Random.Range(90, 118);
                break;

            case 8:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = Random.Range(100, 131);
                break;

            case 9:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = Random.Range(110, 144);
                break;

            case 10:
                _characterBuilder.NumberCharacters = 2;
                _lamiaController.Life = Random.Range(120, 157);
                break;


            default:
                Debug.Log("no cuadran los niveles de dificultad");
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationWorms : MonoBehaviour
{
    private int _numberEnemys;
#pragma warning disable CS0414 // El campo 'ConfigurationWorms._numberAmunition' está asignado pero su valor nunca se usa
    private int _numberAmunition = 0;
#pragma warning restore CS0414 // El campo 'ConfigurationWorms._numberAmunition' está asignado pero su valor nunca se usa
#pragma warning disable CS0649 // El campo 'ConfigurationWorms._conffeti' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _conffeti;
#pragma warning restore CS0649 // El campo 'ConfigurationWorms._conffeti' nunca se asigna y siempre tendrá el valor predeterminado null
    void Start()
    {
        _numberEnemys = FindObjectsOfType<EnemyWorms>().Length;
        configurationLevel();
    }

    // todos los personajes le disparan al player
    public void lostGame()
    {
        if (_numberEnemys > 0)
        {
            FindObjectOfType<GuideControlWorm>().desactiveAll();
            EnemyWorms[] enemyWorms =  FindObjectsOfType<EnemyWorms>();

            foreach(EnemyWorms enemy in enemyWorms)
            {
                enemy.shootPlayer();
            }
        }
    }

    public void destroyEnemy()
    {
        _numberEnemys--;
        if (_numberEnemys == 0)
        {
            FindObjectOfType<GuideControlWorm>().desactiveAll();
            ControlMoney.EarnMoney(Random.Range(5, 10));
            Instantiate(_conffeti);
            Invoke("exitScene", 2);
        }
    }

    public void exitScene()
    {
        GameManager.GetInstance().launchNextMinigame(true);
    }

    public void configurationLevel()
    {
        switch (GameManager.GetInstance().getGameDifficulty())
        {
            case 1:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;
            
                break;

            case 2:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;
             
                break;

            case 3:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;
             
                break;

            case 4:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;

                break;

            case 5:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;
   
                break;

            case 6:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;
  
                break;

            case 7:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;

                break;

            case 8:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;

                break;

            case 9:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;

                break;

            case 10:
                FindObjectOfType<Ammunition>().getAmmunition(6);
                _numberAmunition = 6;

                break;

            default:
                Debug.Log("la dificultad esta fuera del rango " + GameManager.GetInstance().getGameDifficulty());
                break;

        }
    }


}

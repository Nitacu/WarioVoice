using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationWorms : MonoBehaviour
{
    [SerializeField]private int _numberEnemys = 0;
    private int _numberAmunition = 0;
    [SerializeField] private GameObject _conffeti;
    void Start()
    {
        configurationLevel();
    }

    // todos los personajes le disparan al player
    public void lostGame()
    {
        if (_numberEnemys > 0)
        {
            
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
            Instantiate(_conffeti);
            Invoke("exitScene", 2);
            Debug.Log("Ganaste");
        }
    }

    public void exitScene()
    {
        SceneManager.LoadScene("WarioVoiceMenu");
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

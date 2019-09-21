using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneWorms : MonoBehaviour
{

    private void Start()
    {
        changeScene();
    }

    public void changeScene()
    {
        switch (GameManager.GetInstance().getGameDifficulty())
        {
            case 1:
                SceneManager.LoadScene("Worms_D1");
                break;
            case 2:
                SceneManager.LoadScene("Worms_D2");
                break;
            case 3:
                SceneManager.LoadScene("Worms_D3");
                break;
            case 4:
                SceneManager.LoadScene("Worms_D4");
                break;
            case 5:
                SceneManager.LoadScene("Worms_D5");
                break;
            case 6:
                SceneManager.LoadScene("Worms_D6");
                break;
            case 7:
                SceneManager.LoadScene("Worms_D7");
                break;
            case 8:
                SceneManager.LoadScene("Worms_D8");
                break;
            case 9:
                SceneManager.LoadScene("Worms_D9");
                break;
            case 10:
                SceneManager.LoadScene("Worms_D10");
                break;

            default:
                break;
        }
    }
}

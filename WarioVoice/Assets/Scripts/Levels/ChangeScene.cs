using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private const string MAINMENU = "MainMenu";
    private const string LEVEL_1 = "Level_1";
    private const string LEVEL_2 = "Level_2";
    private const string LEVEL_3 = "Level_3";
    private const string LEVEL_4 = "Level_4";
    private const string LEVEL_5 = "Level_5";
    private const string TUTORIAL_1 = "Tutorial_1";
    private const string TUTORIAL_2 = "Tutorial_2";
    private const string TUTORIAL_3 = "Tutorial_3";
    private const string TUTORIAL_4 = "Tutorial_4";
    private const string ENDING = "Ending";
    private const string CREDITS = "Credits";

    [SerializeField] private nameScenes _nameScenes;

    public enum nameScenes
    {
        MAINMENU,
        LEVEL_1,
        LEVEL_2,
        LEVEL_3,
        LEVEL_4,
        TUTORIAL_1,
        TUTORIAL_2,
        TUTORIAL_3,
        TUTORIAL_4,
        LEVEL_5,
        ENDING,
        CREDITS
    }    

    public void chanceScene()
    {
        switch (_nameScenes)
        {
            case nameScenes.CREDITS:
                SceneManager.LoadScene(CREDITS);
                break;

            case nameScenes.ENDING:
                SceneManager.LoadScene(ENDING);
                break;

            case nameScenes.MAINMENU:
                SceneManager.LoadScene(MAINMENU);
                break;

            case nameScenes.LEVEL_1:
                SceneManager.LoadScene(LEVEL_1);
                break;

            case nameScenes.LEVEL_2:
                SceneManager.LoadScene(LEVEL_2);
                break;

            case nameScenes.LEVEL_3:
                SceneManager.LoadScene(LEVEL_3);
                break;

            case nameScenes.LEVEL_4:
                SceneManager.LoadScene(LEVEL_4);
                break;

            case nameScenes.TUTORIAL_1:
                SceneManager.LoadScene(TUTORIAL_1);
                break;

            case nameScenes.TUTORIAL_2:
                SceneManager.LoadScene(TUTORIAL_2);
                break;

            case nameScenes.TUTORIAL_3:
                SceneManager.LoadScene(TUTORIAL_3);
                break;

            case nameScenes.TUTORIAL_4:
                SceneManager.LoadScene(TUTORIAL_4);
                break;

            case nameScenes.LEVEL_5:
                SceneManager.LoadScene(LEVEL_5);
                break;
        }
    }

}

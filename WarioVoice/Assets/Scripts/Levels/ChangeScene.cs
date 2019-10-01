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

    public const string SPIKINGLISHMENU = "SpikinglishMenu";
    private const string MATCHBUILDINGS = "Buildings";
    private const string IMITA_AL_BAILARIN = "DanceScene";
    private const string LOVE_SCENE = "LoveScene";
    public const string RPG = "RPg_adventure";
    private const string ABSTRACTPAINTING = "AbstractPainting";
    private const string ORCHESTA = "Orquesta";
    private const string WORMS = "Worms";
    public const string BETWEENMINIGAMES = "BetweenMiniGames";
    public const string LOADDATA = "WarioVoiceLoadPlayerData";
    public const string TUTORIALESPIKINGLISH = "TutorialEspikinglish";
    public const string ESPIKINGLISHGAMETEST = "MiniGameTest";



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
        CREDITS,
        SPIKINGLISHMENU,
        MATCHBUILDINGS,
        IMITA_AL_BAILARIN,
        LOVE_SCENE,
        RPG,
        PAINTING,
        ORCHESTA,
        WORMS,
        BETWEENMINIGAMES,
        LOADDATA,
        ESPKINGLISHTUTORIAL,
        ESPIKINGLISHGAMETEST
    }    

    public enum EspikinglishMinigames
    {
        PAINTING,
        ORCHESTA,
        LOVE_SCENE,
        WORMS,
        RPG
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

            case nameScenes.SPIKINGLISHMENU:
                SceneManager.LoadScene(SPIKINGLISHMENU);
                break;

            case nameScenes.MATCHBUILDINGS:
                SceneManager.LoadScene(MATCHBUILDINGS);
                break;

            case nameScenes.IMITA_AL_BAILARIN:
                SceneManager.LoadScene(IMITA_AL_BAILARIN);
                break;

            case nameScenes.LOVE_SCENE:
                SceneManager.LoadScene(LOVE_SCENE);
                break;

            case nameScenes.RPG:
                SceneManager.LoadScene(RPG);
                break;
            case nameScenes.PAINTING:
                SceneManager.LoadScene(ABSTRACTPAINTING);

                break;
            case nameScenes.ORCHESTA:
                SceneManager.LoadScene(ORCHESTA);
                break;

            case nameScenes.WORMS:
                SceneManager.LoadScene(WORMS);
                break;
            case nameScenes.BETWEENMINIGAMES:
                SceneManager.LoadScene(BETWEENMINIGAMES);
                break;
            case nameScenes.LOADDATA:
                if (SceneManager.GetActiveScene().name == ESPIKINGLISHGAMETEST)
                {
                    SceneManager.LoadScene(LOADDATA);
                }
                else
                {
                    string scene = (GameManager.GetInstance().DeveloperMode) ? ESPIKINGLISHGAMETEST : LOADDATA;
                    SceneManager.LoadScene(scene);
                }

                break;
            case nameScenes.ESPKINGLISHTUTORIAL:
                SceneManager.LoadScene(TUTORIALESPIKINGLISH);
                break;
        }
    }

    public static void ChangeSceneProgression(EspikinglishMinigames _miniGameScene)        
    {
        switch (_miniGameScene)
        {

            case EspikinglishMinigames.PAINTING:
                SceneManager.LoadScene(ABSTRACTPAINTING);
                break;
            case EspikinglishMinigames.ORCHESTA:
                SceneManager.LoadScene(ORCHESTA);
                break;
            case EspikinglishMinigames.LOVE_SCENE:
                SceneManager.LoadScene(LOVE_SCENE);
                break;
            case EspikinglishMinigames.RPG:
                SceneManager.LoadScene(RPG);
                break;
            case EspikinglishMinigames.WORMS:
                SceneManager.LoadScene(WORMS);
                break;
            default:
                break;
        }
    }

}

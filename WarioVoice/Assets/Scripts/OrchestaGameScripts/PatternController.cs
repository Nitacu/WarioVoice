using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatternController : MonoBehaviour
{
    public GameObject instrumentsGameObject;
    public GameObject director;
    public GameObject audience;
    public List<Instrument> instrumentsList = new List<Instrument>();
    private List<Instrument> instrumentsInScene = new List<Instrument>();
    private List<Instrument> showedInstruments = new List<Instrument>();
    private PatternPanelController patternPanel;
    public int difficulty = 2;
    private int patternDuration = 0;
    private int numberOfInstruments = 0;
    private PartitureController partiture;
    private PatternCreator patternCreator;
    private int numberOfPatterns = 1;
    private FadeController fade;
    public GameObject confetti;
    private int contInstrumentCreator = 0;
    public GameObject partiturePanel;
    public GameObject tomatoes;
    public FeedbackController feedback;
    public AudioSource cameraAudioSource;
    public Canvas pauseMenuCanvas;
    //List that will be used to play and check patrons 
    private List<Instrument[]> patronList = new List<Instrument[]>();
    private Instrument[] checkPattern;

    private bool showingPattern = false;
    private bool isPlaying = false;
    private int countPatrons = 0;
    private int currentPatron = 0;
    private int contInstrument = 0;
    private int contChecking = 0;
    private int instrumentDifficulty = 0;
    private bool patternShowed = false;


    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameManager.GetInstance().getGameDifficulty();

        

        selectDifficulty();

        patternPanel = FindObjectOfType<PatternPanelController>();
        partiture = FindObjectOfType<PartitureController>();
        fade = FindObjectOfType<FadeController>();
        patternCreator = FindObjectOfType<PatternCreator>();

        instrumentCreator();

        for (int i = 0; i < numberOfPatterns; i++)
        {
            patronList.Add(patternCreator.patternCreatorCrystal(numberOfInstruments, patternDuration, instrumentsInScene));
        }

        foreach (Instrument _instrument in patronList[0])
        {
            Debug.Log(_instrument.instrument.ToString());
        }

        setInstruments();
        disableInstruments();
        disableColliders();

        //startGame();

    }

    private void setInstruments()
    {
        
        contInstrumentCreator = 0;

        for (int i = 0; i < patternDuration; i++)
        {
            foreach (Transform child in instrumentsGameObject.transform)
            {
                if (child.gameObject.GetComponent<InstrumentController>().numberspawn == contInstrumentCreator)
                {
                    Debug.Log(contInstrumentCreator);
                    child.gameObject.GetComponent<InstrumentController>().instrumentObject = patronList[currentPatron][contInstrumentCreator];
                    child.gameObject.GetComponent<InstrumentController>().setInstrument();
                    child.gameObject.GetComponent<InstrumentController>().setMemberPlaying();

                }
            }

            contInstrumentCreator++;

        }
        contInstrumentCreator = 0;
    }

    private bool checkShowedInstruments(Instrument _scriptable)
    {
        foreach (Instrument instrument in showedInstruments)
        {
            if (_scriptable == instrument)
            {
                return true;
            }
        }
        return false;
    }

    private void disableInstruments() //this method deactivates all unused instruments in the pattern
    {
        contInstrumentCreator = 0;
        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.gameObject.SetActive(false);
        }


        foreach (Transform child in instrumentsGameObject.transform)
        {
            if (child.gameObject.GetComponent<InstrumentController>().instrumentObject != null)
            {
                if (!checkShowedInstruments(child.gameObject.GetComponent<InstrumentController>().instrumentObject))
                {
                    child.gameObject.SetActive(true);
                    showedInstruments.Add(child.gameObject.GetComponent<InstrumentController>().instrumentObject);
                }
                else
                {
                    child.gameObject.GetComponent<InstrumentController>().instrumentObject = null;
                    child.gameObject.GetComponent<InstrumentController>()._instrument = InstrumentController.ENUMINSTRUMENT.NULL;
                }
            }
        }

       

            contInstrumentCreator = 0;
    }

    public void startGame()
    {
        patternPanel.musicPatternCreator(patronList[currentPatron]);
        patternPanel.gameObject.SetActive(false);
        pauseMenuCanvas.sortingOrder = 100;
        showPatron();
    }

    public void disableColliders()
    {
        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void activateColliders()
    {
        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void activateCollidersWithTime(float time)
    {
        if (patternShowed)
        {
            Invoke("activateColliders", time);
        }
    }

    private void selectDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                numberOfInstruments = 2;
                patternDuration = 2;
                break;
            case 2:
                numberOfInstruments = 3;
                patternDuration = 3;
                break;
            case 3:
                numberOfInstruments = 3;
                patternDuration = 4;
                break;
            case 4:
                numberOfInstruments = 4;
                patternDuration = 5;
                break;
            case 5:
                numberOfInstruments = 5;
                patternDuration = 6;
                break;
            case 6:
                numberOfInstruments = 6;
                patternDuration = 6;
                break;
            case 7:
                numberOfInstruments = 5;
                patternDuration = 5;
                break;
            case 8:
                patternDuration = 6;
                numberOfInstruments = 6;
                break;
            case 9:
                numberOfInstruments = 7;
                patternDuration = 7;
                break;
            case 10:
                numberOfInstruments = 8;
                patternDuration = 8;
                break;
        }

        if (difficulty > 0 && difficulty < 3) //easy instruments (1-4)
        {
            instrumentDifficulty = 1;
        }

        if (difficulty > 2 && difficulty < 5) // easy and medium instruments (5-8)
        {
            instrumentDifficulty = 2;
        }

        if (difficulty > 4) // medium and hard instruments (9-10)
        {
            instrumentDifficulty = 3;
        }
    }

    private void instrumentCreator()
    {
        int randomNumber = 0;

        switch (instrumentDifficulty)
        {
            case 1:
                for (int i = 0; i < numberOfInstruments; i++)
                {
                    randomNumber = Random.Range(0, instrumentsList.Count);
                    if (instrumentsList[randomNumber].difficulty == InstrumentController.INSTRUMENTDIFFICULTY.EASY)
                    {
                        instrumentsInScene.Add(instrumentsList[randomNumber]);
                        instrumentsList.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }

                }
                break;
            case 2:
                for (int i = 0; i < numberOfInstruments; i++)
                {
                    randomNumber = Random.Range(0, instrumentsList.Count);
                    if (instrumentsList[randomNumber].difficulty == InstrumentController.INSTRUMENTDIFFICULTY.MEDIUM)
                    {
                        instrumentsInScene.Add(instrumentsList[randomNumber]);
                        instrumentsList.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }

                }
                break;

            case 3:
                for (int i = 0; i < numberOfInstruments; i++)
                {
                    randomNumber = Random.Range(0, instrumentsList.Count);
                    if (instrumentsList[randomNumber].difficulty == InstrumentController.INSTRUMENTDIFFICULTY.HARD || instrumentsList[randomNumber].difficulty == InstrumentController.INSTRUMENTDIFFICULTY.MEDIUM)
                    {
                        instrumentsInScene.Add(instrumentsList[randomNumber]);
                        instrumentsList.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }

                }
                break;

        }

    }



    private void showPatron()
    {
        countPatrons = patronList[currentPatron].Length;
        showingPattern = true;
        checkPattern = patronList[currentPatron];
        StartCoroutine(ShowInstrument(0.2f, patronList[currentPatron][contInstrument])); //Turns de current instrument in the pattern
    }

    IEnumerator ShowInstrument(float delayTime, Instrument newInstrument)
    {

        yield return new WaitForSeconds(delayTime);
        float clipDuration = 0;

        partiture.playAnimation();

        foreach (Transform child in instrumentsGameObject.transform)
        {

            if (child.gameObject.GetComponent<InstrumentController>()._instrument == newInstrument.instrument)
            {

                FindObjectOfType<ShowingPatternControl>().showPattern(newInstrument.instrument.ToString());
                child.gameObject.GetComponent<InstrumentController>().changeInstrument(true);
                child.gameObject.GetComponent<InstrumentController>().playSound();
                clipDuration = child.gameObject.GetComponent<InstrumentController>().getSoundTime();
            }
        }
        StartCoroutine(turnOffInstrument(clipDuration, newInstrument));
    }

    IEnumerator turnOffInstrument(float delayTime, Instrument newInstrument)
    {
        contInstrument++;

        yield return new WaitForSeconds(delayTime);

        foreach (Transform child in instrumentsGameObject.transform)
        {

            if (child.gameObject.GetComponent<InstrumentController>()._instrument == newInstrument.instrument)
            {
                child.gameObject.GetComponent<InstrumentController>().changeInstrument(false);

            }
        }

        if (contInstrument < countPatrons)
        {
            showPatron();
        }
        else
        {
            contInstrument = 0;
            currentPatron++;
            showingPattern = false;
            fade.playFade();
            FindObjectOfType<ShowingPatternControl>().clearScreen();
            FindObjectOfType<ShowingPatternControl>().cont = 0;
            Invoke("switchScene", 2);
        }
    }

    private void turnQuietInstruments()
    {
        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.gameObject.GetComponent<InstrumentController>().setQuietInstrument();
        }
    }    

    public void checkInstrument(InstrumentController.ENUMINSTRUMENT _enumInstrument, bool instrumentWord)
    {
        isPlaying = false;
        float clipDuration = 0;

        if (instrumentWord)
        {

            director.SetActive(false);

            turnQuietInstruments();

            //Player said an instrument
            if (!showingPattern)
            {

                if (_enumInstrument == patronList[currentPatron - 1][contChecking].instrument)
                {
                    SaveSystem.increaseMicrophonePressedTime(true, _enumInstrument.ToString(), ChangeScene.EspikinglishMinigames.ORCHESTA);
                    foreach (Transform child in instrumentsGameObject.transform)
                    {
                        if (child.gameObject.GetComponent<InstrumentController>()._instrument == _enumInstrument && !isPlaying)
                        {
                            child.gameObject.GetComponent<InstrumentController>().setMemberPlaying();
                            child.gameObject.GetComponent<InstrumentController>().playSound();
                            child.gameObject.GetComponent<InstrumentController>().playClip();
                            isPlaying = true;
                            clipDuration = child.gameObject.GetComponent<InstrumentController>().getSoundTime();
                        }
                    }
                    patternPanel.turnOnNote(contChecking);
                    contChecking++;

                    if (contChecking > patronList[currentPatron - 1].Length - 1)
                    {
                        //Muy bien, ganaste
                        Invoke("winGame", clipDuration);
                        //messageInScreen.GetComponent<ScreenMessage>().winScreen();
                    }
                }
                else
                {
                    //Decir que le quedó mal
                    feedback.playWrong();
                    fade.disableSpeechButton();
                    SaveSystem.increaseMicrophonePressedTime(false);
                    foreach (Transform child in instrumentsGameObject.transform)
                    {
                        child.gameObject.GetComponent<InstrumentController>().setQuietInstrument();
                    }

                    Invoke("endGame", feedback.getWrongLength() + 0.5f);

                }


                GetComponent<PatternCheckOrchesta>().canTalk = true;

            }

        }
        else
        {
            //wrong pronunciation
            feedback.playQuestion();
            SaveSystem.increaseMicrophonePressedTime(false);
        }
    }

    private void winGame()
    {
        Debug.Log("Ganaste");
        confetti.SetActive(true);
        GetComponent<AudioSource>().Play();
        cameraAudioSource.Stop();
        GameManager.GetInstance();
        fade.permanentFade();
        disableColliders();
        Invoke("nextLevel", 3.5f);
    }

    private void endGame()
    {
        cameraAudioSource.Stop();
        Debug.Log("Perdiste");
        fade.permanentFade();
        tomatoes.SetActive(true);
        tomatoes.GetComponent<AudioSource>().Play();
        Invoke("lostLevel", 4);
    }

    private void switchScene()
    {
        //patternPanel.gameObject.SetActive(true); 
        partiture.gameObject.SetActive(false);
        activateColliders();
        partiturePanel.SetActive(true);
        //director.SetActive(true);
        patternShowed = true;
        audience.SetActive(true);

        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.gameObject.GetComponent<InstrumentController>().setQuietInstrument();

        }

    }

    public void nextLevel()
    {
        GameManager.GetInstance().launchNextMinigame(true);
    }

    public void lostLevel()
    {
        GameManager.GetInstance().launchNextMinigame(false);
    }


}

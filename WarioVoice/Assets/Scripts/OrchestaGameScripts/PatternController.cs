﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    public GameObject instrumentsGameObject;
    public GameObject director;
    public GameObject audience;
    public List<Instrument> instrumentsList = new List<Instrument>();
    private List<Instrument> instrumentsInScene = new List<Instrument>();
    private PatternPanelController patternPanel;
    public int difficulty = 2;
    private int patternDuration = 0;
    private int numberOfInstruments = 0;
    private PartitureController partiture;
    private PatternCreator patternCreator;
    private int numberOfPatterns = 1;
    private FadeController fade;
    public GameObject confetti;
    
    //List that will be used to play and check patrons 
    private List<Instrument[]> patronList = new List<Instrument[]>();
    private Instrument[] checkPattern;

    private bool showingPattern = false;

    private int countPatrons = 0;
    private int currentPatron = 0;
    private int contInstrument = 0;
    private int contChecking = 0;

    // Start is called before the first frame update
    void Start()
    {
        patternPanel = FindObjectOfType<PatternPanelController>();
        partiture = FindObjectOfType<PartitureController>();
        fade = FindObjectOfType<FadeController>();
        patternCreator = FindObjectOfType<PatternCreator>();
        difficulty = GameManager.GetInstance().getGameDifficulty();
        selectDifficulty();
        instrumentCreator();

        for (int i = 0; i < numberOfPatterns; i++)
        {
            patronList.Add(patternCreator.patternCreatorCrystal(numberOfInstruments, patternDuration, instrumentsInScene));
        }

        //startGame();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        patternPanel.musicPatternCreator(patronList[currentPatron]);
        patternPanel.gameObject.SetActive(false);
        showPatron();
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
                numberOfInstruments = 2;
                patternDuration = 3;
                break;
            case 3:
                numberOfInstruments = 3;
                patternDuration = 3;
                break;
            case 4:
                numberOfInstruments = 3;
                patternDuration = 4;
                break;
            case 5:
                numberOfInstruments = 4;
                patternDuration = 5;
                break;
            case 6:
                numberOfInstruments = 4;
                patternDuration = 4;
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
                patternDuration = 6;
                break;
            case 10:
                numberOfInstruments = 8;
                patternDuration = 8;
                break;
        }
    }

    private void instrumentCreator()
    {
        int randomNumber = 0;

        for (int i = 0; i < numberOfInstruments; i++)
        {
            randomNumber = Random.Range(0, instrumentsList.Count);
            instrumentsInScene.Add(instrumentsList[randomNumber]);
            instrumentsList.RemoveAt(randomNumber);
        }
    }



    private void showPatron()
    {
        countPatrons = patronList[currentPatron].Length;
        showingPattern = true;
        checkPattern = patronList[currentPatron];
        StartCoroutine(ShowInstrument(0.2f, patronList[currentPatron][contInstrument])); //Turns de current color in the pattern
    }

    IEnumerator ShowInstrument(float delayTime, Instrument newInstrument)
    {
        yield return new WaitForSeconds(delayTime);

        partiture.playAnimation();

        foreach (Transform child in instrumentsGameObject.transform)
        {
            if (child.gameObject.GetComponent<InstrumentController>().instrumentObject == newInstrument)
            {
                child.gameObject.GetComponent<InstrumentController>().changeInstrument(true, newInstrument);
            }
        }

        StartCoroutine(turnOffInstrument(1.2f, newInstrument));
    }

    IEnumerator turnOffInstrument(float delayTime, Instrument newInstrument)
    {
        contInstrument++;

        yield return new WaitForSeconds(delayTime);

        foreach (Transform child in instrumentsGameObject.transform)
        {
            if (child.gameObject.GetComponent<InstrumentController>().instrumentObject == newInstrument)
            {
                child.gameObject.GetComponent<InstrumentController>().changeInstrument(false, newInstrument);
                //if (contInstrument >= countPatrons) //Este indica que es el ultimo
                //child.gameObject.GetComponent<CrystalController>().idleAnimation();
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

        if (instrumentWord)
        {
            director.SetActive(false);

            turnQuietInstruments();

            //Player said an instrument
            if (!showingPattern)
            {

                if (_enumInstrument == patronList[currentPatron - 1][contChecking].instrument)
                {
                    foreach (Transform child in instrumentsGameObject.transform)
                    {
                        if (child.gameObject.GetComponent<InstrumentController>()._instrument == _enumInstrument)
                        {
                            child.gameObject.GetComponent<InstrumentController>().setDirectorPlaying();
                        }
                    }
                    patternPanel.turnOnNote(contChecking);
                    contChecking++;

                    if (contChecking > patronList[currentPatron - 1].Length - 1)
                    {
                        //Muy bien, ganaste
                        Debug.Log("Ganaste");
                        confetti.SetActive(true);
                        fade.permanentFade();
                        GameManager.GetInstance().increaseDifficulty();
                        //messageInScreen.GetComponent<ScreenMessage>().winScreen();
                    }
                }
                else
                {
                    //Decir que le quedó mal
                    Debug.Log("Perdiste");
                    fade.backToMenuButton();
                    foreach (Transform child in instrumentsGameObject.transform)
                    {
                        child.gameObject.GetComponent<InstrumentController>().setQuietInstrument();
                    }

                    director.SetActive(true);
                }

            }

        }
        else
        {
            //wrong pronunciation
        }
    }


    private void switchScene()
    {
        //patternPanel.gameObject.SetActive(true); 
        partiture.gameObject.SetActive(false);
        director.SetActive(true);
        
        audience.SetActive(true);

        foreach (Transform child in instrumentsGameObject.transform)
        {
            child.gameObject.GetComponent<InstrumentController>().setQuietInstrument();

        }

    }


}

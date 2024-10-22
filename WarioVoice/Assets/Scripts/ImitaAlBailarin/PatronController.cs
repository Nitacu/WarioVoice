﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatronController : MonoBehaviour
{
    #region crystalGameObjects
    //Crystals in scene (add to inspector)
    public GameObject twoCrystals;
    public GameObject threeCrystals;
    public GameObject fourCrystals;
    public GameObject fiveCrystals;

    #endregion

    private GameObject messageInScreen;

    #region Difficulty variables
    public int difficulty = 0; //goes from 1 to 10 
    private int patternDuration = 0;
    [SerializeField]
    private int numberOfPatterns = 1;
    private int numberOfCrystals = 0;
    private PatronsScript patronCreator;
    #endregion

    //List that will be used to play and check patrons 
    private List<Crystal[]> patronList = new List<Crystal[]>();
    private int countPatrons;
    private int currentPatron = 0;
    private int contColor = 0;
    private Crystal[] checkPattern;
    private bool showingPattern = false;
    private GameObject activeCrystals;
    private PatternPanelController panelCrystals;
    private List<Transform> GUICrystals = new List<Transform>();
    

    #region Colors List
    //List that has all the existent colors (this will be used to create the crystals in scene)

    public List<Crystal> crystalList = new List<Crystal>();
    private List<Crystal> crystalInScene = new List<Crystal>();
    //This list is used to know what are the crystals in scene, this list will be sent to the pattern creator to know what collors to use
    
    
    #endregion

    private int contChecking = 0;

    // Start is called before the first frame update
    void Start()
    {
        panelCrystals = FindObjectOfType<PatternPanelController>();
        difficulty = GameManager.GetInstance().getGameDifficulty();
        messageInScreen = FindObjectOfType<ScreenMessage>().gameObject;
        patronCreator = GetComponent<PatronsScript>();
        selectDifficulty();
        crystalCreator();
        
        for(int i = 0; i < numberOfPatterns; i++)
        {
            //patronList.Add(patronCreator.patternCreator(numberOfCrystals,patternDuration, colorsInScene));
            patronList.Add(patronCreator.patternCreatorCrystal(numberOfCrystals, patternDuration, crystalInScene));
        }

      /*  foreach(Crystal crystal in patronList[0])
        {
            Debug.Log(crystal.crystalColor.ToString());
        }*/
        
    }

    public void startGame()
    {
        showPatron();
        panelCrystals.patternCreator(patronList[currentPatron]);

        foreach (Transform child in panelCrystals.transform)
        {
            GUICrystals.Add(child);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!showingPattern)
        {
            if (currentPatron < patronList.Count)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    showPatron();
                }
            }
        }
    }

    private void showPatron()
    {
        countPatrons = patronList[currentPatron].Length;
        showingPattern = true;
        checkPattern = patronList[currentPatron];
        StartCoroutine(LightCrystal(0.2f, patronList[currentPatron][contColor])); //Turns de current color in the pattern
    }

    IEnumerator LightCrystal(float delayTime, Crystal crystal)
    {
        yield return new WaitForSeconds(delayTime);

        foreach(Transform child in activeCrystals.transform)
        {
            if(child.gameObject.GetComponent<CrystalController>().crystalColor == crystal.crystalColor)
            {
                child.gameObject.GetComponent<CrystalController>().changeCrystal(true, crystal);
            }
        }
        messageInScreen.GetComponent<ScreenMessage>().showTextColor(crystal);
        StartCoroutine(turnOffCrystal(1.2f, crystal));
    }

    IEnumerator turnOffCrystal(float delayTime, Crystal crystal)
    {
        contColor++;

        yield return new WaitForSeconds(delayTime);

        foreach (Transform child in activeCrystals.transform)
        {
            if (child.gameObject.GetComponent<CrystalController>().crystalColor == crystal.crystalColor)
            {
                child.gameObject.GetComponent<CrystalController>().changeCrystal(false, crystal);
                if (contColor >= countPatrons) //Este indica que es el ultimo
                    child.gameObject.GetComponent<CrystalController>().idleAnimation();
            }
        }

        messageInScreen.GetComponent<ScreenMessage>().turnOffText();

        if (contColor < countPatrons)
        {
            showPatron();
        }
        else
        {
            contColor = 0;
            currentPatron++;
            showingPattern = false;
            messageInScreen.GetComponent<ScreenMessage>().repeatPattern();
        }
    }

    private void selectDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                twoCrystals.SetActive(true);
                activeCrystals = twoCrystals;
                numberOfCrystals = 2;
                patternDuration = 2;
                break;
            case 2:
                activeCrystals = twoCrystals;
                twoCrystals.SetActive(true);
                numberOfCrystals = 2;
                patternDuration = 3;
                break;
            case 3:
                threeCrystals.SetActive(true);
                activeCrystals = threeCrystals;
                numberOfCrystals = 3;
                patternDuration = 3;
                break;
            case 4:
                threeCrystals.SetActive(true);
                activeCrystals = threeCrystals;
                numberOfCrystals = 3;
                patternDuration = 4;
                break;
            case 5:
                threeCrystals.SetActive(true);
                activeCrystals = threeCrystals;
                numberOfCrystals = 3;
                patternDuration = 5;
                break;
            case 6:
                fourCrystals.SetActive(true);
                activeCrystals = fourCrystals;
                numberOfCrystals = 4;
                patternDuration = 4;
                break;
            case 7:
                fourCrystals.SetActive(true);
                activeCrystals = fourCrystals;
                numberOfCrystals = 4;
                patternDuration = 5;
                break;
            case 8:
                fourCrystals.SetActive(true);
                activeCrystals = fourCrystals;
                patternDuration = 6;
                numberOfCrystals = 4;
                break;
            case 9:
                fiveCrystals.SetActive(true);
                activeCrystals = fiveCrystals;
                numberOfCrystals = 5;
                patternDuration = 6;
                break;
            case 10:
                fiveCrystals.SetActive(true);
                activeCrystals = fiveCrystals;
                numberOfCrystals = 5;
                patternDuration = 8;
                break;
        }
    }

    private void crystalCreator()
    {
        int cont = 0;
        int randomNumber2 = 0;

        for(int i = 0; i < numberOfCrystals; i++)
        {
            
            randomNumber2 = Random.Range(0, crystalList.Count);
            crystalInScene.Add(crystalList[randomNumber2]);
            crystalList.RemoveAt(randomNumber2);
            
        }

        foreach (Transform child in activeCrystals.transform)
        {
            child.gameObject.GetComponent<CrystalController>().changeCrystalColor(crystalInScene[cont]);
            cont++;
        }

        
    }

    public void checkVoice(CrystalController.Colors color, bool colorWord)
    {
        if (colorWord)
        {
            foreach (Transform child in activeCrystals.transform)
            {
                if (!child.gameObject.GetComponent<CrystalController>().GUICrystal)
                {
                    child.gameObject.GetComponent<CrystalController>().isOn = false;
                }
            }
            if (!showingPattern)
            {
                if (color == patronList[currentPatron - 1][contChecking].crystalColor)
                {
                    foreach (Transform child in activeCrystals.transform)
                    {
                        if (child.gameObject.GetComponent<CrystalController>().crystalColor == color)
                        {
                            child.gameObject.GetComponent<CrystalController>().changeCrystal(true, patronList[currentPatron - 1][contChecking]);
                        }

                    }

                    GUICrystals[contChecking].gameObject.GetComponent<CrystalController>().changeCrystal(true, patronList[currentPatron - 1][contChecking]);


                    contChecking++;

                    messageInScreen.GetComponent<ScreenMessage>().goodOrBad(true);

                    if (contChecking > patronList[currentPatron - 1].Length - 1)
                    {
                        //Muy bien, ganaste
                        Debug.Log("Ganaste");
                        GameManager.GetInstance().increaseDifficulty();
                        messageInScreen.GetComponent<ScreenMessage>().winScreen();
                    }
                }
                else
                {

                    messageInScreen.GetComponent<ScreenMessage>().goodOrBad(false);

                    //Decir que le quedó mal
                    Debug.Log("Perdiste");

                    foreach (Transform child in activeCrystals.transform)
                    {
                        if (child.gameObject.GetComponent<CrystalController>().crystalColor == color)
                        {
                            child.gameObject.GetComponent<CrystalController>().lostPattern();
                        }
                        else
                        {
                            child.gameObject.GetComponent<CrystalController>().isOn = false;
                        }
                    }
                }
            }
        }
        else
        {

        }
    }

    public bool getShowingPattern()
    {
        return showingPattern;
    }

    public GameObject getActiveCrystals()
    {
        return activeCrystals;
    }
}

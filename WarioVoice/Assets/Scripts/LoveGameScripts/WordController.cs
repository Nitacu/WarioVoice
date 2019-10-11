using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WordController : MonoBehaviour
{
    public List<Sign> signs = new List<Sign>(); //List of all signs in the game (place them in inspector)
    [HideInInspector]
    public List<Sign> signsInGame = new List<Sign>(); //Empty list that will be filled with a certain random number of signs of the main Signs list
    public Sprite emptySign;
    public int gameDifficulty; //Difficulty of the game, this goes from 1 to 10, there must be at least 1 sign per difficulty level to make it work.
    private int numberOfSigns; //This determines the number of signs that the player will 
                               //have to see during the minigame, this number is determined according to difficulty
    private int wordDifficulty = 0; //This variable determines the complexity of the words that will show ingame, 1 = easy, 2 = easy and medium 3 = all difficulties

    #region EverythingRelated to friend showing sign in inspector
    public GameObject playerSign;
    public GameObject player;
    public TextMeshProUGUI signText;
    public GameObject women;
    private GameObject loveMetter;
    private GameObject wtfBar;
    public GameObject finalScreen;
    public ParticleSystem confetti;
    public GameObject speechButton;
    #endregion

    private bool winning = false;
    [HideInInspector]
    public bool endGame = false;
    private bool isShowingSign = true;
    [HideInInspector]
    public int currentSign = 0; //Cont variable that determines the sign that will be shown
    private int contWTF = 0;

    // Start is called before the first frame update
    void Start()
    {
        loveMetter = FindObjectOfType<LoveMeterController>().gameObject;
        wtfBar = FindObjectOfType<WTFBarController>().gameObject;
        setDifficulty();
        player.SetActive(false);
        
    }

    public void startGame()
    {
        player.SetActive(true);
        Invoke("waitStartTime", 1f);   
    }

    public void loseScene()
    {
        signText.enabled = false;
        women.GetComponent<Animator>().Play(Animator.StringToHash("LeavingRestaurant"));
        Invoke("lostLevel", 6.2f);
        //FindObjectOfType<FinalScreenController>().loseScreenImage();
    }

    private void waitStartTime()
    {
        
        player.GetComponent<PositionController>().playAnimation();
        //sends to next method to chose next sign and show it
        Invoke("turnSignOn", player.GetComponent<PositionController>().getCurrentClipTime());
    }

    private void nextSign()
    {
        //turns everything off
        playerSign.SetActive(false);
        isShowingSign = false;
        if (signText.enabled)
        {
            signText.enabled = false;
        }
       

        player.GetComponent<PositionController>().playAnimation();
        //sends to next method to chose next sign and show it
        Invoke("turnSignOn", player.GetComponent<PositionController>().getCurrentClipTime());
    }

    private void turnSignOn()
    {
        //Turn Everything ON again
       

        isShowingSign = true;
        

        //Show current sign
        if(signsInGame[currentSign].signSprite == null) //If has a sprite, set it to the one of the scriptable object, else, enable Text and write the ENUM word
        {
            waitTimeSprite(); //time it has to wait for the position of the player to update, if it doesnt wait, the word will
                                           //be in the air for some miliseconds before clamping to the player sign
        }
        else
        {
            playerSign.GetComponent<SpriteRenderer>().sprite = signsInGame[currentSign].signSprite;
        }
        //playerSign.SetActive(true);
        
    }

    private void waitTimeSprite() //Sets sign to the corresponding TEXT
    {
        playerSign.GetComponent<SpriteRenderer>().sprite = emptySign;
        signText.enabled = true;
        signText.text = signsInGame[currentSign].item.ToString();
        
    }

    private void setDifficulty() //Sets the number of signs that will be used in the minigame and what signs will be shown
    {
        gameDifficulty = GameManager.GetInstance().getGameDifficulty();
        numberOfSigns = gameDifficulty + 1;
        loveMetter.GetComponent<LoveMeterController>().numberOfSigns = numberOfSigns;
        

        if (gameDifficulty > 0 && gameDifficulty < 4)
        {
            wordDifficulty = 1;
        }

        if (gameDifficulty > 3 && gameDifficulty < 8)
        {
            wordDifficulty = 2;
        }

        if (gameDifficulty > 7)
        {
            wordDifficulty = 3;
        }

        createSigns();

       

    }

    private void createSigns()
    {
        int randomNumber = 0;

        switch (wordDifficulty)
        {
            case 1:
                for (int i = 0; i < numberOfSigns; i++)
                {
                    randomNumber = Random.Range(0, signs.Count);
                    if (signs[randomNumber].difficulty == WordList.wordDifficulty.EASY)
                    {
                        signsInGame.Add(signs[randomNumber]);
                        signs.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }         
                }
                break;
            case 2:
                for (int i = 0; i < numberOfSigns; i++)
                {
                    randomNumber = Random.Range(0, signs.Count);
                    if (signs[randomNumber].difficulty == WordList.wordDifficulty.EASY || signs[randomNumber].difficulty == WordList.wordDifficulty.MEDIUM)
                    {
                        signsInGame.Add(signs[randomNumber]);
                        signs.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }
                }
                break;
                
            case 3:
                for (int i = 0; i < numberOfSigns; i++)
                {
                    randomNumber = Random.Range(0, signs.Count);
                    if (signs[randomNumber].difficulty == WordList.wordDifficulty.HARD || signs[randomNumber].difficulty == WordList.wordDifficulty.MEDIUM)
                    {
                        signsInGame.Add(signs[randomNumber]);
                        signs.RemoveAt(randomNumber);
                    }
                    else
                    {
                        i--;
                    }
                }
                break;   
        }
    }

    public void checkAnswer(string answer)
    {
        Sign tempSign;
        bool correctAnswer = false;
        bool correctPronunciation = false;

        if (isShowingSign)
        {
      

            foreach (WordList.itemNames possibleAnswer in signsInGame[currentSign].possibleAnswers) //Checks all possible answers for the word or image in sign
            {
                //answer.Equals(possibleAnswer.ToString(), System.StringComparison.OrdinalIgnoreCase)
                if (answer.Equals(possibleAnswer.ToString(), System.StringComparison.OrdinalIgnoreCase))
                {
                    correctAnswer = true;
                    correctPronunciation = true;
                }
            }

            SaveSystem.increaseMicrophonePressedTime(correctPronunciation);

            if (correctAnswer)
            {
                currentSign++;
                if (currentSign < signsInGame.Count) //Checks cont of how many words has the player said, to know if he won or he should keep going.
                {
                    women.GetComponent<WomanController>().playGoodSoundEffect();
                    chooseExitAnimation(false);
                    women.GetComponent<WomanController>().playLoveAnimation();
                    loveMetter.GetComponent<LoveMeterController>().updateLoveBar();
                }
                else
                {
                    Debug.Log("Ganaste, quedó bien enamorada");
                    chooseExitAnimation(true);
                    disableSpeechButton();
                    //finalScreen.SetActive(true);
                    confetti.Play();
                    loveMetter.GetComponent<LoveMeterController>().updateLoveBar();
                    women.GetComponent<WomanController>().playLoveAnimation();
                    Invoke("nextLevel", 4f);
                    winning = true;
                    //FindObjectOfType<FinalScreenController>().winScreenImage();
                   
                }
            }
            else
            {
                contWTF++;
                winning = false;
                
                women.GetComponent<WomanController>().playBadSoundEffect();
                tempSign = signsInGame[currentSign];
                signsInGame.RemoveAt(currentSign);
                signsInGame.Add(tempSign);
                women.GetComponent<WomanController>().playWTFAnimation(contWTF);
                //Add wtf bar and check tries to see if loses
                wtfBar.GetComponent<WTFBarController>().updateWTFbar();
                if (contWTF < 3)
                {
                    chooseExitAnimation(false);
                }
                else
                {
                    disableSpeechButton();
                    chooseExitAnimation(true);
                    signText.enabled = false;
                }
                
                //finalScreen.SetActive(true);
                //FindObjectOfType<FinalScreenController>().loseScreenImage();
                //Debug.Log("Perdiste");

            }
        }
    }

    private void chooseExitAnimation(bool endGame)
    {
        signText.text = " ";
        if (player.GetComponent<PositionController>().previousClip.name == "Taxi")
        {
            player.GetComponent<PositionController>().playTaxiOut();
            Invoke("nextSign", 1f);

        }else if(player.GetComponent<PositionController>().previousClip.name == "TrashCan")
        {
            player.GetComponent<PositionController>().playTrashCanOut();
            Invoke("nextSign", 1f);
        }
        else if (player.GetComponent<PositionController>().previousClip.name == "Cake")
        {
            player.GetComponent<PositionController>().playCakeOut();
            Invoke("nextSign", 1f);
        }else if (!endGame)
        {
            nextSign();
        }

        if (endGame)
        {
            player.GetComponent<PositionController>().playIdleFriend();
        }
    }

    public void nextGame()
    {
        GameManager.GetInstance().launchNextMinigame(winning);
    }

    public void nextLevel()
    {
        GameManager.GetInstance().launchNextMinigame(true);
    }

    public void lostLevel()
    {
        GameManager.GetInstance().launchNextMinigame(false);
    }

    public void disableSpeechButton()
    {
        speechButton.GetComponent<Image>().color = Color.gray;
        speechButton.GetComponent<EventTrigger>().enabled = false;
    }
}


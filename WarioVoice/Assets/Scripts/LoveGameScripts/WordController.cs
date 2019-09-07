using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WordController : MonoBehaviour
{
    public List<Sign> signs = new List<Sign>(); //List of all signs in the game (place them in inspector)
    private List<Sign> signsInGame = new List<Sign>(); //Empty list that will be filled with a certain random number of signs of the main Signs list

    public int gameDifficulty; //Difficulty of the game, this goes from 1 to 10, there must be at least 1 sign per difficulty level to make it work.
    private int numberOfSigns; //This determines the number of signs that the player will 
                               //have to see during the minigame, this number is determined according to difficulty

    #region EverythingRelated to friend showing sign in inspector
    public GameObject playerSign;
    public GameObject player;
    public TextMeshProUGUI signText;
    #endregion

    private bool isShowingSign = true;
    private int currentSign = 0; //Cont variable that determines the sign that will be shown

    // Start is called before the first frame update
    void Start()
    {
        setDifficulty();
        turnSignOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowingSign)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                
            }
        }
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
        player.GetComponent<Animator>().Play(Animator.StringToHash("SearchingSign"));
        
        //sends to next method to chose next sign and show it
        Invoke("turnSignOn", 1);
    }

    private void turnSignOn()
    {
        //Turn Everything ON again
        player.GetComponent<Animator>().Play(Animator.StringToHash("ShowingSign"));
        playerSign.SetActive(true);
        isShowingSign = true;


        //Show current sign
        if(signsInGame[currentSign].signSprite == null) //If has a sprite, set it to the one of the scriptable object, else, enable Text and write the ENUM word
        {
            playerSign.GetComponent<SpriteRenderer>().sprite = null;
            signText.enabled = true;
            signText.text = signsInGame[currentSign].item.ToString();
        }
        else
        {
            playerSign.GetComponent<SpriteRenderer>().sprite = signsInGame[currentSign].signSprite;
        }

    }

    private void setDifficulty() //Sets the number of signs that will be used in the minigame and what signs will be shown
    {
        numberOfSigns = gameDifficulty + 1;

        int randomNumber = 0;

        for (int i = 0; i < numberOfSigns; i++)
        {
            randomNumber = Random.Range(0, signs.Count);
            signsInGame.Add(signs[randomNumber]);
            signs.RemoveAt(randomNumber);
        }

    }

    public void checkAnswer(string answer)
    {
        if (isShowingSign)
        {
            if (answer == signsInGame[currentSign].item.ToString())
            {
                currentSign++;
                if (currentSign < signsInGame.Count) //Checks cont of how many words has the player said, to know if he won or he should keep going.
                {
                    nextSign();
                }
                else
                {
                    Debug.Log("Ganaste, quedó bien enamorada");
                    SceneManager.LoadScene("WarioVoiceMenu");
                }
            }
            else
            {
                Debug.Log("Perdiste");
                SceneManager.LoadScene("LoveScene");
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EspikinglishTutorialManager : CommandParser
{

    #region COMMANDS
    public const string GO = "GO";
    #endregion

    #region DIALOGS
    public const string WELCOME_ENG = "Welcome to Espikinglish";
    public const string WELCOME_ESP = "Bienvenido a Espikinglish";

    public const string TEST_ENG = "Press the button and say \"Go\"";
    public const string TEST_ESP = "Presiona el botón y di \"Go\"";

    public const string GOODJOB_ENG = "Great, press continue to start the game";
    public const string GOODJOB_ESP = "Excelente, presiona continuar para comenzar a jugar";
    #endregion

    [SerializeField] private TMP_InputField _inputFieldTest;

    [Header("UI Vars")]
    [SerializeField] private TextMeshProUGUI _textGuideEng;
    [SerializeField] private TextMeshProUGUI _textGuideEsp;
    //[SerializeField] private GameObject _pressButtonPanel;
    [SerializeField] private GameObject _buttonPointer;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _speechButton;



    [Header("Timers")]
    [SerializeField] private float welcomteTime;

    public override void parseCommand(string command)
    {
        if (command.Equals(GO, System.StringComparison.OrdinalIgnoreCase))
        {
            _textGuideEng.text = GOODJOB_ENG;
            _textGuideEsp.text = GOODJOB_ESP;
            _startButton.SetActive(true);
            _speechButton.GetComponent<SetActiveSpeechButton>().setButton(false);
        }
        else
        {
            buttonPressed(true);
        }
    }

    private void OnEnable()
    {
        _textGuideEng.text = WELCOME_ENG;
        _textGuideEsp.text = WELCOME_ESP;
        StartCoroutine(nextStep(welcomteTime));
    }

    IEnumerator nextStep(float timeToActivate)
    {
        yield return new WaitForSeconds(timeToActivate);

        //STEP 1
        //_pressButtonPanel.SetActive(true);
        _buttonPointer.SetActive(true);
        _textGuideEng.text = TEST_ENG;
        _textGuideEsp.text = TEST_ESP;
    }

    public void buttonPressed(bool activate)
    {
        _buttonPointer.SetActive(activate);
    }

    public void startGame()
    {
        GameManager.GetInstance().StartGame();
    }

    public void test()
    {
        parseCommand(_inputFieldTest.text);
    }
}

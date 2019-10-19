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
    public const string TEST_ESP = "Presiona el boton y di \"Go\"";

    public const string GOODJOB_ENG = "Great, let's start";
    public const string GOODJOB_ESP = "Excelente, empecemos";
    #endregion

    [SerializeField] private TMP_InputField _inputFieldTest;
    [SerializeField] private GameObject _confetti;

    [SerializeField] private List<GameObject> _developerModeObjects = new List<GameObject>();
    [SerializeField] private AudioSource _sourceEffect;
    [SerializeField] private AudioSource _sourceVoice;

    [Header("UI Vars")]
    [SerializeField] private TextMeshProUGUI _textGuideEng;
    [SerializeField] private TextMeshProUGUI _textGuideEsp;
    //[SerializeField] private GameObject _pressButtonPanel;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _speechButton;

    private bool tutorialComplete = false;

    [Header("Timers")]
    [SerializeField] private float welcomteTime;
    [SerializeField] private float _startGameTime;

    [Header("AudioClips")]
    [SerializeField] AudioClip _welcomeClip;
    [SerializeField] AudioClip _sayGoClip;
    [SerializeField] AudioClip greatClip;
    [SerializeField] AudioClip _greatLetsStart;



    public override void parseCommand(string comandoNoWork, string command)
    {
      

        if (command.Equals(GO, System.StringComparison.OrdinalIgnoreCase))
        {            
            tutorialComplete = true;

            hidePointer(true);

            _textGuideEng.text = GOODJOB_ENG;

            //_startButton.SetActive(true);
            GameObject confetti = Instantiate(_confetti);
            confetti.transform.position = Vector3.zero;

            _textGuideEsp.text = GOODJOB_ESP;
            //playClip(_sourceVoice, _greatLetsStart);

            playClip(_sourceEffect ,greatClip);
            StartCoroutine(startGame());
            StartCoroutine(deactivateSpeechButton());
        }
        else
        {
            hidePointer(false);
        }
    }

    public void playClip(AudioSource source, AudioClip clip)
    {
        source.Pause();
        source.clip = clip;
        source.Play();
    }

    private void OnEnable()
    {
        Debug.Log("Developer mode: " + GameManager.GetInstance().DeveloperMode);

        foreach (var item in _developerModeObjects)
        {
            bool developerMode = GameManager.GetInstance().DeveloperMode;
            item.SetActive(developerMode);
        }

        _startButton.SetActive(false);
        _textGuideEng.text = WELCOME_ENG;
        _textGuideEsp.text = WELCOME_ESP;
        playClip( _sourceVoice, _welcomeClip);
        _speechButton.GetComponent<SetActiveSpeechButton>().setButton(false);
        StartCoroutine(nextStep(welcomteTime));
    }

    IEnumerator deactivateSpeechButton()
    {
        yield return new WaitForEndOfFrame();

        _speechButton.GetComponent<SetActiveSpeechButton>().setButton(false);
    }

    IEnumerator nextStep(float timeToActivate)
    {
        yield return new WaitForSeconds(timeToActivate);

        //STEP 1
        //_pressButtonPanel.SetActive(true);
        _pointer.SetActive(true);
        _textGuideEng.text = TEST_ENG;
        _textGuideEsp.text = TEST_ESP;
        playClip(_sourceVoice,_sayGoClip);
        _speechButton.GetComponent<SetActiveSpeechButton>().setButton(true);
    }

    public void hidePointer(bool hide)
    {
        _pointer.SetActive(!hide);
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(_startGameTime);

        GameManager.GetInstance().StartGame();
    }

    public void test()
    {
        parseCommand("",_inputFieldTest.text);
    }
}

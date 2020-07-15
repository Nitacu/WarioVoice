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

#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._inputFieldTest' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_InputField _inputFieldTest;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._inputFieldTest' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._confetti' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _confetti;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._confetti' nunca se asigna y siempre tendrá el valor predeterminado null

    [SerializeField] private List<GameObject> _developerModeObjects = new List<GameObject>();
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._sourceEffect' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioSource _sourceEffect;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._sourceEffect' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._sourceVoice' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioSource _sourceVoice;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._sourceVoice' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("UI Vars")]
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._textGuideEng' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _textGuideEng;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._textGuideEng' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._textGuideEsp' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _textGuideEsp;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._textGuideEsp' nunca se asigna y siempre tendrá el valor predeterminado null
    //[SerializeField] private GameObject _pressButtonPanel;
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._pointer' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _pointer;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._pointer' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._startButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _startButton;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._startButton' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._speechButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _speechButton;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._speechButton' nunca se asigna y siempre tendrá el valor predeterminado null

#pragma warning disable CS0414 // El campo 'EspikinglishTutorialManager.tutorialComplete' está asignado pero su valor nunca se usa
    private bool tutorialComplete = false;
#pragma warning restore CS0414 // El campo 'EspikinglishTutorialManager.tutorialComplete' está asignado pero su valor nunca se usa

    [Header("Timers")]
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager.welcomteTime' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float welcomteTime;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager.welcomteTime' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._startGameTime' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _startGameTime;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._startGameTime' nunca se asigna y siempre tendrá el valor predeterminado 0

    [Header("AudioClips")]
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._welcomeClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _welcomeClip;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._welcomeClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager._sayGoClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip _sayGoClip;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager._sayGoClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EspikinglishTutorialManager.greatClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AudioClip greatClip;
#pragma warning restore CS0649 // El campo 'EspikinglishTutorialManager.greatClip' nunca se asigna y siempre tendrá el valor predeterminado null
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

            //playClip(_sourceEffect ,greatClip);
            StartCoroutine(playGreat());
            StartCoroutine(startGame());
            StartCoroutine(deactivateSpeechButton());
        }
        else
        {
            hidePointer(false);
        }
    }

    IEnumerator playGreat()
    {
        yield return new WaitForEndOfFrame();

        playClip(_sourceEffect, greatClip);
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

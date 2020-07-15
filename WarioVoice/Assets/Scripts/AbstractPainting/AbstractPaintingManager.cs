using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

[System.Serializable]
public class PaintingLevel
{
#pragma warning disable CS0649 // El campo 'PaintingLevel._referencePaint' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _referencePaint;
#pragma warning restore CS0649 // El campo 'PaintingLevel._referencePaint' nunca se asigna y siempre tendrá el valor predeterminado null
    public GameObject ReferencePaint
    {
        get { return _referencePaint; }
    }

#pragma warning disable CS0649 // El campo 'PaintingLevel._splashSizeScale' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _splashSizeScale;
#pragma warning restore CS0649 // El campo 'PaintingLevel._splashSizeScale' nunca se asigna y siempre tendrá el valor predeterminado 0
    public float SplashSizeScale
    {
        get { return _splashSizeScale; }
    }

    private int _numberOfSplashes;
    public int NumberOfSplashes { get => _numberOfSplashes; set => _numberOfSplashes = value; }


    //Se puede asignar o buscar por tag todos los splashesReference con un script y un enum y asi agregarlos a una lista.
    private List<PaintSplashColor> _availableSplashes = new List<PaintSplashColor>();
    public List<PaintSplashColor> AvailableColors
    {
        get { return _availableSplashes; }
        set { _availableSplashes = value; }
    }

    private List<GameObject> _splashesInReferencePaint = new List<GameObject>();
    public List<GameObject> SplashesInReferencePaint
    {
        get { return _splashesInReferencePaint; }
        set { _splashesInReferencePaint = value; }
    }

}

public class AbstractPaintingManager : CommandParser
{
    #region HelpDialogs
    const string FIRSTDIALOG = "Say a color to set your brush\nDi un color para seleccionar";
    const string SAYACOLORFIRST = "Say a color to set your brush\nDi un color para seleccionar";
    const string PAINTONCANVAS = "Now, paint on your canvas\nAhora, pinta en tu lienzo";
    const string ANALYZING = "Analyzing Paint...\nAnalizando la pintura";
    const string LOSE = "Not enough \n No es suficiente";
    const string ALREADYANALYZING = "Analyzing Paint...\nAnalizando la pintura";
    const string FIRSTDRAW = "Paint something first\nPinta algo primero";
    const string WANTARESULT = "Did you finish your painting?\n ¿Terminaste tu pintura?";
    const string YESORNO = "Yes | No";

    private List<string> _lostDialogs = new List<string>()
    {"Not enough\nNo es suficiente", "Por la oreja de vangoh\nPor la oreja de vangoh\n",
        "Are you a Pigcaso" };
    private List<string> _winDialogs = new List<string>() {"Michelangelo would share you his pizza\nMiguel Angel te compartiría su pizza",
        "Rafael likes your service, 5 ninja stars\nA Rafael le gusta tu servicio, 5 estrellas ninja",
        "Donatello gives you 3 thumbs up\nDonatello te da 3 pulgares arriba",
        "Mother of Leo DaVinci",
    };
    #endregion

    #region Commands
    public const string YES = "YES";
    public const string NO = "NO";

    public const string BLUE = "Blue";
    public const string RED = "Red";
    public const string GREEN = "Green";
    public const string ORANGE = "Orange";
    public const string YELLOW = "Yellow";
    public const string PINK = "Pink";
    public const string PURPLE = "Purple";
    #endregion

    public enum SplashColor
    {
        BLUE,
        RED,
        GREEN,
        ORANGE,
        YELLOW,
        PINK,
        PURPLE,
        BLACK,
        BROWN,
        MAGENTA,
        GRAY,
        SILVER,
        COFFEE,
        RUBY,
        BRONZE,
        GOLD,
        SCARLET,
        OLIVE,
        CHOCOLATE,
        LIME,
        SANGRIA,
        TURQUOISE,
        AMETHYST,
        TEAL,
        JADE,
        BEIGE,
        PEACH,
        TAN,
        AZURE,
        HARLEQUIN

    }

    [SerializeField] private List<PaintingLevel> _levels = new List<PaintingLevel>();
    public List<PaintingLevel> Levels
    {
        get { return _levels; }
    }

#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._canvasMask' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private LayerMask _canvasMask;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._canvasMask' nunca se asigna y siempre tendrá el valor predeterminado 
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._brush' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _brush;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._brush' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._splashBasePrefab' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _splashBasePrefab;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._splashBasePrefab' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._wrongPaintPrefab' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _wrongPaintPrefab;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._wrongPaintPrefab' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._referenceCanvasTransform' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Transform _referenceCanvasTransform;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._referenceCanvasTransform' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._myCanvasTransform' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Transform _myCanvasTransform;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._myCanvasTransform' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._critiqueAppearClip' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AnimationClip _critiqueAppearClip;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._critiqueAppearClip' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._prefabConffeti' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _prefabConffeti;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._prefabConffeti' nunca se asigna y siempre tendrá el valor predeterminado null
    //[SerializeField] private List<HelpButton> _helpButtons = new List<HelpButton>();

    [Header("Critique Sprites")]
    [SerializeField] private Sprite _normalCritique;
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._analyzingCritique' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _analyzingCritique;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._analyzingCritique' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._correctCritique' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _correctCritique;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._correctCritique' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._wrongCritique' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _wrongCritique;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._wrongCritique' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("UI Control")]
    [SerializeField] private List<GameObject> _developermodeObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _buttonsToDeactivate = new List<GameObject>();
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._microphoneButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _microphoneButton;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._microphoneButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _level;
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._guideText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _guideText;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._guideText' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._initPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _initPanel;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._initPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._plallete' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private SetBottles _plallete;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._plallete' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._timeToDeactivateInitPanel' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _timeToDeactivateInitPanel;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._timeToDeactivateInitPanel' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _inactivityTime = 90;
    private float _inactivityTimeTracker;
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._palleteCompleted' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _palleteCompleted;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._palleteCompleted' nunca se asigna y siempre tendrá el valor predeterminado null


    [Header("Win Parameters")]
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._minCoincidencesPercentage' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _minCoincidencesPercentage;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._minCoincidencesPercentage' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._paintedSplashMargin' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private int _paintedSplashMargin;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._paintedSplashMargin' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._analyzingTime' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private int _analyzingTime;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._analyzingTime' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._analyzerBand' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _analyzerBand;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._analyzerBand' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._paintCritique' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _paintCritique;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._paintCritique' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._critiqueSpeechBublle' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _critiqueSpeechBublle;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._critiqueSpeechBublle' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._analyzeResultText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _analyzeResultText;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._analyzeResultText' nunca se asigna y siempre tendrá el valor predeterminado null
    private float _coindencePercentage;
    private float _splashCoincidencesCount;
#pragma warning disable CS0649 // El campo 'AbstractPaintingManager._timeTochangeLevel' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _timeTochangeLevel;
#pragma warning restore CS0649 // El campo 'AbstractPaintingManager._timeTochangeLevel' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private AudioSource _backgroundMusic;


    #region LevelControl
    private PaintSplashColor _currentSplashColorSelected;
    private int _currentLevel;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        //set { _currentLevel = value; }
    }
    private List<GameObject> _paintedSplahes = new List<GameObject>();

    private Vector2 _offsetBetweenCanvas;
    private bool _isAnalyzing;
    private GameObject _currentRerefencePaint;
    private bool _wantAResult = false;

    #endregion

    private void Start()
    {

        bool developermode = GameManager.GetInstance().DeveloperMode;
        foreach (var item in _developermodeObjects)
        {
            item.SetActive(developermode);
        }

        _currentLevel = GameManager.GetInstance().getGameDifficulty();
        _currentLevel--;
        if (_currentLevel + 1 > _levels.Count)
        {
            _currentLevel = _levels.Count - 1;
        }

        setLevel(_currentLevel);
        _initPanel.SetActive(true);
        StartCoroutine(deactivateInitPanel(_timeToDeactivateInitPanel));

        _microphoneButton.GetComponent<Outline>().enabled = (_currentLevel == 0) ? true : false;

        resetHelp();

    }

    private void Update()
    {
        if (_isAnalyzing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;

            if (Input.touches.Length > 0)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, _canvasMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == Tags.PaintCanvas)
                {
                    if (_currentSplashColorSelected != null)
                    {
                        InstantiateNewSplash(mousePos2D);
                        resetHelp();
                    }
                    else
                    {
                        _guideText.text = SAYACOLORFIRST;
                        Debug.Log("Select a color");
                    }
                }
            }
        }


        //TRACK INACTIVITY
        if (_inactivityTimeTracker < _inactivityTime)
        {
            _inactivityTimeTracker += Time.deltaTime;
        }
        else
        {
            _palleteCompleted.GetComponent<DeactivateOutlineOnclick>().PlayAnimationOutine(true);
        }
    }

    public void resetHelp()
    {
        _inactivityTimeTracker = 0;
        _palleteCompleted.GetComponent<DeactivateOutlineOnclick>().PlayAnimationOutine(false);
    }

    private void setHelpButtons()
    {
        _plallete.setBottles();
    }

    public void setLevel(int currentLevel)
    {
        //CAMBIAR BOTONES DE CHECK Y NIVEL
        foreach (var item in _buttonsToDeactivate)
        {
            item.GetComponent<SetActiveSpeechButton>().setButton(true);
        }

        _paintCritique.SetActive(false);
        _critiqueSpeechBublle.SetActive(false);

        //RESETEAR COLOR Y BRUSH
        _currentSplashColorSelected = null;
        _brush.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _isAnalyzing = false;

        //BORRAR PINTURA DE REFERENCIA ANTERIOR
        if (_currentRerefencePaint != null)
        {
            Destroy(_currentRerefencePaint);
            _currentRerefencePaint = null;
        }

        //RESETEAR MI CANVAS
        foreach (var item in _paintedSplahes)
        {
            Destroy(item);
        }
        _paintedSplahes.Clear();

        //TEXTS
        _guideText.text = FIRSTDIALOG;
        //_level.text = LEVEL + (currentLevel + 1);

        _currentRerefencePaint = Instantiate(_levels[currentLevel].ReferencePaint);
        _currentRerefencePaint.transform.position = _referenceCanvasTransform.position;

        PaintSplash[] _splashReferences = _currentRerefencePaint.GetComponentsInChildren<PaintSplash>();
        _levels[currentLevel].NumberOfSplashes = _splashReferences.Length;


        foreach (var item in _splashReferences)
        {
            if (!_levels[currentLevel].AvailableColors.Contains(item.MySplashColorType))
            {
                _levels[currentLevel].AvailableColors.Add(item.MySplashColorType);
            }


            _levels[currentLevel].SplashesInReferencePaint.Add(item.gameObject);
        }

        setHelpButtons();
    }

    public override void parseCommand(string comandoNoWork, string command)
    {
        if (_wantAResult)
        {
            wantAResult(command);
            return;
        }

        setBrushColor(command);
    }

    private void setBrushColor(string commandColor)
    {
        bool colorFinded = false;

        foreach (PaintSplashColor availableSplash in _levels[_currentLevel].AvailableColors)
        {
            if (commandColor.Equals(availableSplash._colorName, System.StringComparison.OrdinalIgnoreCase))
            {
                _currentSplashColorSelected = availableSplash;
                _brush.GetComponent<Image>().color = availableSplash._brushColor;
                colorFinded = true;
                _guideText.text = PAINTONCANVAS;

                resetHelp();
            }
        }

        Debug.Log("Color Finded: " + colorFinded);

        if (colorFinded)
        {
            SaveSystem.increaseMicrophonePressedTime(colorFinded, commandColor, ChangeScene.EspikinglishMinigames.PAINTING);
        }
        else
        {
            SaveSystem.increaseMicrophonePressedTime(colorFinded);
        }

    }

    private void InstantiateNewSplash(Vector2 _position)
    {
        if (PauseMenu._gameIsPaused)
        {
            return;
        }

        GameObject _newSplash = Instantiate(_splashBasePrefab);
        _newSplash.GetComponent<SpriteRenderer>().sprite = _currentSplashColorSelected._splashImage;

        float factor = _levels[_currentLevel].ReferencePaint.transform.localScale.x / _levels[_currentLevel].SplashesInReferencePaint[0].transform.localScale.x;
        float newScale = _myCanvasTransform.localScale.x / factor;

        _newSplash.transform.localScale = new Vector3(newScale, newScale, 1);
        Vector3 _newposition = new Vector3(_position.x, _position.y, 0);
        _newSplash.transform.position = _newposition;
        _newSplash.GetComponent<PaintSplash>().MySplashColorType = _currentSplashColorSelected;
        _paintedSplahes.Add(_newSplash);
    }

    public void deleteLastSplash()
    {
        if (_paintedSplahes.Count > 0)
        {
            Destroy(_paintedSplahes[_paintedSplahes.Count - 1]);
            _paintedSplahes.RemoveAt(_paintedSplahes.Count - 1);
        }
    }

    public void deleteAllSplashes()
    {
        foreach (var item in _paintedSplahes)
        {
            Destroy(item);
        }
        _paintedSplahes.Clear();
    }

    IEnumerator playHummingClip()
    {
        yield return new WaitForEndOfFrame();

        FindObjectOfType<PaintingSoundManager>().playHumming();
    }

    private void evaluatePaint()
    {

        if (_isAnalyzing)
        {
            _guideText.text = ALREADYANALYZING;
            return;
        }


        StartCoroutine(playHummingClip());

        //ANIMACIONES ANALISIS
        GameObject _newAnalyzerBand = Instantiate(_analyzerBand);
        _guideText.text = ANALYZING;
        _paintCritique.GetComponent<Image>().sprite = _analyzingCritique;
        //_paintCritique.SetActive(true);

        foreach (var item in _buttonsToDeactivate)
        {
            item.GetComponent<SetActiveSpeechButton>().setButton(false);
        }

        _analyzeResultText.text = "...";

        //RESETEAR QUE NO HAN SIDO ANALIZADOS
        foreach (var item in _paintedSplahes)
        {
            item.gameObject.GetComponent<SelfPaintSplash>().Matched = false;
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //VARIABLES CONTROL
        _isAnalyzing = true;
        bool _playerWin = false;
        _coindencePercentage = 0;
        _splashCoincidencesCount = 0;


        //ANALISIS E CADA SPLASH ORIGINAL CON LA COPIA

        _offsetBetweenCanvas.x = _myCanvasTransform.position.x - _referenceCanvasTransform.position.x;
        _offsetBetweenCanvas.y = _myCanvasTransform.position.y - _referenceCanvasTransform.position.y;

        float _deltaSize = _myCanvasTransform.localScale.x / _levels[_currentLevel].ReferencePaint.transform.localScale.x;

        foreach (var item in _levels[_currentLevel].SplashesInReferencePaint)
        {
            Vector2 _distanceBetweenSplasnAndCenter = item.transform.position - _referenceCanvasTransform.position;

            GameObject _copyReferenceSplash = Instantiate(item);

            _copyReferenceSplash.transform.position = new Vector3(_referenceCanvasTransform.position.x + (_distanceBetweenSplasnAndCenter.x * _deltaSize) + _offsetBetweenCanvas.x, _referenceCanvasTransform.position.y + (_distanceBetweenSplasnAndCenter.y * _deltaSize) + _offsetBetweenCanvas.y, -5);
            _copyReferenceSplash.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            _copyReferenceSplash.transform.localScale = _splashBasePrefab.transform.localScale;

            //AHORA EVLUAR SI ESTÁ CERCA DE OTRA MANCHA
            if (_copyReferenceSplash.GetComponent<ReferencePaintSplash>().evaluateSimilarSplashAround())
            {
                _splashCoincidencesCount++;

            }

            //Destroy(_copyReferenceSplash);
        }

        _coindencePercentage = (_splashCoincidencesCount * 100) / _levels[_currentLevel].SplashesInReferencePaint.Count;

        if (_coindencePercentage >= _minCoincidencesPercentage)
        {
            if (_paintedSplahes.Count >= (_levels[_currentLevel].SplashesInReferencePaint.Count - _paintedSplashMargin) && _paintedSplahes.Count <= (_levels[_currentLevel].SplashesInReferencePaint.Count + _paintedSplashMargin))
            {
                Debug.Log("Win");
                _playerWin = true;
            }
        }

        StartCoroutine(setWinOrLose(_playerWin, _newAnalyzerBand));
    }

    private void wantAResult(string want)
    {
        bool _want = false;


        if (want.Equals(YES, System.StringComparison.OrdinalIgnoreCase))
        {
            _want = true;
            SaveSystem.increaseMicrophonePressedTime(true, "YES", ChangeScene.EspikinglishMinigames.PAINTING);
        }
        else if (want.Equals(NO, System.StringComparison.OrdinalIgnoreCase))
        {
            SaveSystem.increaseMicrophonePressedTime(true, "NO", ChangeScene.EspikinglishMinigames.PAINTING);
            _want = false;
        }
        else
        {
            _guideText.text = YESORNO;
            SaveSystem.increaseMicrophonePressedTime(false);
            return;
        }

        if (_want)
        {
            evaluatePaint();
        }
        else
        {
            animateCritique(false);
            _critiqueSpeechBublle.SetActive(false);

            foreach (var item in _buttonsToDeactivate)
            {
                item.GetComponent<SetActiveSpeechButton>().setButton(true);
            }

            _guideText.text = "";
            _wantAResult = false;
        }
    }

    public void askWantAResult()
    {
        if (_currentLevel == 0 && _paintedSplahes.Count <= 0)
        {
            FindObjectOfType<PaintingSoundManager>().playWrongSound();
            _guideText.text = FIRSTDRAW;
            return;
        }

        animateCritique(true);

        foreach (var item in _buttonsToDeactivate)
        {
            item.GetComponent<SetActiveSpeechButton>().setButton(false);
        }

        StartCoroutine(showSpeechBubble());
    }

    private void animateCritique(bool appear)
    {
        _paintCritique.SetActive(true);
        string state = (appear) ? "Appear" : "Disappear";
        _paintCritique.GetComponent<Animator>().Play(Animator.StringToHash(state));
    }

    IEnumerator showSpeechBubble()
    {
        yield return new WaitForSeconds(_critiqueAppearClip.averageDuration);

        _microphoneButton.GetComponent<SetActiveSpeechButton>().setButton(true);

        _critiqueSpeechBublle.SetActive(true);
        _analyzeResultText.text = WANTARESULT;
        _guideText.text = YESORNO;

        _wantAResult = true;

    }

    IEnumerator setWinOrLose(bool playerWin, GameObject analyzerBandToDestroy)
    {
        yield return new WaitForSeconds(_analyzingTime);

        _critiqueSpeechBublle.SetActive(true);

        //_backgroundMusic.Stop();

        foreach (var item in _paintedSplahes)
        {
            if (!item.GetComponent<SelfPaintSplash>().Matched)
            {
                item.GetComponent<SpriteRenderer>().color = Color.grey;

                GameObject ecs = Instantiate(_wrongPaintPrefab);

                Vector2 newposition = new Vector2(item.transform.position.x, item.transform.position.y);
                ecs.transform.position = newposition;
            }
        }

        Destroy(analyzerBandToDestroy);

        if (playerWin)
        {
            _paintCritique.GetComponent<Image>().sprite = _correctCritique;
            int randomDialog = Random.Range(0, _winDialogs.Count);
            //int _indexRandom = System.Random.Range(0, _winDialogs.Count);
            _guideText.text = _winDialogs[randomDialog];
            _analyzeResultText.text = "Nice Paint\n" + "Splahes Number: " +
                    _paintedSplahes.Count + "/" + _levels[_currentLevel].SplashesInReferencePaint.Count
                    + "\nCoincidences: " + Mathf.RoundToInt(_coindencePercentage) + "/" + "100%";

            /*
            GameObject confetti = Instantiate(_prefabConffeti);
            confetti.transform.position = Vector3.zero;
            */
        }
        else
        {
            _paintCritique.GetComponent<Image>().sprite = _wrongCritique;
            int randomDialog = Random.Range(0, _lostDialogs.Count);
            _guideText.text = _lostDialogs[randomDialog];
            _analyzeResultText.text = "Meeh\n" + "Splahes Number: " +
                    _paintedSplahes.Count + "/" + _levels[_currentLevel].SplashesInReferencePaint.Count
                    + "\nCoincidences: " + Mathf.RoundToInt(_coindencePercentage) + "/" + "100%";

        }



        _microphoneButton.GetComponent<SetActiveSpeechButton>().setButton(false);

        FindObjectOfType<PaintingSoundManager>().playGoodJob(playerWin);

        StartCoroutine(LaunchNextLevel(playerWin));
    }

    IEnumerator LaunchNextLevel(bool Success)
    {
        //_microphoneButton.GetComponent<SetActiveSpeechButton>().setButton(false);
        yield return new WaitForSeconds(FindObjectOfType<PaintingSoundManager>().GoodPaint.length - 0.5f);

        if (Success)
        {
            ControlMoney.EarnMoney(Random.Range(5,10));
            GameObject confetti = Instantiate(_prefabConffeti);
            confetti.transform.position = Vector3.zero;
            FindObjectOfType<PaintingSoundManager>().playGreatTada();
        }

        yield return new WaitForSeconds(_timeTochangeLevel);

        GameManager.GetInstance().launchNextMinigame(Success);
    }

    IEnumerator deactivateInitPanel(float timeToDeactivate)
    {
        yield return new WaitForSeconds(timeToDeactivate);

        _initPanel.GetComponent<Animator>().Play(Animator.StringToHash("InitialAnimation"));
        //_initPanel.SetActive(false);
    }


}

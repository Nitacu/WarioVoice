using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PaintingLevel
{
    [SerializeField] private GameObject _referencePaint;
    public GameObject ReferencePaint
    {
        get { return _referencePaint; }
    }

    [SerializeField] private float _splashSizeScale;
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
    const string FIRSTDIALOG = "Say a color to set your brush";
    const string SAYACOLORFIRST = "Say a color first";
    const string PAINTONCANVAS = "Know paint on your canvas";
    const string ANALYZING = "Analyzing Paint...";
    const string WIN = "Mother of da Vinci\nPerfect Paint";
    const string LOSE = "BULLSHIT\nTryAgain";
    const string ALREADYANALYZING = "Al ready Analyzing, keep waiting";
    const string LEVEL = "Level ";
    const string GAMECOMPLETEDE = "Game Complete, Back to Menu";
    #endregion

    #region ColorCommands
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
        BLUE, RED, GREEN, ORANGE, YELLOW, PINK, PURPLE
    }

    [SerializeField] private List<PaintingLevel> _levels = new List<PaintingLevel>();

    [SerializeField] private LayerMask _canvasMask;
    [SerializeField] private GameObject _brush;
    [SerializeField] private GameObject _splashBasePrefab;
    [SerializeField] private Transform _referenceCanvasTransform;
    [SerializeField] private Transform _myCanvasTransform;
    [SerializeField] private List<HelpButton> _helpButtons = new List<HelpButton>();

    [Header("UI Control")]
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _guideText;
    [SerializeField] private GameObject _finishPaintButton;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private GameObject _keepTryingButton;
    [SerializeField] private GameObject _speechButton;
    [SerializeField] private GameObject _removeLastButton;
    [SerializeField] private GameObject _removeAllButton;



    [Header("Win Parameters")]
    [SerializeField] private float _minCoincidencesPercentage;
    [SerializeField] private int _paintedSplashMargin;
    [SerializeField] private int _analyzingTime;
    [SerializeField] private GameObject _analyzerBand;
    [SerializeField] private GameObject _paintCritique;
    [SerializeField] private GameObject _critiqueSpeechBublle;
    [SerializeField] private TextMeshProUGUI _analyzeResultText;
    private float _coindencePercentage;
    private float _splashCoincidencesCount;


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
    #endregion

    private void Start()
    {
        setLevel(_currentLevel);
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
                    }
                    else
                    {
                        _guideText.text = SAYACOLORFIRST;
                        Debug.Log("Select a color");
                    }
                }
            }
        }
    }

    private void setHelpButtons()
    {
        foreach (var item in _helpButtons)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < _levels[_currentLevel].AvailableColors.Count; i++)
        {
            _helpButtons[i].gameObject.SetActive(true);
            _helpButtons[i].Sprite.sprite = _levels[_currentLevel].AvailableColors[i]._splashImage;
            _helpButtons[i].Text.text = _levels[_currentLevel].AvailableColors[i]._colorName;
            _helpButtons[i].AudioClip = _levels[_currentLevel].AvailableColors[i]._audioClip;
        }


    }

    public void setLevel(int currentLevel)
    {
        //CAMBIAR BOTONES DE CHECK Y NIVEL
        _finishPaintButton.SetActive(true);
        _speechButton.SetActive(true);
        _removeAllButton.SetActive(true);
        _removeLastButton.SetActive(true);

        _nextLevelButton.SetActive(false);
        _keepTryingButton.SetActive(false);
        _paintCritique.SetActive(false);
        _critiqueSpeechBublle.SetActive(false);

        //RESETEAR COLOR Y BRUSH
        _currentSplashColorSelected = null;
        _brush.GetComponent<SpriteRenderer>().color = Color.white;
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
        _level.text = LEVEL + (currentLevel + 1);

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

    public override void parseCommand(string command)
    {
        setBrushColor(command);
    }

    private void setBrushColor(string commandColor)
    {
        foreach (PaintSplashColor availableSplash in _levels[_currentLevel].AvailableColors)
        {
            if (commandColor.Equals(availableSplash._colorName, System.StringComparison.OrdinalIgnoreCase))
            {
                _currentSplashColorSelected = availableSplash;
                _brush.GetComponent<SpriteRenderer>().color = _currentSplashColorSelected._brushColor;
            }
        }
    }

    private void InstantiateNewSplash(Vector2 _position)
    {
        GameObject _newSplash = Instantiate(_splashBasePrefab);
        _newSplash.GetComponent<SpriteRenderer>().sprite = _currentSplashColorSelected._splashImage;
        _newSplash.transform.localScale = new Vector3(_levels[_currentLevel].SplashSizeScale, _levels[_currentLevel].SplashSizeScale, 1);
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

    public void evaluatePaint()
    {
        if (_isAnalyzing)
        {
            _guideText.text = ALREADYANALYZING;
            return;
        }

        //ANIMACIONES ANALISIS
        GameObject _newAnalyzerBand = Instantiate(_analyzerBand);
        _guideText.text = ANALYZING;
        _paintCritique.SetActive(true);
        _critiqueSpeechBublle.SetActive(true);

        _currentRerefencePaint.SetActive(false);
        _speechButton.SetActive(false);
        _removeAllButton.SetActive(false);
        _removeLastButton.SetActive(false);

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

            Destroy(_copyReferenceSplash);
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

    IEnumerator setWinOrLose(bool playerWin, GameObject analyzerBandToDestroy)
    {
        yield return new WaitForSeconds(_analyzingTime);

        foreach (var item in _paintedSplahes)
        {
            if (!item.GetComponent<SelfPaintSplash>().Matched)
            {
                item.GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

        Destroy(analyzerBandToDestroy);

        if (playerWin)
        {
            _guideText.text = WIN;
            _analyzeResultText.text = "Nice Paint\n" + "Splahes Number: " +
                    _paintedSplahes.Count + "/" + _levels[_currentLevel].SplashesInReferencePaint.Count
                    + "\nCoincidences: " + Mathf.RoundToInt(_coindencePercentage) + "/" + _minCoincidencesPercentage + "%";

            if (_currentLevel + 2 <= _levels.Count)
            {
                _currentLevel++;
                _finishPaintButton.SetActive(false);
                _keepTryingButton.SetActive(false);
                _nextLevelButton.SetActive(true);
            }
            else
            {
                _isAnalyzing = true;
                _guideText.text = GAMECOMPLETEDE;
                _finishPaintButton.SetActive(false);
                _nextLevelButton.SetActive(false);
                _keepTryingButton.SetActive(false);
            }            
        }
        else
        {
            _guideText.text = LOSE;
            _keepTryingButton.SetActive(true);
            _analyzeResultText.text = "Meeh\n" + "Splahes Number: " +
                    _paintedSplahes.Count + "/" + _levels[_currentLevel].SplashesInReferencePaint.Count
                    + "\nCoincidences: " + Mathf.RoundToInt(_coindencePercentage) + "/" + _minCoincidencesPercentage + "%";
        }

    }

    IEnumerator setNextLevel(int currenLevelToLaunch, float timeToLaunchNextLevel)
    {
        yield return new WaitForSeconds(2);

        setLevel(currenLevelToLaunch);
    }

    public void keepTrying()
    {
        foreach (var item in _paintedSplahes)
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }

        _isAnalyzing = false;
        _keepTryingButton.SetActive(false);
        _finishPaintButton.SetActive(true);
        _nextLevelButton.SetActive(false);

        _speechButton.SetActive(true);
        _removeAllButton.SetActive(true);
        _removeLastButton.SetActive(true);

        _paintCritique.SetActive(false);
        _critiqueSpeechBublle.SetActive(false);
        _currentRerefencePaint.SetActive(true);
        _analyzeResultText.text = "...";
    }
}

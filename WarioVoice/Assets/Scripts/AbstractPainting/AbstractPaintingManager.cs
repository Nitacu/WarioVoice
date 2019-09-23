﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    const string FIRSTDIALOG = "Say a color to set your brush\nDi un color";
    const string SAYACOLORFIRST = "Say a color first\nDebes decir un color primero";
    const string PAINTONCANVAS = "Now, paint on your canvas\nAhora, dibuja en tu lienzo";
    const string ANALYZING = "Analyzing Paint...\nAnalizando el cuadro";
    const string WIN = "Mother of DaVinci\n Bien hecho";
    const string LOSE = "BULLSHIT\n Sigue intentando";
    const string ALREADYANALYZING = "Al ready Analyzing, keep waiting\nAun se está analizando el cuadro";
    const string LEVEL = "Level ";
    const string GAMECOMPLETEDE = "Game Complete, Back to Menu\nJuego completado, vuelve al menu";
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

    [SerializeField] private LayerMask _canvasMask;
    [SerializeField] private GameObject _brush;
    [SerializeField] private GameObject _splashBasePrefab;
    [SerializeField] private Transform _referenceCanvasTransform;
    [SerializeField] private Transform _myCanvasTransform;
    //[SerializeField] private List<HelpButton> _helpButtons = new List<HelpButton>();

    [Header("UI Control")]
    [SerializeField] private List<GameObject> _buttonsToDeactivate = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _guideText;
    [SerializeField] private GameObject _initPanel;
    [SerializeField] private SetBottles _plallete;
    [SerializeField] private float _timeToDeactivateInitPanel;


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
        _currentLevel = GameManager.GetInstance().getGameDifficulty();
        _currentLevel--;
        if (_currentLevel + 1 > _levels.Count)
        {
            _currentLevel = _levels.Count - 1;
        }

        setLevel(_currentLevel);
        _initPanel.SetActive(true);
        StartCoroutine(deactivateInitPanel(_timeToDeactivateInitPanel));
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

    public override void parseCommand(string command)
    {
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
                _brush.GetComponent<Image>().color = _currentSplashColorSelected._brushColor;
                colorFinded = true;
            }
        }

        Debug.Log("Color Finded: " + colorFinded);
        SaveSystem.increaseMicrophonePressedTime(colorFinded);
    }

    private void InstantiateNewSplash(Vector2 _position)
    {
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

        _critiqueSpeechBublle.SetActive(true);

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
            StartCoroutine(LaunchNextLevel(true));

            /* if (_currentLevel + 2 <= _levels.Count)
             {
                 _currentLevel++;
                 _finishPaintButton.SetActive(false);

             }
             else
             {
                 _isAnalyzing = true;
                 _guideText.text = GAMECOMPLETEDE;
                 _finishPaintButton.SetActive(false);

             }            */
        }
        else
        {
            _guideText.text = LOSE;
            _analyzeResultText.text = "Meeh\n" + "Splahes Number: " +
                    _paintedSplahes.Count + "/" + _levels[_currentLevel].SplashesInReferencePaint.Count
                    + "\nCoincidences: " + Mathf.RoundToInt(_coindencePercentage) + "/" + _minCoincidencesPercentage + "%";
            StartCoroutine(LaunchNextLevel(false));

        }

    }

    IEnumerator LaunchNextLevel(bool Success)
    {
        yield return new WaitForSeconds(_timeToDeactivateInitPanel);

        GameManager.GetInstance().launchNextMinigame(Success);

    }

    IEnumerator deactivateInitPanel(float timeToDeactivate)
    {
        yield return new WaitForSeconds(timeToDeactivate);

        _initPanel.GetComponent<Animator>().Play(Animator.StringToHash("InitialAnimation"));
        //_initPanel.SetActive(false);
    }

    /*public void keepTrying()
    {
        foreach (var item in _paintedSplahes)
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }

        _isAnalyzing = false;
        _finishPaintButton.SetActive(true);

        _speechButton.GetComponent<SetActiveSpeechButton>().setButton(true);

        _removeAllButton.SetActive(true);
        _removeLastButton.SetActive(true);

        _paintCritique.SetActive(false);
        _critiqueSpeechBublle.SetActive(false);
        _currentRerefencePaint.SetActive(true);
        _analyzeResultText.text = "...";
    }*/
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [Header("Win Parameters")]
    [SerializeField] private float _minCoincidencesPercentage;
    [SerializeField] private int _paintedSplashMargin;


    #region LevelControl
    private PaintSplashColor _currentSplashColorSelected;
    private int _currentLevel;
    private List<GameObject> _paintedSplahes = new List<GameObject>();

    private Vector2 _offsetBetweenCanvas;
    #endregion

    private void Start()
    {
        setLevel(_currentLevel);
    }

    private void Update()
    {
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
                        Debug.Log("Select a color");
                    }
                }
            }
        }
    }

    private void setLevel(int currentLevel)
    {
        GameObject _newReferencePaint = Instantiate(_levels[currentLevel].ReferencePaint);
        _newReferencePaint.transform.position = _referenceCanvasTransform.position;

        PaintSplash[] _splashReferences = _newReferencePaint.GetComponentsInChildren<PaintSplash>();
        _levels[currentLevel].NumberOfSplashes = _splashReferences.Length;


        foreach (var item in _splashReferences)
        {
            if (!_levels[currentLevel].AvailableColors.Contains(item.MySplashColorType))
            {
                _levels[currentLevel].AvailableColors.Add(item.MySplashColorType);
            }


            _levels[currentLevel].SplashesInReferencePaint.Add(item.gameObject);
        }
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
        //EVALUAR CANTIDAD DE SPLASHES

        //EVALUAR SIMILITUD ENTRE SPLASHES
        float splashCoincidencesCount = 0;

        _offsetBetweenCanvas.x = _myCanvasTransform.position.x - _referenceCanvasTransform.position.x;
        _offsetBetweenCanvas.y = _myCanvasTransform.position.y - _referenceCanvasTransform.position.y;

        //sumar a la posicion de cada splash el offset

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
                splashCoincidencesCount++;   
            }
        }

        float coindencePercentage = (splashCoincidencesCount * 100) / _levels[_currentLevel].SplashesInReferencePaint.Count;

        Debug.Log("Porcentaje de aciertos: " + coindencePercentage);
        Debug.Log("Numero de splashes: " + _paintedSplahes.Count);



        if (coindencePercentage >= _minCoincidencesPercentage)
        {
            if (_paintedSplahes.Count >= (_levels[_currentLevel].SplashesInReferencePaint.Count - _paintedSplashMargin) &&   _paintedSplahes.Count <= (_levels[_currentLevel].SplashesInReferencePaint.Count + _paintedSplashMargin))
            {
                Debug.Log("Win");
                return;
            }
        }

        Debug.Log("Lose");
    }
}

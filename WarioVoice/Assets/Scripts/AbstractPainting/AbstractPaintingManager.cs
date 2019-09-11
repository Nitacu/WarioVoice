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


    //Se puede asignar o buscar por tag todos los splashesReference con un script y un enum y asi agregarlos a una lista.
    private List<PaintSplashColor> _availableSplashes = new List<PaintSplashColor>();
    public List<PaintSplashColor> AvailableSplashes
    {
        get { return _availableSplashes; }
        set { _availableSplashes = value; }
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

    [SerializeField] private GameObject _brush;

    [SerializeField] private GameObject _splashBasePrefab;
    private PaintSplashColor _currentSplashColorSelected;

    [SerializeField] private int _currentLevel;


    [SerializeField] private Transform _referenceCanvasTransform;

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

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
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
        
        foreach (var item in _splashReferences)
        {
            if (!_levels[currentLevel].AvailableSplashes.Contains(item.MySplashColorType))
            {
                _levels[currentLevel].AvailableSplashes.Add(item.MySplashColorType);
            }
        }
    }

    public override void parseCommand(string command)
    {
        setBrushColor(command);
    }

    private void setBrushColor(string commandColor)
    {
        foreach (PaintSplashColor availableSplash in _levels[_currentLevel].AvailableSplashes)
        {
            if (commandColor.Equals(availableSplash._colorName, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Colro finded");
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
    }
}

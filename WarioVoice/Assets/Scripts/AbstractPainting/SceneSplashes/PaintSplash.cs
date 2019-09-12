using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplash : MonoBehaviour
{


    [SerializeField] private PaintSplashColor _mySplashColorType;   
    public PaintSplashColor MySplashColorType
    {
        get { return _mySplashColorType; }
        set { _mySplashColorType = value; }
    }

    public virtual void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _mySplashColorType._splashImage;
    }

}

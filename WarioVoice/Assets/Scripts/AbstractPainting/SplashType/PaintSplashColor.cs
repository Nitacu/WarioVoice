using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Paint Splash Color")]
public class PaintSplashColor : ScriptableObject
{
    public string _colorName;
    public Sprite _splashImage;
    public AbstractPaintingManager.SplashColor _splasColorType;
    public Color _brushColor;
    
}

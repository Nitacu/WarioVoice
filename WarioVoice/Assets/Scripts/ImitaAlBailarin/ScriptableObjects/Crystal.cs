using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal", menuName = "ColorCrystal")]
public class Crystal :ScriptableObject
{
    public Sprite crystalSprite;
    public CrystalController.Colors crystalColor;
    public AnimationClip danceClip;

}

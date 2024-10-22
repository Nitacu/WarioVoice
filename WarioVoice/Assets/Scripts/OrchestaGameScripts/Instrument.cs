﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Instrument", menuName = "Instrument")]
public class Instrument : ScriptableObject
{
    public AudioClip clipSound;
    public AudioClip clipName;
    public Sprite sprite;
    public AnimationClip clipAnimation;
    public InstrumentController.ENUMINSTRUMENT instrument;
    public InstrumentController.INSTRUMENTDIFFICULTY difficulty;
}

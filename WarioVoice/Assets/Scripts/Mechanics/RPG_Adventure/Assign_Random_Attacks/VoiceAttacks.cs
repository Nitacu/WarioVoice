﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTypeAttack", menuName = "Type Attack")]
public class VoiceAttacks : ScriptableObject
{
    public string _verb;
    public AttackGlossary.attack _attack;
    public AudioClip _pronunciation;
    public AudioClip _soundEffect;
    public float _damage;
    public bool _cure = false;
    public Sprite _sprite;
    [TextArea]
    public string _sentenceToCompleteAttack;
    [TextArea]
    public string _sentencesToNotUseAttack;
}

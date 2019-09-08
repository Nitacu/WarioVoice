using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTypeAttack", menuName = "Type Attack")]
public class VoiceAttacks : ScriptableObject
{
    public string _verb;
    public AttackGlossary.attack _attack;
    public AudioClip _pronunciation;
    public float _damage;
    public float _cost;
    [TextArea]
    public string _sentenceToCompleteAttack;
    [TextArea]
    public string _riddle;
}

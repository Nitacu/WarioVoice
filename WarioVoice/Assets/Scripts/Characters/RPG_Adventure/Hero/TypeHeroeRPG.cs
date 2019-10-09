using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTypeHeroeRPG", menuName = "Type Hero RPG")]
public class TypeHeroeRPG : ScriptableObject
{
    public Sprite _standing;
    public Sprite _die;
    public Sprite _icon;
    public Sprite _iconDie;
    public RuntimeAnimatorController _animator;
}

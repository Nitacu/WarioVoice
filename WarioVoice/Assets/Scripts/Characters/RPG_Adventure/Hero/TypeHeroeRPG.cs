using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTypeHeroeRPG", menuName = "Type Hero RPG")]
public class TypeHeroeRPG : ScriptableObject
{
    public Sprite _standing;
    public Sprite _icon;
    public RuntimeAnimatorController _animator;
}

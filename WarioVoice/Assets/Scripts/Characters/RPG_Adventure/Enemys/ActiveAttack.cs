using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAttack : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'ActiveAttack._attack' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _attack;
#pragma warning restore CS0649 // El campo 'ActiveAttack._attack' nunca se asigna y siempre tendrá el valor predeterminado null
    public int _damage = 1;
    public void active()
    {
        Instantiate(_attack,transform.position,Quaternion.identity).transform.parent = transform;
    }
}

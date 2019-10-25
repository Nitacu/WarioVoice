using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attack;
    public int _damage = 1;
    public void active()
    {
        Instantiate(_attack,transform.position,Quaternion.identity).transform.parent = transform;
    }
}

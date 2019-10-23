using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attack;

    public void active()
    {
        Instantiate(_attack,transform.position,Quaternion.identity).transform.parent = transform;
    }
}

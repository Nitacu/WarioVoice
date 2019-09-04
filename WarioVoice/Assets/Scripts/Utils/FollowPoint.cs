using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    [SerializeField] private Transform _position;
    
    void Update()
    {
        transform.position = _position.position;
    }
}

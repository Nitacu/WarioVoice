using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;

    public Vector3 Direction { get => _direction; set => _direction = value; }

    void Update()
    {

        transform.position += _direction * _speed * Time.deltaTime;
    }
}

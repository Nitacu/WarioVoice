using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;
    private float _time = 0;
    private float _rate;
    public Vector3 Direction { get => _direction; set => _direction = value; }

    private void Start()
    {
        _rate = 1f / Vector3.Distance(transform.eulerAngles, new Vector3(0,0,180)) * _speed;
    }

    void Update()
    {
        if (_time <= 1)
        {
            _time += Time.deltaTime * _rate;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 180), _time);
        }

    }
}

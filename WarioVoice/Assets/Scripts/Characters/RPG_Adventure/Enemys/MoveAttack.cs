using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : MonoBehaviour
{
    public float _rateTime;
    public float _time = 2;
    public Vector3 _currentEndPosition;
    public float _speed;

    void Update()
    {
        if (_time <= 1)
        {
            _rateTime = 1 / Vector3.Distance(transform.position, _currentEndPosition) * _speed;
            _time = Time.deltaTime * _rateTime;
            transform.position = Vector3.Lerp(transform.position, _currentEndPosition, _time);
        }
    }
}

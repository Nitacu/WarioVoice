using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHeroe : MonoBehaviour
{
    [SerializeField]private Vector3 _starPosition;
    private Vector3 _combatPosition = new Vector3(-1.5f,0,0);
    private Vector3 _currentEndPosition;
    private float _rateTime;
    private float _time = 0; 

    private void Start()
    {
        _starPosition = transform.position;
        _currentEndPosition = transform.position;
    }

    public void changeDirection()
    {
        _time = 0;

        if (_currentEndPosition == _combatPosition)
        {
            _currentEndPosition = _starPosition;
        }
        else
        {
            _currentEndPosition = _combatPosition;
        }

        
    }

    private void Update()
    {
        if (_time <= 1)
        {
            _rateTime = 1 / Vector3.Distance(transform.position, _currentEndPosition) * 5;
            _time = Time.deltaTime * _rateTime;
            transform.position = Vector3.Lerp(transform.position, _currentEndPosition, _time);
        }
    }
}

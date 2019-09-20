using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankWorms : EnemyWorms
{
    [SerializeField] private Transform _pointInFront;
    [SerializeField] private Transform _pointDown;
    private bool _moveRight;
    private float _rate;
    private float _velocity = 0.5f;
    private float _time = 2;
    private Vector2 _endPosition;
    private bool _allowRight = false; // empieza lo contrario

    private void FixedUpdate()
    {
        if (_time <= 1f)
        {
            _time += Time.deltaTime * _rate;

            if (Physics2D.OverlapCircle(_pointDown.position,0.17f))
            {
                transform.position = Vector3.Lerp(transform.position, _endPosition, _time);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            prepareShoot();
            
        }
    }

    public override void prepareShoot()
    {
        RaycastHit2D ray;

        if (_allowRight)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            ray = Physics2D.Raycast(_pointInFront.position, new Vector2(2, 0),3);
            if (ray)
            {
                _endPosition = ray.point - new Vector2(0.7f, 0);
            }
            else
            {
                _endPosition = new Vector2(_pointInFront.position.x - 3, transform.position.y);
            }
            _allowRight = false;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            ray = Physics2D.Raycast(_pointInFront.position, new Vector2(-2, 0),3);

            if (ray)
            {
                _endPosition = ray.point + new Vector2(0.7f, 0);
            }
            else
            {
                _endPosition = new Vector2(_pointInFront.position.x+3,transform.position.y);
            }
            
            _allowRight = true;
        }
        _rate = 1f / Vector2.Distance(transform.position, _endPosition) * _velocity;
        _time = 0;
    }
}

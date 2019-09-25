using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankWorms : EnemyWorms
{
    [SerializeField] private Transform _pointInFront;
    [SerializeField] private Transform _pointInBack;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Transform _pointDownBack;
    private bool _moveRight;
    private float _rate;
    private float _velocity = 0.5f;
    private float _time = 2;
    private Vector2 _endPosition;
    [SerializeField] private bool _allowRight = true; // empieza lo contrario
    [SerializeField] private float _delay = 2;
    private Vector3 _rotateTurret;

    private void FixedUpdate()
    {
        if (_time <= 1f)
        {
            _time += Time.deltaTime * _rate;


            if (_allowRight)
            {
                if (Physics2D.OverlapCircle(_pointDownBack.position, 0.17f))
                    transform.position = Vector3.Lerp(transform.position, _endPosition, _time);
                else
                    _time = 2;
            }
            else
            {

                if (Physics2D.OverlapCircle(_pointDown.position, 0.17f))
                    transform.position = Vector3.Lerp(transform.position, _endPosition, _time);
                else
                    _time = 2;
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            prepareShoot();
        }
    }

    public override void aimPlayer()
    {
        base.aimPlayer();
        _rotateTurret = _turret.transform.eulerAngles;
    }

    public override void prepareShoot()
    {
        RaycastHit2D ray;

        if (_allowRight)
        {
            ray = Physics2D.Raycast(_pointInFront.position, new Vector2(-2, 0), 3);

            if (ray)
            {
                _endPosition = ray.point + new Vector2(0.7f, 0);
            }
            else
            {
                _endPosition = new Vector2(_pointInFront.position.x - _delay, transform.position.y);
            }
            _allowRight = false;
        }
        else
        {
            Debug.Log("izquierda");
            ray = Physics2D.Raycast(_pointInBack.position, new Vector2(2, 0), 3);

            if (ray)
            {
                _endPosition = ray.point - new Vector2(0.7f, 0);
            }
            else
            {
                _endPosition = new Vector2(_pointInBack.position.x + _delay, transform.position.y);
            }

            _allowRight = true;
        }
        _rate = 1f / Vector2.Distance(transform.position, _endPosition) * _velocity;
        _time = 0;
    }
}

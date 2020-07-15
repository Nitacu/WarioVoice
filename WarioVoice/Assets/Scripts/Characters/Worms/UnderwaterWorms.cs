using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterWorms : EnemyWorms
{
    [SerializeField] private Vector3 _pointStart;
#pragma warning disable CS0649 // El campo 'UnderwaterWorms._pointDown' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Transform _pointDown;
#pragma warning restore CS0649 // El campo 'UnderwaterWorms._pointDown' nunca se asigna y siempre tendrá el valor predeterminado null
    private Vector3 _vecDown;
    private GameObject _player;
    private bool _moveRight;
    private float _rate;
    private float _velocity = 0.5f;
    private float _time = 2;
    private Vector3 _endPosition;

    public override void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _pointStart = transform.position;
        _endPosition = _pointStart;
        _vecDown = _pointDown.position;
        base.Start();
    }


    private void FixedUpdate()
    {
        if (_time <= 1f)
        {
            _time += Time.deltaTime * _rate;

            transform.position = Vector3.Lerp(transform.position, _endPosition, _time);

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            prepareShoot();

        }
    }

    public override void prepareShoot()
    {
        if (_endPosition.y == _pointStart.y)
        {
            Debug.Log("arriba");
            _endPosition = _vecDown;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(_endPosition.y == _vecDown.y)
        {
            Debug.Log("abajo");
            _endPosition = _pointStart;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        Debug.Log("cambio posicion " + _endPosition);
        _rate = 1f / Vector2.Distance(transform.position, _endPosition) * _velocity;
        _time = 0;
    }
}

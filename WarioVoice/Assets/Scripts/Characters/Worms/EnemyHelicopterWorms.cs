using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopterWorms : EnemyWorms
{
    [SerializeField] private Transform _pointInFront;
    [SerializeField] private Transform _pointDown;
    private GameObject _player;
    private bool _moveRight;
    private float _rate;
    private float _velocity = 0.5f;
    private float _time = 2;
    private Vector2 _endPosition;
    private bool _allowShoot = true;

    public override void Start()
    {
        _player = GameObject.FindWithTag("Player");
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

    private void Update()
    {
        RaycastHit2D ray;
        ray = Physics2D.Raycast(_pointDown.position, new Vector2(0, -1), Mathf.Infinity);
        aimPlayer();
        if (ray.collider.CompareTag("Player"))
        {
            _time = 2;

            if (_allowShoot)
                shootPlayer();
        }
    }

    public override void shootPlayer()
    {
        base.shootPlayer();
        _allowShoot = false;
    }

    public override void prepareShoot()
    {
        RaycastHit2D ray;

        Vector2 vec = new Vector2(_player.transform.position.x, 0);
        vec.Normalize();
        ray = Physics2D.Raycast(_pointInFront.position, vec, 3);

        if (ray)
        {
            _endPosition = ray.point - new Vector2(0.7f, 0);
        }
        else
        {
            _endPosition = new Vector2(_pointInFront.position.x - 3, transform.position.y);
        }

        _rate = 1f / Vector2.Distance(transform.position, _endPosition) * _velocity;
        _time = 0;
    }
}

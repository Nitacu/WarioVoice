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
    public float Speed { get => _speed; set => _speed = value; }
    public float Rate { get => _rate; set => _rate = value; }
    public float Time { get => _time; set => _time = value; }

    private void Start()
    {
        Rate = 1f / Vector3.Distance(transform.eulerAngles, new Vector3(0,0,180)) * Speed;
    }

    void Update()
    {
        if (Time <= 1)
        {
            Time += UnityEngine.Time.deltaTime * Rate;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 180), Time);
        }

    }
}

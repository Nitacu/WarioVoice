using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorms : MonoBehaviour
{
    [SerializeField] protected int _timeToShoot;
    protected PointingGun _pointingGun;
    [SerializeField] protected GameObject _proyectile;
    [SerializeField] protected Transform _positionShoot;
    [SerializeField] protected GameObject _myFather;
    [SerializeField] protected GameObject _turret;
    [SerializeField] protected typeShoot _typeShoot;
    private const float _SPEED = 12;
    float T;

    public enum typeShoot
    {
        S_MAX,
        S_MIN,
        S_LOW_ENERGI
    }
    public int TimeToShoot { get => _timeToShoot; set => _timeToShoot = value; }

    public virtual void Start()
    {
        _pointingGun = FindObjectOfType<PointingGun>();
        aimPlayer();
    }


    public virtual void aimPlayer()
    {
        float angle;
        Vector3 toTarget = _pointingGun.transform.position - _positionShoot.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = _SPEED * _SPEED + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;
        float discRoot = Mathf.Sqrt(discriminant);

        switch (_typeShoot)
        {
            case typeShoot.S_LOW_ENERGI:
                float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));
                 T = T_lowEnergy;
                break;

            case typeShoot.S_MAX:
                float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);
                 T = T_max;
                break;

            case typeShoot.S_MIN:
                float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);
                T = T_min;
                break;

        }



        //Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        _turret.transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public virtual void prepareShoot()
    { 

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootPlayer();
        }
    }

    public virtual void shootPlayer()
    { 
        GameObject aux;
        float angle;
        aux = Instantiate(_proyectile, _positionShoot.position, Quaternion.identity);
       
        Vector2 vector = _pointingGun.transform.position - aux.transform.position;

        Vector3 toTarget = _pointingGun.transform.position - aux.transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = _SPEED * _SPEED + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant < 0)
        {
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
            Debug.Log("no puedo golpear al objetivo");
        }

        float discRoot = Mathf.Sqrt(discriminant);

        switch (_typeShoot)
        {
            case typeShoot.S_LOW_ENERGI:
                float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));
                T = T_lowEnergy;
                break;

            case typeShoot.S_MAX:
                float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);
                T = T_max;
                break;

            case typeShoot.S_MIN:
                float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);
                T = T_min;
                break;

        }

        //Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
        aux.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);

        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        aux.transform.localEulerAngles = new Vector3(0, 0, angle - 90);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Rocket))
        {
            FindObjectOfType<ConfigurationWorms>().destroyEnemy();
            collision.gameObject.GetComponent<RocketControl>().checkRocket();
            Destroy(_myFather);
        }

    }
}

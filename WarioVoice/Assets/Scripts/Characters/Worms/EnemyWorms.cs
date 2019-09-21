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
    public int TimeToShoot { get => _timeToShoot; set => _timeToShoot = value; }

    public virtual void Start()
    {
        _pointingGun = FindObjectOfType<PointingGun>();
        aimPlayer();
    }


    public void aimPlayer()
    {
        float angle;

        Vector2 vector = _pointingGun.transform.position - transform.position;
        angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        _turret.transform.eulerAngles = new Vector3(0,0,angle);
    }

    public virtual void prepareShoot()
    { 

    }

    public virtual void shootPlayer()
    {
        GameObject aux;
        float angle;
        aux = Instantiate(_proyectile, _positionShoot.position, Quaternion.identity);

        Vector2 vector = _pointingGun.transform.position - aux.transform.position;
        angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        aux.transform.localEulerAngles = new Vector3(0, 0, angle - 90);

        Vector3 vec = _pointingGun.transform.position - aux.transform.position;
        vec.Normalize();
        aux.GetComponent<MoveForward>().Direction = vec;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Rocket))
        {
            FindObjectOfType<ConfigurationWorms>().destroyEnemy();
            Destroy(collision.gameObject);
            Destroy(_myFather);
        }

    }
}

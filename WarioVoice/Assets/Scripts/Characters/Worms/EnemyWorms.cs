using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorms : MonoBehaviour
{
    [SerializeField] private int _timeToShoot;
    private PointingGun _pointingGun;
    [SerializeField] private GameObject _proyectile;
    [SerializeField] private Transform _positionShoot;
    [SerializeField] private GameObject _myFather;
    [SerializeField] private GameObject _turret;
    public int TimeToShoot { get => _timeToShoot; set => _timeToShoot = value; }

    private void Start()
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
    { /*
        _timeToShoot--;
        if (_timeToShoot == 0)
        {
            shootPlayer();
        }
        */
    }

    public void shootPlayer()
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
            Debug.Log("si entra");
            FindObjectOfType<ConfigurationWorms>().destroyEnemy();
            Destroy(collision.gameObject);
            Destroy(_myFather);
        }

    }
}

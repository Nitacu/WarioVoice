using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointingGun : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _proyectile;
    [SerializeField] private Transform _positionShoot;
    [SerializeField] private bool _allowShoot = false;
    [SerializeField] private GameObject _explotion;
    void Update()
    {
        if (transform.localEulerAngles.z != _angle)
            point(_angle);
    }

    public void point(float angle)
    {

        _angle = angle;
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, angle), Time.deltaTime);

        if ((transform.eulerAngles.z - _angle <=1 && transform.eulerAngles.z - _angle >= 0) ||
            (_angle - transform.eulerAngles.z <= 1 && _angle - transform.eulerAngles.z > 0))
        {
            transform.eulerAngles = new Vector3(0,0,_angle);
        }

        if (transform.eulerAngles.z == _angle)
        {
            GetComponent<Ammunition>().useWeapon();
        }
    }

    public void shoot()
    {
        if (AllowShoot)
        {
            GameObject aux;

            aux = Instantiate(_proyectile, _positionShoot.position, Quaternion.identity);
            aux.transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - 90);

            Vector3 vec = aux.transform.position - transform.position;
            vec.Normalize();
            aux.GetComponent<MoveForward>().Direction = vec;

            AllowShoot = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
            Instantiate(_explotion,transform.position,Quaternion.identity);
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Invoke("exitScene", 2);
        }
    }

    public void exitScene()
    {
        SceneManager.LoadScene("WarioVoiceMenu");
    }

    public bool AllowShoot { get => _allowShoot; set => _allowShoot = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointingGun : MonoBehaviour
{
    private float _angle;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _proyectile;
    [SerializeField] private Transform _positionShoot;
    [SerializeField] private GameObject _explotion;
    [SerializeField] private GameObject _turrent;
    [SerializeField] private SpriteRenderer[] _sp;
    private GuideControlWorm _guideControlWorm;

    private void Start()
    {
        _guideControlWorm = FindObjectOfType<GuideControlWorm>();
    }

    void Update()
    {
        if (transform.localEulerAngles.z != _angle)
            point(_angle);
    }

    public void point(float angle)
    {
        _angle = angle;


        if (transform.eulerAngles.z != _angle)
        {
            if ((transform.eulerAngles.z - _angle <= 1 && transform.eulerAngles.z - _angle >= 0) ||
            (_angle - transform.eulerAngles.z <= 1 && _angle - transform.eulerAngles.z > 0))
            {
                _turrent.transform.eulerAngles = new Vector3(0, 0, _angle);

            }
            else
            {
                _turrent.transform.eulerAngles = Vector3.Lerp(_turrent.transform.rotation.eulerAngles, new Vector3(0, 0, angle), Time.deltaTime);
            }

        }

    }

    public void shoot(float force)
    {

        GameObject aux;

        aux = Instantiate(_proyectile, _positionShoot.position, Quaternion.identity);
        aux.transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - 90);


        Vector3 vec = aux.transform.position - _turrent.transform.position;
        vec.Normalize();

        aux.GetComponent<Rigidbody2D>().AddForce(vec * force, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
            Instantiate(_explotion, transform.position, Quaternion.identity);

            foreach (SpriteRenderer spriteRenderer in _sp)
            {
                Destroy(spriteRenderer);
            }

            Invoke("exitScene", 2);
        }
    }

    public void exitScene()
    {
        GameManager.GetInstance().launchNextMinigame(false);
    }

}

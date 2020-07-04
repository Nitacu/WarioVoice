using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private int _amnunition = 0;
    [Header("Donde se vera la municon")]
    [SerializeField]private GameObject _ammoContent;
    [SerializeField] private GameObject _ammoPicture;
    private List<GameObject> _rockets = new List<GameObject>();
    private PointingGun _pointingGun;
    private const float FORCE_SHOOT = 15;
    private GuideControlWorm _guideControlWorm;
    private List<EnemyWorms> _enemys  = new List<EnemyWorms>();
    private bool _shot = false;
    private float _force = 0;
    private void Start()
    {
        _guideControlWorm = FindObjectOfType<GuideControlWorm>();
        _pointingGun = FindObjectOfType<PointingGun>();
    }

    public void turnEnemys()
    {
        _enemys = FindObjectsOfType<EnemyWorms>().ToList();

        foreach (EnemyWorms aux in _enemys)
        {
            aux.prepareShoot();
        }
        _enemys.Clear();
    }

    public void getAmmunition(int ammunition)
    {
        _amnunition += ammunition;

        for (int i = 0;i<ammunition ;i++)
        {
            GameObject aux =  Instantiate(_ammoPicture, _ammoContent.transform);
            _rockets.Add(aux);
        }
    }

    public void useWeapon(float percent)
    {
        if (Amnunition == 0)
        {
            FindObjectOfType<ConfigurationWorms>().lostGame();
        }
        else
        {
            Destroy(_rockets[0]);

            _rockets.RemoveAt(0);

            _force = (percent * FORCE_SHOOT) / 100;
            _shot = true;
            //StartCoroutine(shootCoroutine(force));

            //_pointingGun.shoot(force);
        }
        Amnunition--;
        
    }

    public void Update()
    {
        if (_shot)
        {
            _pointingGun.shoot(_force);
            _shot = false;
        }
    }

    IEnumerator shootCoroutine(float force)
    {
        yield return new WaitForEndOfFrame();

        _pointingGun.shoot(force);

    }

    public int Amnunition { get => _amnunition; set => _amnunition = value; }
}

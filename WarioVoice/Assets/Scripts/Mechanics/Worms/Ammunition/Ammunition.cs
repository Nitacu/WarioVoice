using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private int _amnunition = 0;
    [Header("Donde se vera la municon")]
    [SerializeField]private GameObject _ammoContent;
    [SerializeField] private GameObject _ammoPicture;
    private List<GameObject> _rockets = new List<GameObject>();
    private PointingGun _pointingGun;
    private EnemyWorms[] _enemys;
    private const float FORCE_SHOOT = 15;
    [SerializeField] private GameObject _power;
    [SerializeField] private GameObject _angle;
    private void Start()
    {
        _pointingGun = FindObjectOfType<PointingGun>();
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
            _enemys = FindObjectsOfType<EnemyWorms>();

            foreach (EnemyWorms aux in _enemys)
            {
                aux.prepareShoot();
            }

            _rockets.RemoveAt(0);

            float force = (percent * FORCE_SHOOT) / 100;
            
            _pointingGun.shoot(force);
        }
        Amnunition--;

        if (Amnunition >= 0)
        {
            Debug.Log("entyro");
            _power.SetActive(false);
            _angle.SetActive(true);
        }
        else
        {
            _power.SetActive(false);
            _angle.SetActive(false);
        }
    }

    public int Amnunition { get => _amnunition; set => _amnunition = value; }
}

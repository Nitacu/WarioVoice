﻿using System.Collections;
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

    public void useWeapon()
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
            _pointingGun.shoot();
        }
        Amnunition--;
    }

    public int Amnunition { get => _amnunition; set => _amnunition = value; }
}

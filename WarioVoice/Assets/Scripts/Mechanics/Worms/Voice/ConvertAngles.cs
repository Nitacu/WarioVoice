using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertAngles : CommandParser
{

    private string[] _sentenses;
    private PointingGun _pointingGun;
    private Ammunition _ammunition;
    private bool _allowPoint = true;
    private bool _allowShoot = false;
    [SerializeField] private string _a;
    [SerializeField] private GameObject _power;
    [SerializeField] private GameObject _angles;

    private void Start()
    {
        _pointingGun = FindObjectOfType<PointingGun>();
        _ammunition = FindObjectOfType<Ammunition>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            parseCommand(_a);
        }
    }

    public override void parseCommand(string command)
    {
        command = command.Replace(' ', '_');
        _sentenses = command.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        if (_allowPoint)
        {
            if (_sentenses.Length == 2)
            {

                if (string.Equals(_sentenses[1], GlossaryOfAngles.DEGREES))
                {
                    try
                    {
                        int result = Int32.Parse(_sentenses[0]);
                        _pointingGun.point(result);
                        _pointingGun.AllowShoot = true;
                        _allowShoot = true;
                        _allowPoint = false;
                        _angles.SetActive(false);
                        _power.SetActive(true);
                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
            }
            else if (_sentenses.Length == 1)
            {

                if (_sentenses[0][_sentenses[0].Length - 1] == GlossaryOfAngles.SYMBOL_GRADES.ToCharArray()[0])
                {

                    _sentenses = _sentenses[0].Split("°".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        int result = Int32.Parse(_sentenses[0]);
                        _pointingGun.point(result);
                        _pointingGun.AllowShoot = true;
                        _allowShoot = true;
                        _allowPoint = false;
                        _angles.SetActive(false);
                        _power.SetActive(true);
                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
            }
        }
        else if (_allowShoot)
        {
            if (_sentenses.Length == 2)
            {

                if (string.Equals(_sentenses[1], GlossaryOfAngles.PERCENT))
                {
                    try
                    {
                        int result = Int32.Parse(_sentenses[0]);

                        if (result <= 100)
                        {
                            _ammunition.useWeapon(result);
                            _allowShoot = false;
                            _allowPoint = true;
                        }

                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
            }
            else if (_sentenses.Length == 1)
            {

                if (_sentenses[0][_sentenses[0].Length - 1] == GlossaryOfAngles.SYMBOL_PERCENT.ToCharArray()[0])
                {

                    _sentenses = _sentenses[0].Split("%".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        int result = Int32.Parse(_sentenses[0]);
                        if (result <= 100)
                        {
                            _ammunition.useWeapon(result);
                            _allowShoot = false;
                            _allowPoint = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
            }
        }


    }
}

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
    private GuideControlWorm _controlWorm;
    private bool _allowPoint = true;
    private bool _allowShoot = false;
    [SerializeField] private string _a;
    [SerializeField] private TMP_InputField _inputField;

    private void Start()
    {
        _controlWorm = FindObjectOfType<GuideControlWorm>();
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

    public void angle()
    {
        Debug.Log(_inputField.text);
        parseCommand(_inputField.text);
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
                        _controlWorm.desactiveAll();
                        _controlWorm.Invoke("activePower",2);
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
                        _controlWorm.desactiveAll();
                        _controlWorm.Invoke("activePower", 2);
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
                            _controlWorm.desactiveAll();
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
                            _controlWorm.desactiveAll();
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertAngles : CommandParser
{

    private string[] _sentenses;
    private PointingGun _pointingGun;


    private void Start()
    {
        _pointingGun = FindObjectOfType<PointingGun>();
    }

    public override void parseCommand(string command)
    {
        command = command.Replace(' ', '_');
        _sentenses = command.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        if (_sentenses.Length == 2)
        {

            if (string.Equals(_sentenses[1], GlossaryOfAngles.DEGREES))
            {
                try
                {
                    int result = Int32.Parse(_sentenses[0]);
                    _pointingGun.point(result);
                    _pointingGun.AllowShoot = true;
                }
                catch (FormatException)
                {
                    Debug.Log($"Unable to parse '{_sentenses[0]}'");
                }
            }
        }
        else if (_sentenses.Length == 1)
        {
            try
            {
                int result = Int32.Parse(_sentenses[0]);
                _pointingGun.point(result);
                _pointingGun.AllowShoot = true;
            }
            catch (FormatException)
            {
                Debug.Log($"Unable to parse '{_sentenses[0]}'");
            }

        }
    }
}

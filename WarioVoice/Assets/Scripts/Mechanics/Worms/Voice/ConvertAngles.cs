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
    [SerializeField]private bool _allowPoint = false;
    [SerializeField] private bool _allowShoot = false;
    [SerializeField] private TMP_InputField _inputField;
    public bool AllowPoint { get => _allowPoint; set => _allowPoint = value; }
    public bool AllowShoot { get => _allowShoot; set => _allowShoot = value; }

    private void Start()
    {
        _controlWorm = FindObjectOfType<GuideControlWorm>();
        _pointingGun = FindObjectOfType<PointingGun>();
        _ammunition = FindObjectOfType<Ammunition>();
        allowPoint();
    }

    public void angle()
    {
        parseCommand(_inputField.text);
    }

    public void allowPower()
    {
        if (TutorialMode)
        {
            _allowShoot = true;
            _allowPoint = false;
            //cambio visual
            FindObjectOfType<GuideControlWorm>().activePower();
        }
        else
        {
            _allowShoot = true;
            _allowPoint = true;
            FindObjectOfType<SetActiveSpeechButton>().setButton(true);
        }

    }

    public void allowPoint()
    {
        if (TutorialMode)
        {
            _allowShoot = false;
            _allowPoint = true;
            //cambio visual
            FindObjectOfType<GuideControlWorm>().activeAngle();
        }
        else
        {
            _allowShoot = true;
            _allowPoint = true;
            FindObjectOfType<SetActiveSpeechButton>().setButton(true);
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
                
                if (string.Equals(_sentenses[1], GlossaryOfAngles.DEGREES) || 
                    string.Equals(_sentenses[1], GlossaryOfAngles.DEGREE))
                {
                    try
                    {
                        int result = Int32.Parse(_sentenses[0]);

                        if (result <= 180 && result >= 0)
                        {
                            SaveSystem.increaseMicrophonePressedTime(true);
                            _pointingGun.point(result);
                            _controlWorm.desactiveAll();
                            if (TutorialMode)
                            {
                                Invoke("allowPower", 2);
                            }
                            else
                            {
                                _controlWorm.Invoke("activeKeepAction", 2);
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
                else
                {
                    SaveSystem.increaseMicrophonePressedTime(false);
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

                        if (result <= 180 && result >= 0)
                        {
                            SaveSystem.increaseMicrophonePressedTime(true);
                            _pointingGun.point(result);
                            _controlWorm.desactiveAll();
                            if (TutorialMode)
                            {
                                Invoke("allowPower", 2);
                            }
                            else
                            {
                                _controlWorm.Invoke("activeKeepAction", 2);
                            }
                        }

                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
                else
                {
                    SaveSystem.increaseMicrophonePressedTime(false);
                }
            }
            else
            {
                SaveSystem.increaseMicrophonePressedTime(false);
            }
        }
        if (_allowShoot)
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
                            SaveSystem.increaseMicrophonePressedTime(true);
                            _ammunition.useWeapon(result);
                            _controlWorm.desactiveAll();
                        }

                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
                else
                {
                    SaveSystem.increaseMicrophonePressedTime(false);
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
                            SaveSystem.increaseMicrophonePressedTime(true);
                            _ammunition.useWeapon(result);
                            _controlWorm.desactiveAll();
                        }
                    }
                    catch (FormatException)
                    {
                        Debug.Log($"Unable to parse '{_sentenses[0]}'");
                    }
                }
                else
                {
                    SaveSystem.increaseMicrophonePressedTime(false);
                }
            }
            else
            {
                SaveSystem.increaseMicrophonePressedTime(false);
            }
        }


    }
}

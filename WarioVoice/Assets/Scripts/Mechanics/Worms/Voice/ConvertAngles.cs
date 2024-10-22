﻿using System;
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
    private bool _allowPoint = false;
    private bool _allowShoot = false;
#pragma warning disable CS0649 // El campo 'ConvertAngles._inputField' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_InputField _inputField;
#pragma warning restore CS0649 // El campo 'ConvertAngles._inputField' nunca se asigna y siempre tendrá el valor predeterminado null
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

    public override void parseCommand(string command, string originalText)
    {
        originalText = originalText.Replace(' ', '_');
        _sentenses = originalText.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

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
                            SaveSystem.increaseMicrophonePressedTime(true, originalText, ChangeScene.EspikinglishMinigames.WORMS);
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
                            SaveSystem.increaseMicrophonePressedTime(true, originalText, ChangeScene.EspikinglishMinigames.WORMS);
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
                            SaveSystem.increaseMicrophonePressedTime(true, originalText, ChangeScene.EspikinglishMinigames.WORMS);
                            _ammunition.useWeapon(result);
                            //_controlWorm.desactiveAll();
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
                            SaveSystem.increaseMicrophonePressedTime(true, originalText, ChangeScene.EspikinglishMinigames.WORMS);
                            _ammunition.useWeapon(result);
                            //_controlWorm.desactiveAll();
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

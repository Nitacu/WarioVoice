using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternCheck : CommandParser
{
    private PatronController patternController;
    private CrystalController.Colors enumColor;
    private bool colorWord = false;

    private void Start()
    {
        patternController = GetComponent<PatronController>();
    }

    public override void parseCommand(string command)
    {
        getColor(command);
    }

    public void parseTextTest(string text)
    {
        getColor(text);
    }

    private void getColor(string color)
    {
        foreach (Crystal crystal in patternController.crystalList)
        {
            if (color == crystal.crystalColor.ToString())
            {
                colorWord = true;
            }
        }

        switch (color)
        {
            case "BLUE":
                enumColor = CrystalController.Colors.BLUE;
                break;
            case "GREEN":
                enumColor = CrystalController.Colors.GREEN;
                break;
            case "YELLOW":
                enumColor = CrystalController.Colors.YELLOW;
                break;
            case "PINK":
                enumColor = CrystalController.Colors.PINK;
                break;
            case "BROWN":
                enumColor = CrystalController.Colors.BROWN;
                break;
            case "ORANGE":
                enumColor = CrystalController.Colors.ORANGE;
                break;
            case "LIME":
                enumColor = CrystalController.Colors.LIME;
                break;
            case "SILVER":
                enumColor = CrystalController.Colors.SILVER;
                break;
            case "MAGENTA":
                enumColor = CrystalController.Colors.MAGENTA;
                break;
            case "WHITE":
                enumColor = CrystalController.Colors.WHITE;
                break;
            case "TURQUOISE":
                enumColor = CrystalController.Colors.TURQUOISE;
                break;
            case "BLACK":
                enumColor = CrystalController.Colors.BLACK;
                break;
            case "INDIGO":
                enumColor = CrystalController.Colors.INDIGO;
                break;
            case "RED":
                enumColor = CrystalController.Colors.RED;
                break;
            case "FUCHSIA":
                enumColor = CrystalController.Colors.FUCHSIA;
                break;

        }

        patternController.checkVoice(enumColor, colorWord);


    }



}

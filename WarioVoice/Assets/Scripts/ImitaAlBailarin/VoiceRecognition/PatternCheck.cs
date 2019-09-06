using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCheck : CommandParser
{
    private PatronController patternController;
    private CrystalController.Colors enumColor;

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
        }

        patternController.checkVoice(enumColor);

        
    }

}

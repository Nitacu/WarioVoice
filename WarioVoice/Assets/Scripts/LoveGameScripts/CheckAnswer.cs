using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswer : CommandParser
{
    WordController wordControl;

    private void Start()
    {
        wordControl = GetComponent<WordController>();
    }

    public override void parseCommand(string command)
    {
        wordControl.checkAnswer(command);
    }
}

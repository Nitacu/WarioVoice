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

    public override void parseCommand(string command, string originalText)
    {
        StartCoroutine(checkAnswerCoroutine(originalText));
        //wordControl.checkAnswer(originalText);
    }

    IEnumerator checkAnswerCoroutine(string originaltext)
    {
        yield return new WaitForEndOfFrame();
        wordControl.checkAnswer(originaltext);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestVoiceInput : MonoBehaviour
{
    public TMP_InputField text;

    public void testVoice()
    {
        FindObjectOfType<CommandParser>().parseCommand(text.text);
    }
}

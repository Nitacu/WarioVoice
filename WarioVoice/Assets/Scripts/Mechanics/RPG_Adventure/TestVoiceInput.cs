using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestVoiceInput : MonoBehaviour
{
    public TMP_Dropdown text;

    public void testVoice()
    {
        switch (text.value)
        {
            case 0:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.ACCUSE);
                break;

            case 1:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.BLOW);
                break;

            case 2:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.BURN);
                break;

            case 3:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.DOWNGRADE);
                break;

            case 4:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.EAT);
                break;

            case 5:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.FLY);
                break;

            case 6:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.GIVE_AWAY);
                break;

            case 7:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.HIT);
                break;

            case 8:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.LAUGH);
                break;

            case 9:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.LOOK);
                break;

            case 10:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.PASTE);
                break;

            case 11:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.SCRATCH);
                break;

            case 12:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.SHOOT);
                break;

            case 13:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.SHOUT);
                break;

            case 14:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.TELL);
                break;

            case 15:
                FindObjectOfType<CommandParser>().parseCommand(AttackGlossary.THROW);
                break;
                
        }
    }
}

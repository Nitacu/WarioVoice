using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertAttack : CommandParser
{
    private ExeAttack _exeAttack;

    private void Start()
    {
        _exeAttack = FindObjectOfType<ExeAttack>();
    }

    public override void parseCommand(string command)
    {

        switch (command)
        {
            case AttackGlossary.ACCUSE:
                _exeAttack.TypeAttack = AttackGlossary.attack.ACCUSE;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.BLOW:
                _exeAttack.TypeAttack = AttackGlossary.attack.BLOW;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.BURN:
                _exeAttack.TypeAttack = AttackGlossary.attack.BURN;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.DOWNGRADE:
                _exeAttack.TypeAttack = AttackGlossary.attack.DOWNGRADE;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.EAT:
                _exeAttack.TypeAttack = AttackGlossary.attack.EAT;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.FLY:
                _exeAttack.TypeAttack = AttackGlossary.attack.FLY;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.GIVE_AWAY:
                _exeAttack.TypeAttack = AttackGlossary.attack.GIVE_AWAY;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.HIT:
                _exeAttack.TypeAttack = AttackGlossary.attack.HIT;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.LAUGH:
                _exeAttack.TypeAttack = AttackGlossary.attack.LAUGH;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.LOOK:
                _exeAttack.TypeAttack = AttackGlossary.attack.LOOK;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.PASTE:
                _exeAttack.TypeAttack = AttackGlossary.attack.PASTE;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.SHOOT:
                _exeAttack.TypeAttack = AttackGlossary.attack.SHOOT;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.SHOUT:
                _exeAttack.TypeAttack = AttackGlossary.attack.SHOUT;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.TELL:
                _exeAttack.TypeAttack = AttackGlossary.attack.TELL;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.THROW:
                _exeAttack.TypeAttack = AttackGlossary.attack.THROW;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.SCRATCH:
                _exeAttack.TypeAttack = AttackGlossary.attack.SCRATCH;
                _exeAttack.selectAttack();
                break;

            default:
                Debug.Log("la palabra " + command + " no coincide");
                break;
        }
    }

}

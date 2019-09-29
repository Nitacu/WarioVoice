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
            case AttackGlossary.CHLORINE:
                _exeAttack.TypeAttack = AttackGlossary.attack.CHORINE;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.LEMON:
                _exeAttack.TypeAttack = AttackGlossary.attack.LEMON;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.ONION:
                _exeAttack.TypeAttack = AttackGlossary.attack.ONION;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.PAPER_PLANE:
                _exeAttack.TypeAttack = AttackGlossary.attack.PAPER_PLANE;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.PEPPER:
                _exeAttack.TypeAttack = AttackGlossary.attack.PEPPER;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.PERFUME:
                _exeAttack.TypeAttack = AttackGlossary.attack.PERFUME;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.BUBBLE_GUM:
                _exeAttack.TypeAttack = AttackGlossary.attack.BUBLE_BUM;
                _exeAttack.selectAttack();
                
                break;

            case AttackGlossary.SWORD:
                _exeAttack.TypeAttack = AttackGlossary.attack.SWORD;
                _exeAttack.selectAttack();
                
                break;


            default:
                
                break;
        }
    }

}

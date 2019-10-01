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

            case AttackGlossary.BLACK_PEPPER:
                _exeAttack.TypeAttack = AttackGlossary.attack.BLACK_PEPPER;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.PERFUME:
                _exeAttack.TypeAttack = AttackGlossary.attack.PERFUME;
                _exeAttack.selectAttack();
                break;

            case AttackGlossary.CANDY:
                _exeAttack.TypeAttack = AttackGlossary.attack.CANDY;
                _exeAttack.selectAttack();
                
                break;

            case AttackGlossary.UMBRELLA:
                _exeAttack.TypeAttack = AttackGlossary.attack.UMBRELLA;
                _exeAttack.selectAttack();
                
                break;

            case AttackGlossary.SPATULA:
                _exeAttack.TypeAttack = AttackGlossary.attack.SPATULA;
                _exeAttack.selectAttack();

                break;

            case AttackGlossary.NEWSPAPER:
                _exeAttack.TypeAttack = AttackGlossary.attack.NEWSPAPER;
                _exeAttack.selectAttack();

                break;

            case AttackGlossary.COIN:
                _exeAttack.TypeAttack = AttackGlossary.attack.COIN;
                _exeAttack.selectAttack();

                break;

            case AttackGlossary.CUPCAKE:
                _exeAttack.TypeAttack = AttackGlossary.attack.CUPCAKE;
                _exeAttack.selectAttack();

                break;


            default:
                SaveSystem.increaseMicrophonePressedTime(false);
                break;
        }
    }

}

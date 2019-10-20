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

    public override void parseCommand(string command, string originalText)
    {
        switch (originalText)
        {
            case AttackGlossary.HIGH_HEEL:
                _exeAttack.TypeAttack = AttackGlossary.attack.HIGH_HEEL;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.LEMON:
                _exeAttack.TypeAttack = AttackGlossary.attack.LEMON;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.ONION:
                _exeAttack.TypeAttack = AttackGlossary.attack.ONION;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.GLASSES:
                _exeAttack.TypeAttack = AttackGlossary.attack.GLASSES;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.BLACK_PEPPER:
                _exeAttack.TypeAttack = AttackGlossary.attack.BLACK_PEPPER;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.PERFUME:
                _exeAttack.TypeAttack = AttackGlossary.attack.PERFUME;
                _exeAttack.selectAttack(originalText);
                break;

            case AttackGlossary.CANDY:
                _exeAttack.TypeAttack = AttackGlossary.attack.CANDY;
                _exeAttack.selectAttack(originalText);
                
                break;

            case AttackGlossary.UMBRELLA:
                _exeAttack.TypeAttack = AttackGlossary.attack.UMBRELLA;
                _exeAttack.selectAttack(originalText);
                
                break;

            case AttackGlossary.SPATULA:
                _exeAttack.TypeAttack = AttackGlossary.attack.SPATULA;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.NEWSPAPER:
                _exeAttack.TypeAttack = AttackGlossary.attack.NEWSPAPER;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.COIN:
                _exeAttack.TypeAttack = AttackGlossary.attack.COIN;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.CUPCAKE:
                _exeAttack.TypeAttack = AttackGlossary.attack.CUPCAKE;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.BATTERY:
                _exeAttack.TypeAttack = AttackGlossary.attack.BATTERY;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.CHOCOLATE:
                _exeAttack.TypeAttack = AttackGlossary.attack.CHOCOLATE;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.EXTINGUISHER:
                _exeAttack.TypeAttack = AttackGlossary.attack.EXTINGUISHER;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.FAN:
                _exeAttack.TypeAttack = AttackGlossary.attack.FAN;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.PICKAXE:
                _exeAttack.TypeAttack = AttackGlossary.attack.PICKAXE;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.SAND:
                _exeAttack.TypeAttack = AttackGlossary.attack.SAND;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.SPONGE:
                _exeAttack.TypeAttack = AttackGlossary.attack.SPONGE;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.STRAWBERRY:
                _exeAttack.TypeAttack = AttackGlossary.attack.STRAWBERRY;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.VACUUM_CLEANER:
                _exeAttack.TypeAttack = AttackGlossary.attack.VACUUM_CLEANER;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.DRILL:
                _exeAttack.TypeAttack = AttackGlossary.attack.DRILL;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.ANTENNA:
                _exeAttack.TypeAttack = AttackGlossary.attack.ANTENNA;
                _exeAttack.selectAttack(originalText);

                break;

            case AttackGlossary.BAT:
                _exeAttack.TypeAttack = AttackGlossary.attack.BAT;
                _exeAttack.selectAttack(command);

                break;

            case AttackGlossary.BOUQUET:
                _exeAttack.TypeAttack = AttackGlossary.attack.BOUQUET;
                _exeAttack.selectAttack(command);

                break;

            case AttackGlossary.HONEY:
                _exeAttack.TypeAttack = AttackGlossary.attack.HONEY;
                _exeAttack.selectAttack(command);

                break;

            case AttackGlossary.KITE:
                _exeAttack.TypeAttack = AttackGlossary.attack.KITE;
                _exeAttack.selectAttack(command);

                break;

            case AttackGlossary.LEAVES_BLOWER:
                _exeAttack.TypeAttack = AttackGlossary.attack.LEAVES_BLOWER;
                _exeAttack.selectAttack(command);

                break;

            case AttackGlossary.RACKET:
                _exeAttack.TypeAttack = AttackGlossary.attack.RACKET;
                _exeAttack.selectAttack(command);

                break;

            default:
                SaveSystem.increaseMicrophonePressedTime(false);
                break;
        }
    }

}

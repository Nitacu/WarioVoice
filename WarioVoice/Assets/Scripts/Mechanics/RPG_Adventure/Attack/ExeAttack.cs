using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeAttack : MonoBehaviour
{
    private AttackGlossary.attack _typeAttack;
    private LamiaController _lamia;
    private List<VoiceAttacks> _listAttacks = new List<VoiceAttacks>();
    private HeroProperties _hero;
    private VoiceAttacks _currentAttack;
    private ControlShifts _controlShifts;

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _controlShifts = FindObjectOfType<ControlShifts>();
    }

    public void prepareAttack(List<VoiceAttacks> listAttacks, HeroProperties hero)
    {
        _listAttacks.Clear();
        _listAttacks = listAttacks;
        _hero = hero;
    }

    public bool characterContainsAttack()
    {
        foreach (VoiceAttacks attacks in _listAttacks)
        {
            if (attacks._attack == _typeAttack)
            {
                _currentAttack = attacks;
                return true;
            }
        }

        return false;
    }

    public void selectAttack( )
    {
        
        if (characterContainsAttack() )
        {
            switch (_typeAttack)
            {
                case AttackGlossary.attack.ACCUSE:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.BLOW:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.BURN:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.DOWNGRADE:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.EAT:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.FLY:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.GIVE_AWAY:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.HIT:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.LAUGH:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.LOOK:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.PASTE:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.SCRATCH:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.SHOOT:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.SHOUT:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.TELL:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

                case AttackGlossary.attack.THROW:
                    _lamia.lostLife(_currentAttack._damage);
                    _hero.getCharacterStastic(0, _currentAttack._cost);
                    break;

            }
            FindObjectOfType<ControlShifts>().playerEnemy();
            FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
        }
  
    }


    public AttackGlossary.attack TypeAttack { get => _typeAttack; set => _typeAttack = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeAttack : MonoBehaviour
{
    [Header("acertijos")]
    private List<VoiceAttacks> _riddle;
    private AttackGlossary.attack _typeAttack; //lo masnda el comanparse 
    [SerializeField] private AttackGlossary.attack _correctAttack; //es el que tiene el que se tiene que usar
    private LamiaController _lamia;
    private List<VoiceAttacks> _listAttacks = new List<VoiceAttacks>();
    private HeroProperties _hero;
    [SerializeField] private VoiceAttacks _currentAttack;
    private ControlShifts _controlShifts;
    [SerializeField] private VisualDamage _visualDamage;
    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _controlShifts = FindObjectOfType<ControlShifts>();
    }

    public void prepareAttack(List<VoiceAttacks> listAttacks, HeroProperties hero)
    {
        _listAttacks = listAttacks;
        _hero = hero;
    }

    //revisa si el heroe tiene el ataque 
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

    public void selectAttack()
    {
        SaveSystem.increaseMicrophonePressedTime(true);
        if (characterContainsAttack())
        {
            if (_lamia.effectiveAttack(_typeAttack))
            {
                //aplcia el daño
                if (_lamia.lostLife(_currentAttack._damage))
                    _controlShifts.Invoke("playerEnemy", 2);
                // visual

                //frase del ataque
                FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
                _hero.GetComponent<MoveHeroe>().changeDirection();
                _hero.GetComponent<HeroProperties>().Attacks.Remove(_currentAttack);

            }
            else
            {
                _controlShifts.Invoke("playerEnemy", 2);
                FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
                _hero.GetComponent<MoveHeroe>().changeDirection();
            }
        }
    }


    public AttackGlossary.attack TypeAttack { get => _typeAttack; set => _typeAttack = value; }
    public AttackGlossary.attack CorrectAttack { get => _correctAttack; set => _correctAttack = value; }
}

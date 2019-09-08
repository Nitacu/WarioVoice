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

        if (characterContainsAttack() && CorrectAttack == _typeAttack)
        {

            //aplcia el daño
            if (_lamia.lostLife(_currentAttack._damage))
                FindObjectOfType<ControlShifts>().playerTurn();
            // visual
            _visualDamage.gameObject.transform.position = _lamia.transform.position;
            _visualDamage.gameObject.SetActive(true);
            _visualDamage.showDamage(_currentAttack._damage);
            //frase del ataque
            FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
            _hero.GetComponent<MoveHeroe>().changeDirection();

        }
        else
        {
            _lamia.attack(_hero);
        }

    }


    public AttackGlossary.attack TypeAttack { get => _typeAttack; set => _typeAttack = value; }
    public AttackGlossary.attack CorrectAttack { get => _correctAttack; set => _correctAttack = value; }
}

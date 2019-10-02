using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeAttack : MonoBehaviour
{
    [Header("acertijos")]
    private List<VoiceAttacks> _riddle;
    private AttackGlossary.attack _typeAttack; //lo masnda el comanparse 
    private LamiaController _lamia;
    private List<VoiceAttacks> _listAttacks = new List<VoiceAttacks>();
    private HeroProperties _hero;
    [SerializeField] private VoiceAttacks _currentAttack;
    private ControlShifts _controlShifts;
    [SerializeField] private VisualDamage _visualDamage;
    [SerializeField] private GameObject _usedObject; //muestra que objeto uso

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
            // crea el objeto que muestra cual es que uso
            GameObject usedObject = Instantiate(_usedObject);
            usedObject.GetComponent<SpriteRenderer>().sprite = _currentAttack._sprite;
            usedObject.GetComponent<AudioSource>().clip = _currentAttack._soundEffect;
            usedObject.GetComponent<AudioSource>().Play();

            if (_currentAttack._cure)
            {
                HeroProperties[] aux =  FindObjectsOfType<HeroProperties>();
                List<HeroProperties> revive = new List<HeroProperties>();

                foreach (HeroProperties hero in aux)
                {
                    if (!hero.IsLive)
                        _lamia.Characters.Add(hero);

                    hero.getCharacterStastic(1);
                    
                }
                
                //para finalizar el turno
                _controlShifts.Invoke("playerEnemy", 3);
                //frase del ataque
                FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
            }
            else
            {
                if (_lamia.effectiveAttack(_typeAttack))
                {
                    //aplcia el daño
                    if (_lamia.lostLife(_currentAttack._damage))
                        _controlShifts.Invoke("playerEnemy", 3);
                    // visual

                    //frase del ataque
                    FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
                }
                else
                {
                    _controlShifts.Invoke("playerEnemy", 3);
                    FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentencesToNotUseAttack);
                }
            }
            _hero.GetComponent<MoveHeroe>().Invoke("changeDirection", 1);
            _hero.GetComponent<HeroProperties>().Attacks.Remove(_currentAttack);
            FindObjectOfType<LevelInformationPanel>().activeDialogue(_currentAttack._sentenceToCompleteAttack);
        }
    }


    public AttackGlossary.attack TypeAttack { get => _typeAttack; set => _typeAttack = value; }
}
